using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_20
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            List<Tile> tiles = GetListOfTiles(input);

            Console.WriteLine("########## Day 20 2020 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(tiles)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(tiles)}");
            Console.WriteLine("#################################");
        }

        private static long SolvePartOne(List<Tile> tiles)
        {
            Dictionary<int, int> positions = new Dictionary<int, int>();
            List<string> checkedCombinations = new List<string>();
            for (int i = 0; i < tiles.Count; i++)
            {
                for (int j = 0; j < tiles.Count; j++)
                {
                    Tile t1 = tiles[i];
                    Tile t2 = tiles[j];
                    if (t1.ID == t2.ID)
                    {
                        continue;
                    }

                    if (t1.Edges.Where(x => t2.Edges.Any(c => c == x)).Count() > 0)
                    {
                        if (checkedCombinations.Contains($"{t1.ID}{t2.ID}"))
                        {
                            continue;
                        }

                        checkedCombinations.Add($"{t1.ID}{t2.ID}");
                        if (checkedCombinations.Contains($"{t2.ID}{t1.ID}"))
                        {
                            continue;
                        }

                        checkedCombinations.Add($"{t2.ID}{t1.ID}");
                        if (positions.ContainsKey(t1.ID))
                        {
                            positions[t1.ID]++;
                        }
                        else
                        {
                            positions.Add(t1.ID, 1);
                        }

                        if (positions.ContainsKey(t2.ID))
                        {
                            positions[t2.ID]++;
                        }
                        else
                        {
                            positions.Add(t2.ID, 1);
                        }
                    }
                }
            }

            return positions.Where(x => x.Value == 2).Select(x => x.Key).Aggregate((long)1, (x, y) => (long)x * (long)y);
        }

        private static List<Tile> GetListOfTiles(string[] input)
        {
            List<Tile> tiles = new List<Tile>();
            Tile currentTile = new Tile() { ImageData = new List<string>(), NamedEdges = new Dictionary<string, string>() };
            foreach (string line in input)
            {
                if (line == "")
                {
                    tiles.Add(currentTile);
                    currentTile = new Tile() { ImageData = new List<string>(), NamedEdges = new Dictionary<string, string>() };
                    continue;
                }

                if (line.StartsWith("Tile "))
                {
                    currentTile.ID = int.Parse(line.Split(" ")[1].Trim().Replace(":", ""));
                }
                else
                {
                    currentTile.ImageData.Add(line);
                }
            }

            tiles.Add(currentTile);
            List<Tile> tilesToReturn = new List<Tile>();
            foreach (Tile tile in tiles)
            {
                tilesToReturn.Add(CalculateEdges(tile));
            }

            return tilesToReturn;
        }

        private static Tile CalculateEdges(Tile tile)
        {
            tile.NamedEdges = new Dictionary<string, string>();
            int counter = tile.ImageData.First().Count();
            string upperEgde = tile.ImageData[0];
            tile.NamedEdges.Add("UpperEdge", upperEgde);

            string upperEdgeReverse = ReverseString(upperEgde);
            tile.NamedEdges.Add("UpperEdgeReverse", upperEdgeReverse);

            string bottomEdge = tile.ImageData[counter - 1];
            tile.NamedEdges.Add("BottomEdge", bottomEdge);

            string bottomEdgeReverse = ReverseString(bottomEdge);
            tile.NamedEdges.Add("BottomEdgeReverse", bottomEdgeReverse);

            string leftEdge = String.Join("", tile.ImageData.Select(x => x[0].ToString()).ToList());
            tile.NamedEdges.Add("LeftEdge", leftEdge);

            string leftEdgeReverse = ReverseString(leftEdge);
            tile.NamedEdges.Add("LeftEdgeReverse", leftEdgeReverse);

            string rightEdge = String.Join("", tile.ImageData.Select(x => x[9].ToString()).ToList());
            tile.NamedEdges.Add("RightEdge", rightEdge);

            string rightEdgeReverse = ReverseString(rightEdge);
            tile.NamedEdges.Add("RightEdgeReverse", rightEdgeReverse);

            tile.Edges = new List<string>() { upperEgde, upperEdgeReverse, bottomEdge, bottomEdgeReverse, leftEdge, leftEdgeReverse, rightEdge, rightEdgeReverse };

            return tile;
        }

        private static string ReverseString(string upperEgde)
        {
            char[] charArray = upperEgde.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        private static int SolvePartTwo(List<Tile> tiles)
        {
            tiles = SetTilesPositions(tiles);
            tiles = FlipTiles(tiles);
            List<string> result = MergeMap(tiles);

            var monster = new string[]{
                "                  # ",
                "#    ##    ##    ###",
                " #  #  #  #  #  #   "
            };

            int rows = result.First().Length;
            int cols = result.Count;

            int monstersNumner = 0;
            int iterator = 0;
            
            while (true)
            {
                monstersNumner = CountMonsers(result, monster);
                if (monstersNumner != 0)
                {
                    break;
                }

                result = new List<string>(RotateClockWise(result, cols-1));
                if (iterator == 4) 
                {
                    result.Reverse();
                }

                iterator++;
            }


            int hashNumberInImage = result.Select(x => x.Count(c => c == '#')).Aggregate(0, (x, y) => x + y);
            int hashNumberInMonster = monster.Select(x => x.Count(c => c == '#')).Aggregate(0, (x, y) => x + y);

            return hashNumberInImage - hashNumberInMonster * monstersNumner;
        }

        private static List<Tile> FlipTiles(List<Tile> tiles)
        {
            Tile topLeft = tiles.Where(x => x.X == 0 && x.Y == 0).FirstOrDefault();
            Tile nextLeft = tiles.Where(x => x.X == 1 && x.Y == 0).FirstOrDefault();
            Tile bottomNext = tiles.Where(x => x.X == 0 && x.Y == 1).FirstOrDefault();

            bool foundedLeft = false;
            bool foundedBottom = false;
            topLeft.Done = true;

            if (!foundedBottom)
            {
                foreach (var topLeftTile in topLeft.NamedEdges)
                {
                    foreach (var bottomNextTile in bottomNext.NamedEdges)
                    {
                        if (topLeftTile.Value == bottomNextTile.Value)
                        {
                            if (topLeftTile.Key == "UpperEdge" && bottomNextTile.Key == "BottomEdge")
                            {
                                topLeft.ImageData.Reverse();
                                topLeft = CalculateEdges(topLeft);

                                bottomNext.ImageData.Reverse();
                                bottomNext = CalculateEdges(bottomNext);
                            }
                            else if (topLeftTile.Key == "BottomEdge" && bottomNextTile.Key == "LeftEdge")
                            {
                                bottomNext.ImageData = new List<string>(RotateCounterClockWise(bottomNext.ImageData));
                                bottomNext = CalculateEdges(bottomNext);
                            }
                            else
                            {
                                //TODO: implement other combinations
                                throw new NotImplementedException();
                            }

                            foundedBottom = true;
                            bottomNext.Done = true;
                            break;
                        }
                    }

                    if (foundedBottom)
                    {
                        break;
                    }
                }
            }

            if (!foundedLeft)
            {
                foreach (var topLeftTile in topLeft.NamedEdges)
                {
                    foreach (var nextLeftTile in nextLeft.NamedEdges)
                    {
                        if (topLeftTile.Value == nextLeftTile.Value)
                        {
                            if (topLeftTile.Key == "RightEdge" && nextLeftTile.Key == "LeftEdgeReverse")
                            {
                                //rotate just left one
                                nextLeft.ImageData.Reverse();
                                nextLeft = CalculateEdges(nextLeft);
                            }
                            else if (topLeftTile.Key == "RightEdge" && nextLeftTile.Key == "BottomEdgeReverse")
                            {
                                List<string> resversed = ReverseAllData(nextLeft.ImageData);
                                List<string> newDoirection = RotateClockWise(resversed, 9);
                                newDoirection.Reverse();
                                nextLeft.ImageData = new List<string>(newDoirection);
                                nextLeft = CalculateEdges(nextLeft);
                            }
                            else
                            {
                                //TODO: implement other combinations
                                throw new NotImplementedException($"Not implemented combination {topLeftTile.Key} : {nextLeftTile.Key}");
                            }

                            nextLeft.Done = true;
                            foundedLeft = true;
                            break;
                        }
                    }

                    if (foundedLeft)
                    {
                        break;
                    }
                }
            }

            tiles = FlipOtherTiles(tiles);

            return tiles;
        }

        private static List<Tile> FlipOtherTiles(List<Tile> tiles)
        {
            int rowIndex = 0;
            int columnIndex = 1;
            while (true)
            {
                Tile topLeft = tiles.Where(x => x.X == columnIndex && x.Y == rowIndex).FirstOrDefault();
                Tile nextLeft = tiles.Where(x => x.X == columnIndex + 1 && x.Y == rowIndex).FirstOrDefault();
                Tile bottomNext = tiles.Where(x => x.X == columnIndex && x.Y == rowIndex + 1).FirstOrDefault();

                if (nextLeft != null && !nextLeft.Done)
                {
                    foreach (var nextLeftTile in nextLeft.NamedEdges)
                    {
                        if (nextLeftTile.Value == topLeft.NamedEdges["RightEdge"])
                        {
                            if (nextLeftTile.Key == "LeftEdge")
                            {
                                // no need to rotate
                            }
                            else if (nextLeftTile.Key == "LeftEdgeReverse")
                            {
                                nextLeft.ImageData.Reverse();
                                nextLeft = CalculateEdges(nextLeft);
                            }
                            else if (nextLeftTile.Key == "BottomEdge")
                            {
                                List<string> newDoirection = RotateClockWise(ReverseAllData(nextLeft.ImageData), 9);
                                nextLeft.ImageData = new List<string>(newDoirection);
                                nextLeft = CalculateEdges(nextLeft);
                            }
                            else if (nextLeftTile.Key == "BottomEdgeReverse")
                            {
                                nextLeft.ImageData = new List<string>(RotateClockWise(nextLeft.ImageData, 9));
                                nextLeft = CalculateEdges(nextLeft);
                            }
                            else if (nextLeftTile.Key == "UpperEdge")
                            {
                                nextLeft.ImageData = new List<string>(RotateCounterClockWise(nextLeft.ImageData));
                                nextLeft = CalculateEdges(nextLeft);
                            }
                            else if (nextLeftTile.Key == "UpperEdgeReverse")
                            {
                                nextLeft.ImageData = new List<string>(RotateCounterClockWise(ReverseAllData(nextLeft.ImageData)));
                                nextLeft = CalculateEdges(nextLeft);
                            }
                            else if (nextLeftTile.Key == "RightEdge")
                            {
                                nextLeft.ImageData = ReverseAllData(nextLeft.ImageData);
                                nextLeft = CalculateEdges(nextLeft);
                            }
                            else if (nextLeftTile.Key == "RightEdgeReverse")
                            {
                                nextLeft.ImageData.Reverse();
                                nextLeft.ImageData = ReverseAllData(nextLeft.ImageData);
                                nextLeft = CalculateEdges(nextLeft);
                            }

                            nextLeft.Done = true;
                            break;
                        }
                    }
                }

                foreach (var egde in bottomNext.NamedEdges)
                {
                    if (egde.Value == topLeft.NamedEdges["BottomEdge"])
                    {
                        if (egde.Key == "BottomEdge")
                        {
                            // need to rotate
                            bottomNext.ImageData.Reverse();
                            bottomNext = CalculateEdges(bottomNext);
                        }
                        else if (egde.Key == "BottomEdgeReverse")
                        {
                            bottomNext.ImageData = new List<string>(ReverseAllData(bottomNext.ImageData));
                            bottomNext.ImageData.Reverse();
                            bottomNext = CalculateEdges(bottomNext);
                        }
                        else if (egde.Key == "RightEdge")
                        {
                            bottomNext.ImageData = new List<string>(ReverseAllData(RotateClockWise(bottomNext.ImageData, 9)));
                            bottomNext = CalculateEdges(bottomNext);
                        }
                        else if (egde.Key == "RightEdgeReverse")
                        {
                            bottomNext.ImageData = new List<string>(ReverseAllData(RotateCounterClockWise(bottomNext.ImageData)));
                            bottomNext.ImageData.Reverse();
                            bottomNext = CalculateEdges(bottomNext);
                        }
                        else if (egde.Key == "UpperEdge")
                        {
                            //no need to do anything
                        }
                        else if (egde.Key == "UpperEdgeReverse")
                        {
                            bottomNext.ImageData = new List<string>(ReverseAllData(bottomNext.ImageData));
                            bottomNext = CalculateEdges(bottomNext);
                        }
                        else if (egde.Key == "LeftEdgeReverse")
                        {
                            bottomNext.ImageData = new List<string>(ReverseAllData(RotateCounterClockWise(bottomNext.ImageData)));
                            bottomNext = CalculateEdges(bottomNext);
                        }
                        else if (egde.Key == "LeftEdge")
                        {
                            bottomNext.ImageData = new List<string>(RotateCounterClockWise(bottomNext.ImageData));
                            bottomNext = CalculateEdges(bottomNext);
                        }

                        bottomNext.Done = true;
                        break;
                    }
                }

                if (nextLeft == null)
                {
                    rowIndex += 1;
                    columnIndex = 0;
                }
                else
                {
                    columnIndex += 1;
                }

                if (tiles.All(x => x.Done)) 
                {
                    break;
                }
            }

            return tiles;
        }

        private static List<string> RotateClockWise(List<string> imageData, int imageSize)
        {
            List<string> newDirection = new List<string>();
            for (int i = imageSize; i >= 0; i--)
            {
                string newString = string.Empty;
                for (int j = imageSize; j >= 0; j--)
                {
                    newString += imageData[j][i].ToString();
                }

                newDirection.Add(newString);
            }

            return newDirection;
        }

        private static List<string> ReverseAllData(List<string> imageData)
        {
            List<string> reversedData = new List<string>();
            foreach (string toRevers in imageData)
            {
                reversedData.Add(ReverseString(toRevers));
            }

            return reversedData;
        }

        private static List<string> RotateCounterClockWise(List<string> imageData)
        {
            List<string> newDirection = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                string newString = string.Empty;
                for (int j = 0; j < 10; j++)
                {
                    newString += imageData[j][i].ToString();
                }

                newDirection.Add(newString);
            }

            return newDirection;
        }

        private static List<Tile> SetTilesPositions(List<Tile> tiles)
        {
            Dictionary<int, int> positions = new Dictionary<int, int>();
            List<string> checkedCombinations = new List<string>();
            Dictionary<int, List<int>> mapping = new Dictionary<int, List<int>>();

            for (int i = 0; i < tiles.Count; i++)
            {
                for (int j = 0; j < tiles.Count; j++)
                {
                    Tile t1 = tiles[i];
                    Tile t2 = tiles[j];
                    if (t1.ID == t2.ID)
                    {
                        continue;
                    }

                    if (t1.Edges.Where(x => t2.Edges.Any(c => c == x)).Count() > 0)
                    {
                        if (checkedCombinations.Contains($"{t1.ID}{t2.ID}"))
                        {
                            continue;
                        }

                        checkedCombinations.Add($"{t1.ID}{t2.ID}");

                        if (checkedCombinations.Contains($"{t2.ID}{t1.ID}"))
                        {
                            continue;
                        }

                        checkedCombinations.Add($"{t2.ID}{t1.ID}");

                        if (positions.ContainsKey(t1.ID))
                        {
                            positions[t1.ID]++;
                        }
                        else
                        {
                            positions.Add(t1.ID, 1);
                        }

                        if (positions.ContainsKey(t2.ID))
                        {
                            positions[t2.ID]++;
                        }
                        else
                        {
                            positions.Add(t2.ID, 1);
                        }

                        if (mapping.ContainsKey(t1.ID))
                        {
                            mapping[t1.ID].Add(t2.ID);
                        }
                        else
                        {
                            mapping.Add(t1.ID, new List<int>() { t2.ID });
                        }

                        if (mapping.ContainsKey(t2.ID))
                        {
                            mapping[t2.ID].Add(t1.ID);
                        }
                        else
                        {
                            mapping.Add(t2.ID, new List<int>() { t1.ID });
                        }
                    }
                }
            }

            int startKey = positions.Where(x => x.Value == 2).Select(x => x.Key).First();
            Tile pivotTile = tiles.Where(x => x.ID == startKey).FirstOrDefault();
            pivotTile.X = 0;
            pivotTile.Y = 0;

            int row = 0;
            while (true)
            {
                foreach (var item in mapping[startKey])
                {
                    Tile currentTile = tiles.Where(x => x.ID == item).FirstOrDefault();

                    if (currentTile.X == null)
                    {
                        if (mapping[currentTile.ID].Count > 3)
                        {
                            currentTile.X = pivotTile.X;
                            currentTile.Y = pivotTile.Y + 1;
                        }
                        else
                        {
                            //egdes
                            if (mapping[startKey].Count == 1)
                            {
                                currentTile.X = pivotTile.X;
                                currentTile.Y = pivotTile.Y + 1;
                            }
                            else
                            {
                                Tile tempTile = tiles.Where(x => x.X == pivotTile.X + 1 && x.Y == pivotTile.Y).FirstOrDefault();
                                if (tempTile == null)
                                {
                                    currentTile.X = pivotTile.X + 1;
                                    currentTile.Y = pivotTile.Y;
                                }
                                else
                                {
                                    currentTile.X = pivotTile.X;
                                    currentTile.Y = pivotTile.Y + 1;
                                }
                            }
                        }
                    }

                    mapping[currentTile.ID].Remove(startKey);
                }

                pivotTile = tiles.Where(x => x.X == pivotTile.X + 1 && x.Y == row).FirstOrDefault();
                if (pivotTile == null)
                {
                    row++;
                    pivotTile = tiles.Where(x => x.X == 0 && x.Y == row).FirstOrDefault();
                }

                startKey = pivotTile.ID;
                if (tiles.All(x => x.X != null && x.Y != null))
                    break;
            }

            return tiles;
        }

        private static int CountMonsers(List<string> newDoirection, string[] monster)
        {
            var monstCol = monster[0].Length;
            var monstRow = monster.Length;

            int monterCounter = 0;
            for (int row = 0; row < newDoirection.Count - monstRow; row++)
            {
                for (int col = 0; col < newDoirection.Count - monstCol; col++)
                {
                    if (IsMonsterFound(newDoirection, monster, monstCol, monstRow, row, col))
                    {
                        monterCounter++;
                    }
                }
            }

            return monterCounter;
        }

        private static bool IsMonsterFound(List<string> newDoirection, string[] monster, int monstCol, int monstRow, int row, int col)
        {
            for (var columnIndex = 0; columnIndex < monstCol; columnIndex++)
            {
                for (var rowIndex = 0; rowIndex < monstRow; rowIndex++)
                {
                    if (monster[rowIndex][columnIndex] == '#' && newDoirection[row + rowIndex][col + columnIndex] != '#')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static List<string> MergeMap(List<Tile> tiles)
        {
            int maxY = (int)tiles.Select(x => x.Y).Max();
            int maxX = (int)tiles.Select(x => x.X).Max();

            List<string> result = new List<string>();
            for (int i = 0; i < maxX + 1; i++)
            {
                List<string> tempResult = new List<string>();
                for (int j = 0; j < maxY + 1; j++)
                {
                    Tile t = tiles.Where(x => x.X == j && x.Y == i).FirstOrDefault();
                    if (tempResult.Count == 0)
                    {
                        for (int offset = 0; offset < t.ImageData.Count; offset++)
                        {
                            if (offset == 0 || offset == t.ImageData.Count - 1)
                            {
                                continue;
                            }

                            string newString = String.Join("", t.ImageData[offset].ToCharArray().Skip(1).SkipLast(1));
                            tempResult.Add(newString);
                        }
                    }
                    else
                    {
                        List<string> tempList = new List<string>();
                        for (int offset = 0; offset < t.ImageData.Count; offset++)
                        {
                            if (offset == 0 || offset == t.ImageData.Count - 1)
                            {
                                continue;
                            }

                            string newString = String.Join("", t.ImageData[offset].ToCharArray().Skip(1).SkipLast(1));
                            string tmp = tempResult[offset - 1] + newString;
                            tempList.Add(tmp);
                        }

                        tempResult = new List<string>(tempList);
                    }
                }

                result.AddRange(tempResult);
            }

            return result;
        }
    }

    class Tile 
    {
        public int ID { get; set; }
        public List<string> ImageData { get; set; }
        public List<string> Edges { get; set; }
        public Dictionary<string, string> NamedEdges { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public bool Done { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, ({X},{Y}), Done: {Done}";
        }
    }
}
