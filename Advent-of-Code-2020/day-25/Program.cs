using System;
using System.IO;

namespace day_25
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            long publicKeyOne = long.Parse(input[0]);
            long publicKeyTwo = long.Parse(input[1]);
            long t1 = CalculateLoopSize(publicKeyOne);
            long encrptyKey = Encript(t1, publicKeyTwo);

            Console.WriteLine("########## Day 25 2020 ##########");
            Console.WriteLine($"Part one solution: {encrptyKey}");
            Console.WriteLine("#################################");
        }

        private static long Encript(long t1, long publicKeyTwo)
        {
            long result = 1;
            for (int i = 0; i < t1; i++) 
            {
                result = (result * publicKeyTwo) % 20201227;
            }

            return result;
        }

        private static long CalculateLoopSize(long publicKeyOne)
        {
            long result = 0;
            long step = 1;
            while (true) 
            {
                if (step == publicKeyOne)
                    break;

                step = (step * 7) % 20201227;

                result++;
            }

            return result;
        }
    }
}