using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_13
{
    class Program
    {
        private static int firstFoldDotNumbers;
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 13 2021 ##########");
            HashSet <Tuple<int, int>> dots = SolvePartOne(input);
            Console.WriteLine($"Part one solution: {firstFoldDotNumbers}");
            Console.WriteLine($"Part two solution:");
            SolvePartTwo(dots);
            Console.WriteLine("################################");
        }

        private static HashSet<Tuple<int, int>> SolvePartOne(string[] input)
        {
            List<string> foldInstruction = new List<string>();
            HashSet<Tuple<int, int>> coordinates = new HashSet<Tuple<int, int>>();
            foreach (string line in input) 
            {
                if (line.StartsWith("fold"))
                {
                    foldInstruction.Add(line);
                    continue;
                }

                if (line == "")
                    continue;

                string[] xy = line.Split(',');
                coordinates.Add(new Tuple<int, int>(Int32.Parse(xy[0]), Int32.Parse(xy[1])));
            }

            coordinates = Fold(foldInstruction, coordinates);

            return coordinates;
        }

        private static HashSet<Tuple<int, int>> Fold(List<string> foldInstruction, HashSet<Tuple<int, int>> coordinates)
        {
            coordinates = coordinates.OrderBy(x => x.Item1).ThenBy(x => x.Item2).ToHashSet();
            string regex = @"fold along (?<asix>x|y)=(?<value>\d+)";
            foreach (string instruction in foldInstruction) 
            {
                HashSet<Tuple<int, int>> changed = new HashSet<Tuple<int, int>>();
                Match match = Regex.Match(instruction, regex);
                string asix = match.Groups["asix"].Value;
                int value = Int32.Parse(match.Groups["value"].Value);
                if (asix == "x")
                {
                    coordinates.Where(x => x.Item1 > value).ToList().ForEach(x => changed.Add(new Tuple<int, int>(value - (x.Item1 - value), x.Item2)));
                    coordinates.Where(x => x.Item1 < value).ToList().ForEach(x => changed.Add(x));
                }
                else 
                {
                    coordinates.Where(x => x.Item2 > value).ToList().ForEach(x => changed.Add(new Tuple<int, int>(x.Item1, value - (x.Item2- value))));
                    coordinates.Where(x => x.Item2 < value).ToList().ForEach(x => changed.Add(x));
                }

                coordinates = changed;
                if (firstFoldDotNumbers == 0) 
                {
                    firstFoldDotNumbers = coordinates.Count;
                }
            }

            return coordinates;
        }

        private static void SolvePartTwo(HashSet<Tuple<int, int>> input)
        {
            Print(input);
        }

        private static void Print(HashSet<Tuple<int, int>> coordinates)
        {
            for (int row = 0; row <= coordinates.Max(x => x.Item2); row++)
            {
                for (int collumn = 0; collumn <= coordinates.Max(x => x.Item1); collumn++)
                {
                    if (coordinates.Any(x => x.Item1 == collumn && x.Item2 == row))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.Write(Environment.NewLine);
            }
        }
    }
}
