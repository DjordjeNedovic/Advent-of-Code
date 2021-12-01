using System;
using System.IO;
using System.Linq;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            int[] report = (Array.ConvertAll(input, s => Int32.Parse(s)));

            Console.WriteLine("########## Day 1 2020 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(report)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(report)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(int[] report)
        {
            int increaceNumber = 0;
            for (int i = 1; i < report.Count(); i++) 
            {
                if (report[i] > report[i - 1]) 
                {
                    increaceNumber++;
                }
            }

            return increaceNumber;
        }

        private static int SolvePartTwo(int[] report)
        {
            int increaceNumber = 0;

            for (int i = 1; i < report.Count()-2; i++)
            {
                int currentOne = report[i - 1] + report[i] + report[i + 1];
                int nextOne = report[i] + report[i + 1] + report[i + 2];

                if (nextOne > currentOne)
                {
                    increaceNumber++;
                }
            }

            return increaceNumber;
        }
    }
}
