using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            long[] inputs = (Array.ConvertAll(input, s => Int64.Parse(s)));
            long findThisNumber = SolvePartOne(inputs);
            Console.WriteLine($"Part one solution:  {findThisNumber}");
            Console.WriteLine($"Part two solution:  {SolvePartTwo(findThisNumber, inputs)}");
        }

        private static long SolvePartTwo(long findThisNumber, long[] inputs)
        {
            for (int y = 0; y < inputs.Length; y++)
            {
                for (int k = y + 2; k < inputs.Length; k++)
                {
                    //TIL array split sintax
                    long[] temp = inputs[y..k];
                    long sum = temp.Sum();
                    if (sum == findThisNumber)
                    {
                        return temp.Min() + temp.Max();
                    }
                }
            }

            return 0;
        }

        private static long SolvePartOne(long[] inputs)
        {
            List<long> results = new List<long>();
            int preambleSize = 25;
            for (int i = preambleSize; i < inputs.Length; i++)
            {
                long goal = inputs[i];
                int yStartIndex = i - preambleSize;
                for (int y = yStartIndex; y < i; y++)
                {
                    for (int k = yStartIndex; k < i; k++)
                    {
                        if (inputs[k] != inputs[y] && inputs[y] + inputs[k] == goal)
                        {
                            results.Add(goal);
                        }
                    }
                }

                if (!results.Exists(x => x == goal))
                {
                    return goal;
                }
            }

            return 0;
        }
    }
}
