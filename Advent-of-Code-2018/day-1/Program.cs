using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 1 2018 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(string[] inputs)
        {
            string regex = @"(\+|\-)(?<value>\d+)";

            int count = 0;
            foreach (string input in inputs)
            {
                Match match = Regex.Match(input, regex);
                string sign = match.Groups[1].Value;
                int neededSize = Int32.Parse(match.Groups["value"].Value);

                neededSize = sign == "+" ? neededSize : -neededSize;
                count += neededSize;
            }

            return count;
        }

        private static int SolvePartTwo(string[] inputs)
        {
            string regex = @"(\+|\-)(?<value>\d+)";
            int count = 0;

            List<int> results = new List<int>();
            bool shoulBreak = false;
            while (true) 
            {
                for (int i = 0; i < inputs.Length; i++) 
                {
                    Match match = Regex.Match(inputs[i], regex);
                    string sign = match.Groups[1].Value;
                    int neededSize = Int32.Parse(match.Groups["value"].Value);

                    neededSize = sign == "+" ? neededSize : -neededSize;
                    count += neededSize;
                    if (results.Contains(count)) 
                    {
                        shoulBreak = true;
                        break;
                    }

                    results.Add(count);
                }

                if (shoulBreak) 
                {
                    break;
                }
            }

            return count;
        }
    }
}
