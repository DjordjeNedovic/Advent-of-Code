using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_10
{
    class Program
    {
        private static List<string> invalidOnes;
        private static List<string> incompoleteOnes;
        private static Dictionary<char, int> partOneValues;
        private static Dictionary<char, int> partTwoValues;
        private static Dictionary<char, char> pairs;
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            PopulateDictionaries();
            SortLines(input);

            Console.WriteLine("########## Day 10 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static long SolvePartOne(string[] input)
        {
            long result = 0;
            foreach (string line in invalidOnes)
            {
                Stack<char> stack = new Stack<char>();
                foreach (char t1 in line.ToCharArray())
                {
                    if (pairs.Values.Contains(t1))
                    {
                        char t2 = stack.Pop();
                        if (IfClosingIsInvalid(t1, t2))
                        {
                            result += partOneValues[t1];
                        }
                    }
                    else
                    {
                        stack.Push(t1);
                    }
                }
            }

            return result;
        }

        private static object SolvePartTwo(string[] input)
        {
            long[] results = new long[incompoleteOnes.Count];
            for(int i = 0; i< incompoleteOnes.Count; i++) 
            {
                string line = incompoleteOnes[i];
                Stack<char> stack = new Stack<char>();
                foreach (char t1 in line.ToCharArray())
                {
                    if (pairs.Values.Contains(t1))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(t1);
                    }
                }

                long tempResult = 0;
                foreach (var item in stack) 
                {
                    tempResult *= 5;
                    tempResult += partTwoValues[pairs[item]];
                }

                results[i] = tempResult;
            }

            int middleIndex = (int)Math.Ceiling((double)results.Length / (double)2);
            Array.Sort(results);

            return results[middleIndex-1];
        }

        private static void PopulateDictionaries()
        {
            partOneValues = new Dictionary<char, int>();
            partOneValues.Add(')', 3);
            partOneValues.Add(']', 57);
            partOneValues.Add('}', 1197);
            partOneValues.Add('>', 25137);

            partTwoValues = new Dictionary<char, int>();
            partTwoValues.Add(')', 1);
            partTwoValues.Add(']', 2);
            partTwoValues.Add('}', 3);
            partTwoValues.Add('>', 4);

            pairs = new Dictionary<char, char>();
            pairs.Add('(', ')');
            pairs.Add('[', ']');
            pairs.Add('{', '}');
            pairs.Add('<', '>');
        }

        private static void SortLines(string[] input)
        {
            invalidOnes = new List<string>();
            incompoleteOnes = new List<string>();
            foreach (string line in input)
            {
                Stack<char> temp = new Stack<char>();
                bool isInvalidLine = false;
                foreach (char t1 in line.ToCharArray())
                {
                    if (pairs.Values.Contains(t1))
                    {
                        char t2 = temp.Pop();
                        if (IfClosingIsInvalid(t1, t2))
                        {
                            invalidOnes.Add(line);
                            isInvalidLine = true;
                            break;
                        }
                    }
                    else
                    {
                        temp.Push(t1);
                    }
                }
                
                if (temp.Count != 0 && !isInvalidLine)
                {
                    incompoleteOnes.Add(line);
                }
            }
        }

        private static bool IfClosingIsInvalid(char t1, char t2)
        {
            return (t1 == ')' && t2 != '(') || ((t1 == ']' && t2 != '[')) || ((t1 == '}' && t2 != '{')) || ((t1 == '>' && t2 != '<'));
        }
    }
}
