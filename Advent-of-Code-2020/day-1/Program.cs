using System;
using System.IO;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            int[] modelues = Array.ConvertAll(input, s => Int32.Parse(s));

            int solutionPartOne = PartOne(modelues);
            Console.WriteLine($"Part one solution is {solutionPartOne}");

            int solutionPartTwo = PartTwo(modelues);
            Console.WriteLine($"Part two solution is {solutionPartTwo}");
        }

        static int PartOne(int[] modelues) 
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

        static int PartTwo(int[] modelues)
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
