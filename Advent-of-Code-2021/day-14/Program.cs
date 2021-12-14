using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            string polymerTemplate = input[0];
            Dictionary<string, string> pairInsertion = PopulatePairs(input);

            Console.WriteLine("########## Day 14 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(polymerTemplate, pairInsertion, 10)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(polymerTemplate, pairInsertion, 40)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string polymerTemplate, Dictionary<string, string> pairInsertion, int iterationNumer)
        {
            for (int iteration = 0; iteration < iterationNumer; iteration++)
            {
                char[] polimersInTemplate = polymerTemplate.ToCharArray();
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i < polymerTemplate.Length; i++)
                {
                    char polimerA = polimersInTemplate[i - 1];
                    char polimerB = polimersInTemplate[i];

                    string middlePolimer = pairInsertion[polimerA.ToString() + polimerB.ToString()];

                    if(sb.Length==0)
                        sb.Append(polimerA);
                    
                    sb.Append(middlePolimer);
                    sb.Append(polimerB);
                }

                polymerTemplate = sb.ToString();
            }

            var resslut = polymerTemplate.GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).ToList();

            var min = resslut.OrderBy(x => x.Count).First();
            var max = resslut.OrderBy(x => x.Count).Last();

            return max.Count - min.Count;
        }

        private static object SolvePartTwo(string polymerTemplate, Dictionary<string, string> pairInsertion, int iterationNumer)
        {
            Dictionary<char, long> charCount = polymerTemplate.GroupBy(x => x).ToDictionary(y => y.Key, y => (long)y.Count());
            Dictionary<string, long> usedItems = Pairs(polymerTemplate);

            for (int iteration = 0; iteration < iterationNumer; iteration++)
            {
                var newPairs = new Dictionary<string, long>();
                foreach (var pair in usedItems)
                {
                    var middlePolimer = pairInsertion[pair.Key];
                    var newPair1 = pair.Key.ToString()[0] + middlePolimer;
                    var newPair2 = middlePolimer + pair.Key.ToString()[1];

                    if (!charCount.ContainsKey(char.Parse(middlePolimer)))
                    {
                        charCount.Add(char.Parse(middlePolimer), 1);
                    }
                    else
                    {
                        charCount[char.Parse(middlePolimer)] += pair.Value;
                    }

                    if (newPairs.ContainsKey(newPair1))
                    {
                        newPairs[newPair1] += pair.Value;
                    }
                    else
                    {
                        newPairs.Add(newPair1, pair.Value);
                    }

                    if (newPairs.ContainsKey(newPair2))
                    {
                        newPairs[newPair2] += pair.Value;
                    }
                    else
                    {
                        newPairs.Add(newPair2, pair.Value);
                    }
                }

                usedItems = new Dictionary<string, long>(newPairs);
            }


            var min = charCount.Values.Min();
            var max = charCount.Values.Max();
            return max - min;
        }

        private static Dictionary<string, long> Pairs(string polymerTemplate)
        {
            Dictionary<string, long> pairs = new Dictionary<string, long>();
            char[] polimersInTemplate = polymerTemplate.ToCharArray();
            for (int i = 1; i < polymerTemplate.Length; i++)
            {
                char polimerA = polimersInTemplate[i - 1];
                char polimerB = polimersInTemplate[i];

                if (pairs.ContainsKey(polimerA.ToString() + polimerB.ToString()))
                {
                    pairs[polimerA.ToString() + polimerB.ToString()] += 1;
                }
                else
                {
                    pairs.Add(polimerA.ToString() + polimerB.ToString(), 1);
                }
            }

            return pairs;
        }

        private static Dictionary<string, string> PopulatePairs(string[] input)
        {
            string regex = @"(\w+) -> (\w+)";
            Dictionary<string, string> pairInsertion = new Dictionary<string, string>();
            for (int i = 2; i < input.Length; i++)
            {
                Match m = Regex.Match(input[i], regex);
                pairInsertion.Add(m.Groups[1].Value, m.Groups[2].Value);
            }

            return pairInsertion;
        }
    }
}
