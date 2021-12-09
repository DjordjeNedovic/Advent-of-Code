using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_7
{
    class Program
    {
        private static List<string> startElements = new List<string>();
        private static string lastElement = "";

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("########## Day 7 2018 ##########");
            SortedDictionary<string, List<string>> dictionary = GetDictionary(input);
            Console.WriteLine($"Part one solution: {SolvePartOne(dictionary)}");
            dictionary = GetDictionary(input);
            Console.WriteLine($"Part two solution: {SolvePartTwo(dictionary)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(SortedDictionary<string, List<string>> dictionary)
        {
            string result = "";
            var newOne = startElements.First();
            List<string> options = new List<string>();
            List<string> done = new List<string>();
            options.AddRange(startElements);
            int counter = 0;
            while (true)
            {
                if (newOne != lastElement && !done.Contains(newOne) && IsCurrentElementDependend(dictionary, lastElement, newOne, options))
                {
                    newOne = options.ElementAt(++counter);
                    if (newOne == lastElement)
                    {
                        newOne = options.ElementAt(++counter);
                    }

                    continue;
                }

                if (options.Count == 0 || newOne == lastElement)
                    break;

                counter = 0;
                options.Remove(newOne);
                done.Add(newOne);
                result += newOne;

                options.AddRange(dictionary[newOne]);
                options = options.Distinct().ToList();
                dictionary.Remove(newOne);

                options = options.Where(x => !done.Contains(x)).ToList();
                options.Sort();
                newOne = options.First();
            }

            result += lastElement;

            return result;
        }

        private static object SolvePartTwo(SortedDictionary<string, List<string>> dictionary)
        {
            int[] workers = new int[5];
            string[] elements = new string[5];
            string abeceda = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            List<char> alphabet = abeceda.ToList();
            int counter = 0;
            
            List<string> options = new List<string>();
            List<string> done = new List<string>();
            options.AddRange(startElements);
            options.Sort();

            while (true)
            {
                if (workers.Any(x => x <= counter)) 
                {
                    for (int workerIndex = 0; workerIndex < workers.Length; workerIndex++) 
                    {
                        if (workers[workerIndex] <= counter) 
                        {
                            string element = "";
                            if (elements[workerIndex] != null && elements[workerIndex] !="")
                            {
                                options.AddRange(dictionary[elements[workerIndex]]);
                                options = options.Distinct().ToList();
                                options.Sort();
                                dictionary.Remove(elements[workerIndex]);
                                done.Add(elements[workerIndex]);
                                elements[workerIndex] = "";
                            }
                            for (int optionIndex = 0; optionIndex < options.Count; optionIndex++)
                            {
                                element = options[optionIndex];
                                if (!IsCurrentElementDependend(dictionary, lastElement, element, options) && lastElement != element)
                                {
                                    int ofset = alphabet.IndexOf(char.Parse(element)) + 1;
                                    workers[workerIndex] = ofset + 60 + counter;
                                    elements[workerIndex] = element;
                                    options.Remove(element);

                                    break;
                                }
                            }
                        }
                    }
                }

                if (dictionary.Count == 0)
                {
                    int ofset = alphabet.IndexOf(char.Parse(lastElement)) + 1;

                    // in this logic, there is one extra step made in last calcluation
                    // i don't know how i made it, but this -1 will solve that

                    counter += (ofset + 60 - 1);
                    //TODO: find why i need to do -1 on last step

                    break;
                }

                counter++;
            }

            return counter;
        }

        private static SortedDictionary<string, List<string>> GetDictionary(string[] input)
        {
            SortedDictionary<string, List<string>> dictionary = new SortedDictionary<string, List<string>>();
            string regex = @"Step (?<parent>\w) must be finished before step (?<child>\w) can begin.";
            List<string> elements = new List<string>();
            foreach (string line in input)
            {
                Match match = Regex.Match(line, regex);
                string parentEleemnt = match.Groups["parent"].Value;
                string childElement = match.Groups["child"].Value;

                if (dictionary.ContainsKey(parentEleemnt))
                {
                    dictionary[parentEleemnt].Add(childElement);
                }
                else
                {
                    dictionary.Add(parentEleemnt, new List<string>() { childElement });
                }

                if (!elements.Contains(parentEleemnt)) { elements.Add(parentEleemnt); }
                if (!elements.Contains(childElement)) { elements.Add(childElement); }
            }

            lastElement = elements.First(x => !dictionary.Keys.Contains(x));
            var listOfValues = dictionary.Values.SelectMany(x => x).Distinct().ToList();
            listOfValues.Sort();
            startElements = dictionary.Keys.Where(x => !listOfValues.Contains(x)).ToList();
            startElements.Sort();

            return dictionary;
        }

        private static bool IsCurrentElementDependend(SortedDictionary<string, List<string>> dictionary, string lastElement, string newOne, List<string> options)
        {
            for (int i = 0; i < dictionary.Keys.Count; i++)
            {
                string temp = dictionary.ElementAt(i).Key;
                if (temp == lastElement)
                {
                    continue;
                }

                if (dictionary[temp].Contains(newOne))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
