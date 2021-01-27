using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_19
{
    public class Program
    {
        static List<string> vs = new List<string>();
        static Dictionary<int, string> simpleRules = new Dictionary<int, string>();
        static Dictionary<int, List<string>> complexRules = new Dictionary<int, List<string>>();

        static void Main(string[] args)
        {
            string input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 19 2020 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("#################################");
        }

        public static int SolvePartOne(string input)
        {
            string[] groups = input.Split("\r\n\r\n");
            List<string> rules = groups[0].Split("\r\n").ToList();

            foreach (string rule in rules) 
            {
                if (rule.Contains("\""))
                {
                    simpleRules.Add(int.Parse(rule.Split(":")[0]), rule.Split(":")[1].Trim().Replace("\"", ""));
                }
                else if (rule.Contains("|"))
                {
                    var t = rule.Split(":")[1].Trim().Split("|");
                    List<string> subrules = new List<string>();
                    foreach (var tt in t) 
                    {
                        subrules.Add(tt.Trim());
                    }
                    complexRules.Add(int.Parse(rule.Split(":")[0]), subrules);

                }
                else 
                {
                    complexRules.Add(int.Parse(rule.Split(":")[0]), new List<string>() { rule.Split(":")[1].Trim().Replace("\"", "") });
                }
            }


            List<string> comb = new List<string>();
            var gg = complexRules[0].First().Split(" ");
            foreach (var ggg in gg) 
            {
                if (simpleRules.ContainsKey(int.Parse(ggg)))
                {
                    if (comb.Count == 0)
                    {
                        comb.Add(simpleRules[int.Parse(ggg)].First().ToString());
                    }
                    else 
                    {
                        List<string> tmpLint = new List<string>();
                        foreach (var sh in comb)
                        {
                            tmpLint.Add(sh + simpleRules[int.Parse(ggg)].First().ToString());
                        }
                        comb = new List<string>(tmpLint);
                    }
                }
                else 
                {
                    List<string> comb2 = Recursive(int.Parse(ggg));
                    if (comb.Count == 0)
                    {
                        comb.AddRange(comb2);
                    }
                    else 
                    {
                        List<string> tmpLint = new List<string>();
                        foreach (var sh in comb)
                        {
                            foreach (var shs in comb2)
                            {
                                tmpLint.Add(sh + shs);
                            }
                        }

                        comb = new List<string>(tmpLint);
                    }
                }
            }

            List<string> toCheck = groups[1].Split("\r\n").ToList();

            return comb.Intersect(toCheck).Count();
        }

        private static List<string> Recursive(int v)
        {
            List<string> comb = new List<string>();
            var gg = complexRules[v];
            foreach (var ggg in gg)
            {
                List<string> recursiveCombinations = new List<string>();
                var gggg = ggg.Split(" ");
                foreach (var final in gggg) 
                {
                    if (simpleRules.ContainsKey(int.Parse(final)))
                    {
                        if (recursiveCombinations.Count == 0)
                        {
                            recursiveCombinations.Add(simpleRules[int.Parse(final)].First().ToString());
                        }
                        else
                        {
                            List<string> tmpLint = new List<string>();
                            foreach (var sh in recursiveCombinations)
                            {
                                tmpLint.Add(sh + simpleRules[int.Parse(final)].First().ToString());
                            }
                            recursiveCombinations = new List<string>(tmpLint);
                        }
                    }
                    else
                    {
                        List<string> recursiveCombinationsPart = Recursive(int.Parse(final));
                        if (recursiveCombinations.Count == 0)
                        {
                            recursiveCombinations.AddRange(recursiveCombinationsPart);
                        }
                        else 
                        {
                            List<string> tmpLint = new List<string>();
                            foreach (var sh in recursiveCombinations) 
                            {
                                foreach (var shs in recursiveCombinationsPart) 
                                {
                                    tmpLint.Add(sh + shs);
                                }
                            }

                            recursiveCombinations = new List<string>(tmpLint);

                        }
                    }
                }

                comb.AddRange(recursiveCombinations);
            }

            return comb;
        }

        private static object SolvePartTwo(string input)
        {
            throw new NotImplementedException();
        }
    }
}
