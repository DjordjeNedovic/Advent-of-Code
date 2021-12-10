using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("########## Day 6 2018 ##########");
            //Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(string[] input)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            foreach (string line in input)
            {
                coordinates.Add(new Coordinate() { X= Int32.Parse(line.Split(',')[0].Trim()), Y = Int32.Parse(line.Split(',')[1].Trim()) });
            }

            int maxX = coordinates.Max(x => x.X);
            int maxY = coordinates.Max(x => x.Y);

            List<Coordinate> mutualCoordinates = new List<Coordinate>();

            Dictionary<(int,int), int> pairs = new Dictionary<(int,int), int>();
            Dictionary<(int,int), int> count = new Dictionary<(int,int), int>();

            for (int x = 0; x <= maxX + 1; x++) 
            {
                for (int y = 0; y <= maxY + 1; y++) 
                {
                    var distances = coordinates
                                        .Select((c, i) => (i, dist: Math.Abs(c.X - x) + Math.Abs(c.Y - y)))
                                        .OrderBy(c => c.dist)
                                        .ToArray();

                    if (distances[1].dist != distances[0].dist) 
                    {
                        if (x != 0 && y != 0 && x != maxX && y != maxY)
                        {
                            coordinates.ElementAt(distances[0].i).Count++;
                        }
                        else 
                        {
                            coordinates.ElementAt(distances[0].i).IsInfinity = true;
                        }
                    }
                }
            }

            return coordinates.Where(x => x.IsInfinity == false).OrderByDescending(x => x.Count).ToList().First().Count;
        }

        private static object SolvePartTwo(string[] input)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            foreach (string line in input)
            {
                coordinates.Add(new Coordinate() { X = Int32.Parse(line.Split(',')[0].Trim()), Y = Int32.Parse(line.Split(',')[1].Trim()) });
            }

            int maxX = coordinates.Max(x => x.X);
            int maxY = coordinates.Max(x => x.Y);
            int minSum = 0;
            List<Coordinate> mutualCoordinates = new List<Coordinate>();

            Dictionary<(int, int), int> pairs = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> count = new Dictionary<(int, int), int>();

            for (int x = 0; x <= maxX + 1; x++)
            {
                for (int y = 0; y <= maxY + 1; y++)
                {
                    var distances = coordinates
                                        .Select((c, i) => (i, dist: Math.Abs(c.X - x) + Math.Abs(c.Y - y)))
                                        .OrderBy(c => c.dist)
                                        .ToArray();

                    var sum = distances.Sum(x => x.dist);
                    if (sum < 10000)
                        minSum++;

                }
            }

            return minSum;
        }
    }

    class Coordinate 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Count { get; set; }
        public bool IsInfinity { get; set; }
        public int Test { get; set; }
        public override string ToString()
        {
            return $"{X} : {Y} - Count {Count}, IsInfinity {IsInfinity}, Test {Test}";
        }
    }
}
