using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_10
{
    class Program
    {
        static Dictionary<long, long> resultSet = new Dictionary<long, long>();
        static int counter = 0;
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            List<long> inputs = (Array.ConvertAll(input, s => Int64.Parse(s))).ToList();
            inputs.Sort();

            Console.WriteLine($"Part one solution:  {SolvePartOne(inputs)}");

            inputs.Add(inputs.Last() + 3);
            inputs.Insert(0, 0);
            Console.WriteLine($"Part two solution:  {SolvePartTwo(inputs.ToArray())}, {counter}");
        }

        private static long SolvePartOne(List<long> inputs)
        {
            int jolt_1_diff = 1;
            int jolt_3_diff = 1;
            for (int i = 0; i < inputs.Count - 1; i++)
            {
                long temp1 = inputs[i];
                long temp2 = inputs[i + 1];
                if (temp2 - temp1 == 1)
                {
                    jolt_1_diff++;
                }
                else if (temp2 - temp1 == 3)
                {
                    jolt_3_diff++;
                }
            }

            return jolt_1_diff * jolt_3_diff;
    }

        //not my proudest solution, thanks reddit on help
        private static long SolvePartTwo(long[] inputs)
        {
            counter++;
            if (resultSet.ContainsKey(inputs.Length)) 
            {
               return resultSet[inputs.Length];
            }

            if (inputs.Length == 1) 
            {
                return 1;
            }

            long total = 0;
            long temp = inputs[0];
            for (int i = 1; i < inputs.Length; i++) 
            {
                if (inputs[i] - temp <= 3) 
                {
                    total +=SolvePartTwo(inputs[i..]);
                }
                else 
                {
                    break;
                }
            }

            resultSet.Add(inputs.Length, total);

            return total;
        }
    }
}