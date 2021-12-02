using System;
using System.IO;

namespace day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 2 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(string[] input)
        {
            int depth = 0;
            int postition = 0;
            foreach (string line in input) 
            {
                string[] parts = line.Split(" ");
                if (parts[0] == "forward")
                {
                    postition += Int32.Parse(parts[1]);
                }
                else if (parts[0] == "up")
                {
                    depth -= Int32.Parse(parts[1]);
                }
                else if (parts[0] == "down") 
                {
                    depth += Int32.Parse(parts[1]);
                }
            }

            return depth * postition;
        }

        private static object SolvePartTwo(string[] input)
        {
            int depth = 0;
            int postition = 0;
            int aim = 0;
            foreach (string line in input)
            {
                string[] parts = line.Split(" ");
                if (parts[0] == "forward")
                {
                    int newPostion = Int32.Parse(parts[1]);
                    depth += newPostion * aim;
                    postition += newPostion;
                }
                else if (parts[0] == "up")
                {
                    aim -= Int32.Parse(parts[1]);
                }
                else if (parts[0] == "down")
                {
                    aim += Int32.Parse(parts[1]);
                }
            }

            return depth * postition;
        }
    }
}
