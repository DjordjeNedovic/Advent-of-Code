using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_5
{
    class Program
    {
        private static Dictionary<Tuple<int, int>, int> coordinatePairs = new Dictionary<Tuple<int, int>, int>();
        private static string regex = @"(?<x1>[0-9]+?),(?<y1>[0-9]+?) -> (?<x2>[0-9]+?),(?<y2>[0-9]+.?)";

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 5 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            foreach (string line in input) 
            {
                MatchCollection m = Regex.Matches(line, regex);
                var x1 = Int32.Parse(m[0].Groups["x1"].Value);
                var y1 = Int32.Parse(m[0].Groups["y1"].Value);
                var x2 = Int32.Parse(m[0].Groups["x2"].Value);
                var y2 = Int32.Parse(m[0].Groups["y2"].Value);

                if (x1 == x2 || y1 == y2)
                {
                    FindHorizonatalAndVeritacal(x1, y1, x2, y2);
                }
            }

            return coordinatePairs.Values.Count(x => x > 1);
        }

        private static object SolvePartTwo(string[] input)
        {
            foreach (string line in input)
            {
                MatchCollection m = Regex.Matches(line, regex);
                var x1 = Int32.Parse(m[0].Groups["x1"].Value);
                var y1 = Int32.Parse(m[0].Groups["y1"].Value);
                var x2 = Int32.Parse(m[0].Groups["x2"].Value);
                var y2 = Int32.Parse(m[0].Groups["y2"].Value);

                if (x1 == x2 || y1 == y2)
                {
                    continue;
                }
                else 
                {
                    if ((x1 > x2 && y1 > y2) || (x1 < x2 && y1 <y2)) 
                    {
                        if (x1 < x2)
                        {
                            int tempA = x1;
                            x1 = x2;
                            x2 = tempA;
                        }

                        if (y1 < y2)
                        {
                            int tempB = y1;
                            y1 = y2;
                            y2 = tempB;
                        }

                        int deltaX = Math.Abs(x1 - x2);
                        int deltaY = Math.Abs(y1 - y2);
                        for (int i = 0; i < Math.Abs(deltaY) + 1; i++)
                        {
                            int newX = Math.Abs(x1 - deltaX + i);
                            int newY = Math.Abs(y1 - deltaY + i);
                            InsertPair(newX, newY);
                        }
                    }
                    else 
                    {
                        if (x1 > x2)
                        {
                            int tempA = x1;
                            x1 = x2;
                            x2 = tempA;

                            int tempB = y1;
                            y1 = y2;
                            y2 = tempB;
                        }

                        int deltaY = Math.Abs(y1 - y2);
                        for (int i = 0; i < Math.Abs(deltaY) + 1; i++)
                        {
                            int newX = x1 + i;
                            int newY = y1 - i;

                            InsertPair(newX, newY);
                        }
                    }
                }
            }

            return coordinatePairs.Values.Count(x => x > 1);
        }

        private static void FindHorizonatalAndVeritacal(int x1, int y1, int x2, int y2)
        {
            if (x1 > x2)
            {
                int tempA = x1;
                x1 = x2;
                x2 = tempA;
            }

            if (y1 > y2)
            {
                int tempB = y1;
                y1 = y2;
                y2 = tempB;
            }

            for (int deltaY = y1; deltaY <= y2; deltaY++)
            {
                for (int deltaX = x1; deltaX <= x2; deltaX++)
                {
                    InsertPair(deltaX, deltaY);

                }
            }
        }

        private static void InsertPair(int newX, int newY)
        {
            Tuple<int, int> temp = new Tuple<int, int>(newX, newY);

            if (coordinatePairs.ContainsKey(temp))
            {
                coordinatePairs[temp]++;
            }
            else
            {
                coordinatePairs.Add(temp, 1);
            }
        }
    }
}