using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Split(",");
            int[] inputs = (Array.ConvertAll(input, s => Int32.Parse(s)));

            Console.WriteLine("########## Day 5 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(inputs)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(inputs)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(int[] inputs)
        {
            int max = inputs.Max();
            int[] results = new int[max];
            for (int i = 1; i < max; i++) 
            {
                int result = 0;
                for (int j = 0; j < inputs.Length; j++) 
                {
                    result += Math.Abs(inputs[j] - i);
                }

                results[i] = result;
            }

            return results.Where(x=> x!=0).Min();
        }

        private static object SolvePartTwo(int[] inputs)
        {
            Dictionary<int, int> resultPairs = new Dictionary<int, int>();
            int max = inputs.Max();
            int[] results = new int[max];
            for (int i = 1; i < max; i++)
            {
                int result = 0;
                for (int j = 0; j < inputs.Length; j++)
                {
                    int numberOfSteps = Math.Abs(inputs[j] - i);

                    if (resultPairs.ContainsKey(numberOfSteps)) 
                    {
                        result += resultPairs[numberOfSteps];
                    }
                    else
                    {
                        int sum = Enumerable.Range(1, numberOfSteps).ToArray().Sum();
                        resultPairs.Add(numberOfSteps, sum);
                        result += sum;
                    }
                }

                results[i] = result;
            }

            return results.Where(x => x != 0).Min();
        }
    }
}
