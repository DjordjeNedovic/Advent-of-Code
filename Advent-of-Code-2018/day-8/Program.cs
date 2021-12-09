using System;
using System.IO;
using System.Linq;

namespace day_8
{
    class Program
    {
        private static int partOneCount = 0;
        static void Main(string[] args)
        {
            string[] input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Split(" ");
            int[] startArray = Array.ConvertAll(input, x => Int32.Parse(x));
            Console.WriteLine("########## Day 8 2018 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(startArray)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(int[] input)
        {
            RecursivePartOne(input, 0);

            return partOneCount;
        }

        private static int RecursivePartOne(int[] body, int currentIndex)
        {
            var childCount = body[currentIndex];
            var metaCount = body[currentIndex + 1];

            currentIndex += 2;
            while (childCount > 0)
            {
                currentIndex = RecursivePartOne(body, currentIndex);
                childCount--;
            }

            partOneCount += body[currentIndex..(currentIndex + metaCount)].Sum();
            currentIndex += metaCount;
            return currentIndex;
        }
    }
}
