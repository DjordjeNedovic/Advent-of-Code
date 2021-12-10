using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_3
{
    class Program
    {
        static Dictionary<string, int> dictionary = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("########## Day 3 2018 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            Regex regex = new Regex(@"#[0-9]+ @ ([0-9]+),([0-9]+): ([0-9]+)x([0-9]+)");
            foreach (string line in input) 
            {
                MatchCollection mc = regex.Matches(line);
                var y = int.Parse(mc.First().Groups[1].Value);
                var x = int.Parse(mc.First().Groups[2].Value);
                var y1 = int.Parse(mc.First().Groups[3].Value);
                var x1 = int.Parse(mc.First().Groups[4].Value);

                for (int i = 0; i < y1; i++) 
                {
                    for (int j = 0; j < x1; j++) 
                    {
                        string key = $"({x+1+j},{y+1+i})";
                        if (dictionary.ContainsKey(key))
                        {
                            dictionary[key]++;
                        }
                        else 
                        {
                            dictionary.Add(key, 1);
                        }
                    }
                }
            }

            return dictionary.Where(x => x.Value >= 2).ToList().Count;
            
        }

        private static object SolvePartTwo(string[] input)
        {
            string result = "";
            Regex regex = new Regex(@"#([0-9]+) @ ([0-9]+),([0-9]+): ([0-9]+)x([0-9]+)");
            if (dictionary.Count == 0) 
            {
                SolvePartOne(input);
            }

            bool flag = true;

            foreach (string line in input)
            {
                flag = true;
                MatchCollection mc = regex.Matches(line);
                var y = int.Parse(mc.First().Groups[2].Value);
                var x = int.Parse(mc.First().Groups[3].Value);
                var y1 = int.Parse(mc.First().Groups[4].Value);
                var x1 = int.Parse(mc.First().Groups[5].Value);

                for (int i = 0; i < y1; i++)
                {
                    for (int j = 0; j < x1; j++)
                    {
                        string key = $"({x + 1 + j},{y + 1 + i})";
                        if (dictionary[key] >= 2)
                            flag = false;
                    }

                    if (!flag)
                        break;
                }

                if (flag) 
                {
                    result = mc.First().Groups[1].Value;
                    break;
                }
            }

            return result;
        }
    }
}
