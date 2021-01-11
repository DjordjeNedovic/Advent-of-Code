using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_24
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("########## Day 24 2020 ##########");
            List<Tile> startingBlackTiles = SolvePartOne(input);
            Console.WriteLine($"Part one solution: {startingBlackTiles.Where(x => x.Fliped % 2 == 1).Count()}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(startingBlackTiles.Where(x => x.Fliped % 2 == 1).ToList())}");
            Console.WriteLine("#################################");
        }

        private static List<Tile> SolvePartOne(string[] input)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (string line in input)
            {
                Tile tile = GetStepstToTile(line);
                var tt = tiles.Find(x => x.X == tile.X && x.Y == tile.Y);
                if (tt == null)
                {
                    tiles.Add(tile);
                }
                else 
                {
                    tt.Fliped += 1;
                }
            }

            return tiles;
        }

        private static Tile GetStepstToTile(string line)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < line.Length; i++)
            {
                char curretChar = line[i];
                char nextChar = line[i];
                if((i+1)<line.Length)
                    nextChar = line[i + 1];

                if (curretChar == 'e')
                {
                    x -= 2;
                }
                else if (curretChar == 'w')
                {
                    x += 2;
                }
                else if (curretChar == 's' && nextChar == 'e')
                {
                    i += 1;

                    x -= 1;
                    y -= 1;

                }
                else if (curretChar == 's' && nextChar == 'w')
                {
                    i += 1;

                    x += 1;
                    y -= 1;

                }
                else if (curretChar == 'n' && nextChar == 'e')
                {
                    i += 1;

                    x -= 1;
                    y += 1;

                }
                else if (curretChar == 'n' && nextChar == 'w')
                {
                    i += 1;
                    x += 1;
                    y += 1;
                }
                else 
                {
                    throw new NotImplementedException();
                }
            }

            return new Tile() { X = x, Y = y, Fliped = 1 };
        }

        private static int SolvePartTwo(List<Tile> list)
        {
            var currentBlackTiles = list;
            for (int i = 0; i < 100; i++) 
            {
                currentBlackTiles = NextDayFlip(currentBlackTiles);
            }

            return currentBlackTiles.Count;
        }

        private static List<Tile> NextDayFlip(List<Tile> list)
        {
            List<Tile> result = new List<Tile>();
            var currentBlackTiles = list.ToHashSet();
            var tilesToCheck = new HashSet<Tile>();
            foreach (var blackTile in list) 
            {
                tilesToCheck.Add(blackTile);
                List<Tile> adjacentTiles = GetAdjacentTiles(blackTile);
                foreach (var adjTile in adjacentTiles)
                {
                    if (!tilesToCheck.Contains(adjTile))
                    {
                        tilesToCheck.Add(adjTile);
                    }
                }
            }

            foreach (var tile in tilesToCheck) 
            {
                var isCurrentlyBlack = currentBlackTiles.Contains(tile);
                var numberOfAdjacentBlackTiles = GetAdjacentTiles(tile)
                                                .Where(tile => currentBlackTiles.Contains(tile))
                                                .Count();

                if (isCurrentlyBlack && (numberOfAdjacentBlackTiles == 1 || numberOfAdjacentBlackTiles == 2))
                {
                    result.Add(tile);
                }
                else if (!isCurrentlyBlack && numberOfAdjacentBlackTiles == 2) 
                {
                    result.Add(tile);
                }
            }

            return result;
        }

        private static List<Tile> GetAdjacentTiles(Tile blackTile)
        {
            return new List<Tile>() 
            {
                new Tile(){ X = blackTile.X + 2, Y = blackTile.Y },
                new Tile(){ X = blackTile.X - 2, Y = blackTile.Y },
                new Tile(){ X = blackTile.X - 1, Y = blackTile.Y - 1 },
                new Tile(){ X = blackTile.X + 1, Y = blackTile.Y - 1 },
                new Tile(){ X = blackTile.X - 1, Y = blackTile.Y + 1},
                new Tile(){ X = blackTile.X + 1, Y = blackTile.Y + 1 },
            };
        }
    }

    class Tile 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Fliped { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Tile p = (Tile)obj;
                return (X == p.X) && (Y == p.Y);
            }
        }

        public override int GetHashCode()
        {
            var tuple = Tuple.Create(X, Y);
            int hash = tuple.GetHashCode();
            return hash;
        }
    }
}