using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_15
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            List<Tile> tilesPartOne = GetTilesForPartOne(input);
            Console.WriteLine("########## Day 15 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(tilesPartOne)}");
            Console.WriteLine("################################");
        }

        private static List<Tile> GetTilesForPartOne(string[] input)
        {
            List<Tile> tilesPartOne = new List<Tile>();

            for (int i = 0; i < input.Length; i++)
            {
                char[] riskLevels = input[i].ToCharArray();
                for (int j = 0; j < riskLevels.Length; j++)
                {
                    tilesPartOne.Add(new Tile() { X = i, Y = j, Risk = Int32.Parse(riskLevels[j].ToString()) });
                }
            }

            return tilesPartOne;
        }

        private static int SolvePartOne(List<Tile> tiles)
        {
            Tile firstNode = tiles.First(x => x.X == 0 && x.Y == 0);
            firstNode.Distance = 0;
            Queue<Tile> q = new Queue<Tile>();
            q.Enqueue(firstNode);
            Tile destinationNode = tiles.First(x => x.X == tiles.Max(x=>x.X) && x.Y == tiles.Max(x => x.Y));
            while (q.Count > 0) 
            {
                Tile current = q.Dequeue();
                if (current.X == destinationNode.X && current.Y == destinationNode.Y) 
                {
                    break;
                }

                if (current.IsVisited == true) 
                {
                    continue;
                }

                current.IsVisited = true;
                List<Tile> tilesT = GetAdjustent(current.X, current.Y, tiles);
                foreach (var tt in tilesT) 
                {
                    if (tt == null)
                        continue;

                    var alt = current.Distance + tt.Risk;
                    if (alt < tt.Distance)
                    {
                        tt.Distance = alt;
                    }

                    if (tt.Distance != int.MaxValue)
                    {
                        q.Enqueue(tt);
                    }
                }
            }

            return destinationNode.Distance;
        }

        private static List<Tile> GetAdjustent(int x, int y, List<Tile> tiles)
        {
            List<Tile> t = new List<Tile>();
            t.Add(tiles.FirstOrDefault(tile => tile.X == (x + 1) && tile.Y == y));
            t.Add(tiles.FirstOrDefault(tile => tile.X == (x - 1) && tile.Y == y));
            t.Add(tiles.FirstOrDefault(tile => tile.X == (x) && tile.Y == y+1));
            t.Add(tiles.FirstOrDefault(tile => tile.X == (x) && tile.Y == y-1));

            return t;
        }
    }

    class Tile 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Risk { get; set; }
        public int Distance { get; set; } = Int32.MaxValue;
        public bool IsVisited { get; set; }

        public override string ToString()
        {
            return $"({X},{Y}) -> Risk:{Risk}, Distance: {Distance}, is visited: {IsVisited}";
        }
    }
}
