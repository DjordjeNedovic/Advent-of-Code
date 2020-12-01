using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            List<int> expenceReport = (Array.ConvertAll(input, s => Int32.Parse(s))).ToList();
            expenceReport.Sort();

            int solutionPartOne = PartOne(expenceReport);
            Console.WriteLine($"Part one solution is {solutionPartOne}");

            int solutionPartTwo = PartTwo(expenceReport);
            Console.WriteLine($"Part two solution is {solutionPartTwo}");
        }

        static int PartOne(List<int> modelues) 
        {
            foreach (int i in modelues)
            {
                foreach (int y in modelues)
                {
                    if (i + y == 2020)
                    {
                        return i * y;
                    }
                }
            }

            return 0;
        }

        static int PartTwo(List<int> modelues)
        {
            foreach (int i in modelues)
            {
                foreach (int y in modelues)
                {
                    foreach (int j in modelues)
                    {
                        if (i + y + j == 2020)
                        {
                            return i * y * j;
                        }
                    }
                }
            }

            return 0;
        }
    }
}
