using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Split(",");
            int[] inputs = (Array.ConvertAll(input, s => Int32.Parse(s)));

            Console.WriteLine("########## Day 6 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(inputs)}");
            inputs = (Array.ConvertAll(input, s => Int32.Parse(s)));
            Console.WriteLine($"Part two solution: {SolvePartTwo(inputs)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(int[] input)
        {
            for(int iteration = 0; iteration < 80; iteration++)
            {
                List<int> newDay = new List<int>();
                for (int i = 0; i < input.Count(); i++) 
                {
                    if (input[i] == 0) 
                    {
                        input[i] = 7;
                        newDay.Add(8);
                    }

                    input[i]--;
                }

                List<int> t = new List<int>();
                t.AddRange(input);
                t.AddRange(newDay);

                input = t.ToArray();
            }

            return input.Length;
        }

        private static object SolvePartTwo(int[] input)
        {
            long[] fishGeneration = new long[9];
            foreach (int i in input)
            {
                fishGeneration[i]++;
            }

            for (int iteration = 0; iteration < 256; iteration++)
            {
                long newOnes = fishGeneration[0];
                for (int i = 1; i < fishGeneration.Length; i++)
                {
                    fishGeneration[i - 1] = fishGeneration[i];
                }

                fishGeneration[8] = newOnes;
                fishGeneration[6] += newOnes;
            }

            return fishGeneration.Sum();
        }
    }
}
