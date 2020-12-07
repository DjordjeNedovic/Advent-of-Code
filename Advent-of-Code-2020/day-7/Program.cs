using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_7
{
    class Program
    {
        static List<string> final = new List<string>();
        static void Main(string[] args)
        {
            string[] input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Split(Environment.NewLine);
            Dictionary<string, List<string>> bagsRecepies = ParseBags(input);
            PartOne(bagsRecepies);
            Console.WriteLine($"Part one solution:  {final.Count}");
            Console.WriteLine($"Part two solution:  {PartTwo(bagsRecepies)}");
        }

        private static Dictionary<string, List<string>> ParseBags(string[] input)
        {
            Dictionary<string, List<string>> bagsRecepies = new Dictionary<string, List<string>>();
            foreach (string b in input)
            {
                string[] bagsContent = b.Split(" bags contain ");
                List<string> bagsChilds = bagsContent[1].Split(",").Select(x => x.Replace("bags.", "").Replace("bag.", "").Replace("bags", "").Replace("bag", "").Trim()).ToList();
                if (!bagsRecepies.ContainsKey(bagsContent[0]))
                {
                    bagsRecepies.Add(bagsContent[0], bagsChilds);
                }
                else
                {
                    bagsRecepies[bagsContent[0]].AddRange(bagsChilds);
                }
            }

            return bagsRecepies.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        private static int PartTwo(Dictionary<string, List<string>> bagsRecepies)
        {
            string regex = @"(?<value>\d) (?<name>\w+ \w+)";
            int result = 0;
            var bags = bagsRecepies["shiny gold"];
            foreach (var bag in bags) 
            {
                Match match = Regex.Match(bag, regex);
                int count = Int32.Parse(match.Groups["value"].Value);
                string bagName = match.Groups["name"].Value;
                int bagsNumber = RecursionForPartTwo(bagName, bagsRecepies, regex);
                if (bagsNumber == 1)
                {
                    result += count * bagsNumber;
                }
                else 
                {
                    result += count + count * bagsNumber;
                }
            }

            return result;
        }

        private static int RecursionForPartTwo(string bagName, Dictionary<string, List<string>> bagsRecepies, string regex)
        {
            int result = 0;
            var bags = bagsRecepies[bagName];
            foreach (var bag in bags)
            {
                if (bag.Contains("no other")) 
                {
                    return 1;
                }

                Match match = Regex.Match(bag, regex);
                int count = Int32.Parse(match.Groups["value"].Value);
                string bagName2 = match.Groups["name"].Value;
                int child = RecursionForPartTwo(bagName2, bagsRecepies, regex);
                if (child == 1)
                {
                    result += count * child;
                }
                else 
                {
                    result += count + count * child;
                }
            }

            return result;
        }

        private static void PartOne(Dictionary<string, List<string>> bagsRecepies)
        {
            var childBags = new List<string>();
            foreach (KeyValuePair<string, List<string>> recepie in bagsRecepies)
            {
                if (recepie.Value.Any(bag => bag.Contains("no other")))
                {
                    continue;
                }

                if (recepie.Value.Any(bag => bag.Contains("shiny gold")))
                {
                    childBags.Add(recepie.Key);
                    final.Add(recepie.Key);
                }
            }

            Recursion(childBags, bagsRecepies);
        }

        private static void Recursion(List<string> childBags, Dictionary<string, List<string>> allBags)
        {
            List<string> dependentBags = new List<string>();
            foreach (string needBag in childBags) 
            {
                foreach (KeyValuePair<string, List<string>> bag in allBags)
                {
                    if (bag.Value.Any(x => x.Contains(needBag)))
                    {
                        dependentBags.Add(bag.Key);
                        final.Add(bag.Key);
                    }
                }
            }

            if (dependentBags.Count != 0)
            {
                dependentBags = dependentBags.Distinct().ToList();
                final = final.Distinct().ToList();
                Recursion(dependentBags, allBags);
            }
        }
    }
}
