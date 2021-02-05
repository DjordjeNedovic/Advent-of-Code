using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_19
{
    public class Program
    {
        static List<string> vs = new List<string>();
        static Dictionary<int, string> simpleRules;
        static Dictionary<int, List<string>> complexRules;

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
            complexRules = new Dictionary<int, List<string>>();
            simpleRules = new Dictionary<int, string>();
            foreach (string rule in rules) 
            {
                if (rule.Contains("\""))
                {
                    simpleRules.Add(int.Parse(rule.Split(":")[0]), rule.Split(":")[1].Trim().Replace("\"", ""));
                }
                else if (rule.Contains("|"))
                {
                    var orGroups = rule.Split(":")[1].Trim().Split("|");
                    List<string> subRules = new List<string>();
                    foreach (var orGroup in orGroups) 
                    {
                        subRules.Add(orGroup.Trim());
                    }

                    complexRules.Add(int.Parse(rule.Split(":")[0]), subRules);

                }
                else 
                {
                    complexRules.Add(int.Parse(rule.Split(":")[0]), new List<string>() { rule.Split(":")[1].Trim().Replace("\"", "") });
                }
            }


            List<string> allCombinations = new List<string>();
            var startingRules = complexRules[0].First().Split(" ");
            foreach (var startingRule in startingRules) 
            {
                if (simpleRules.ContainsKey(int.Parse(startingRule)))
                {
                    if (allCombinations.Count == 0)
                    {
                        allCombinations.Add(simpleRules[int.Parse(startingRule)].First().ToString());
                    }
                    else 
                    {
                        List<string> tempCombinationsList = new List<string>();
                        foreach (var combination in allCombinations)
                        {
                            tempCombinationsList.Add(combination + simpleRules[int.Parse(startingRule)].First().ToString());
                        }

                        allCombinations = new List<string>(tempCombinationsList);
                    }
                }
                else 
                {
                    List<string> recursiveCombinations = RecursiveCall(int.Parse(startingRule));
                    if (allCombinations.Count == 0)
                    {
                        allCombinations.AddRange(recursiveCombinations);
                    }
                    else 
                    {
                        List<string> tempCombinationsList = new List<string>();
                        foreach (var combination in allCombinations)
                        {
                            foreach (var combinationFromRecursive in recursiveCombinations)
                            {
                                tempCombinationsList.Add(combination + combinationFromRecursive);
                            }
                        }

                        allCombinations = new List<string>(tempCombinationsList);
                    }
                }
            }

            List<string> toCheck = groups[1].Split("\r\n").ToList();

            return allCombinations.Intersect(toCheck).Count();
        }

        private static List<string> RecursiveCall(int ruleNumer)
        {
            List<string> allCombination = new List<string>();
            var complexSubRules = complexRules[ruleNumer];
            foreach (var complexSubRule in complexSubRules)
            {
                List<string> recursiveCombinations = new List<string>();
                var rules = complexSubRule.Split(" ");
                foreach (var rule in rules) 
                {
                    if (simpleRules.ContainsKey(int.Parse(rule)))
                    {
                        if (recursiveCombinations.Count == 0)
                        {
                            recursiveCombinations.Add(simpleRules[int.Parse(rule)].First().ToString());
                        }
                        else
                        {
                            List<string> tempCombinationsList = new List<string>();
                            foreach (var sh in recursiveCombinations)
                            {
                                tempCombinationsList.Add(sh + simpleRules[int.Parse(rule)].First().ToString());
                            }

                            recursiveCombinations = new List<string>(tempCombinationsList);
                        }
                    }
                    else
                    {
                        List<string> recursiveCombinationsPart = RecursiveCall(int.Parse(rule));
                        if (recursiveCombinations.Count == 0)
                        {
                            recursiveCombinations.AddRange(recursiveCombinationsPart);
                        }
                        else 
                        {
                            List<string> tempCombinationsList = new List<string>();
                            foreach (var sh in recursiveCombinations) 
                            {
                                foreach (var shs in recursiveCombinationsPart) 
                                {
                                    tempCombinationsList.Add(sh + shs);
                                }
                            }

                            recursiveCombinations = new List<string>(tempCombinationsList);

                        }
                    }
                }

                allCombination.AddRange(recursiveCombinations);
            }

            return allCombination;
        }

        private static int SolvePartTwo(string input)
        {
            string[] groups = input.Split("\r\n\r\n");
            List<string> rules = groups[0].Split("\r\n").ToList();

            Dictionary<int, string> rulesInDictionary = new Dictionary<int, string>();
            Dictionary<int, string> resolvedRule = new Dictionary<int, string>();

            int numberEightOccurece = 0;
            int numberElevenOccurece = 0;

            foreach (string rule in rules)
            {
                rulesInDictionary.Add(int.Parse(rule.Split(":")[0]), rule.Split(":")[1].Trim());
                if (rule.Contains("\""))
                {
                    resolvedRule.Add(int.Parse(rule.Split(":")[0]), rule.Split(":")[1].Trim().Replace("\"", ""));
                }
            }

            rulesInDictionary[8] = "42 | 42 8";
            rulesInDictionary[11] = "42 31 | 42 11 31";

            string regex = String.Empty;
            Regex numbers = new Regex(@"\d+");
            while (true) 
            {
                if (String.IsNullOrEmpty(regex)) 
                {
                    regex = rulesInDictionary[0];
                }

                Dictionary<int, string> resovedRules = new Dictionary<int, string>();
                string[] startRegex = regex.Split(" ");
                foreach (string part in startRegex)
                {
                    if (part == " " || part == "|" || part == "a" || part == "b")
                    {
                        continue;
                    }

                    MatchCollection numberCollection = numbers.Matches(part);
                    string number = String.Empty;
                    if (numberCollection.Count() == 0)
                    {
                        continue;
                    }
                    else
                    {
                        number = numberCollection.First().Value;
                        int ruleNum = int.Parse(number);
                        if (ruleNum == 8)
                        {
                            numberEightOccurece++;
                        }
                        else if (ruleNum == 11) 
                        {
                            numberElevenOccurece++;
                        }

                        if (resovedRules.ContainsKey(ruleNum))
                        {
                            break;
                        }
                        else 
                        {
                            resovedRules.Add(ruleNum, "");
                        }

                        string replacemt = String.Empty;
                        if (resolvedRule.ContainsKey(ruleNum))
                        {
                            replacemt = resolvedRule[ruleNum];
                        }
                        else 
                        {
                            replacemt = rulesInDictionary[ruleNum];

                            //using magic numbers
                            if ((ruleNum == 8 && numberEightOccurece == 15) || (ruleNum == 11 && numberElevenOccurece == 15))
                            {
                                replacemt = rulesInDictionary[ruleNum].Split("|")[0].Trim();
                            }
                        }

                        Regex numberWithBoundary = new Regex(@"\b"+number+@"\b");
                        regex = numberWithBoundary.Replace(regex, $"({replacemt})");
                    }
                }

                if (!numbers.IsMatch(regex)) 
                {
                    break;
                }
            }

            regex = regex.Replace(" ", "");
            int counter = 0;
            Regex regexWithBoundary = new Regex(@"\b" + regex + @"\b");
            foreach (string final in groups[1].Split("\r\n")) 
            {
                if (regexWithBoundary.IsMatch(final)) 
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
