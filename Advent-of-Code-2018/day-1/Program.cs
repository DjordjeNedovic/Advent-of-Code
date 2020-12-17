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
            string[] inputs = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine(SolvePartOne(inputs));
            Console.WriteLine(SolvePartTwo(inputs));
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
                //Console.WriteLine($"{neededSize},  {count += neededSize}");
                count += neededSize;
            }

            return count;
        }

        private static int SolvePartTwo(string[] inputs)
        {
            string regex = @"(\+|\-)(?<value>\d+)";
            int count = 0;

            List<int> results = new List<int>();
            bool flag = false;
            while (true) 
            {
                int ii = 0;
                for (int i = ii; i < inputs.Length; i++) 
                {
                    Match match = Regex.Match(inputs[i], regex);
                    string sign = match.Groups[1].Value;
                    int neededSize = Int32.Parse(match.Groups["value"].Value);

                    neededSize = sign == "+" ? neededSize : -neededSize;
                    //Console.WriteLine($"{neededSize},  {count += neededSize}");
                    count += neededSize;

                    if (results.Contains(count)) 
                    {
                        flag = true;
                        break;
                    }

                    results.Add(count);
                }

                if (flag) 
                {
                    break;
                }
            }

            return count;
        }
    }
}
