using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_13
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("############ Day 13 2020 ############");
            Console.WriteLine($"Part one solution:  {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution:  {SolvePartTwo(input)}");
            Console.WriteLine("#####################################");
        }

        private static int SolvePartOne(string[] input)
        {
            long departureTime = long.Parse(input[0]);
            var busses = input[1].Split(',').Where(x => x != "x").ToList();
            decimal minuts = 0;
            int earliestBusId = 0;
            foreach (string busId in busses)
            {
                int busNumber = int.Parse(busId);
                decimal multiplyer = Math.Ceiling((decimal) departureTime / busNumber);
                decimal rest = (multiplyer * busNumber) - departureTime;
                if (minuts == 0 || rest < minuts)
                {
                    minuts = rest;
                    earliestBusId = busNumber;
                }
            }

            return earliestBusId * (int) minuts;
        }

        private static long SolvePartTwo(string[] input)
        {
            var busses = input[1].Split(',').ToList();
            /*solved by Chinese Remainder Theorem
             N is sum multiply of all modules
             in this case, multily all busses ids
             */
            long N = GetN(busses);
            long sum = 0;
            for (int i = 0; i < busses.Count; i++)
            {
                if (busses[i] == "x")
                {
                    continue;
                }

                int busNumber = int.Parse(busses[i]);
                long module = AbsoluteModulo(busNumber - i, busNumber);
                long Ni = N / busNumber;
                long inverse = GetInverse(Ni, busNumber);

                sum += module * Ni * inverse;
            }

            return sum % N;
        }

        private static long GetN(List<string> busses)
        {
            long N = 1;
            for (int i = 0; i < busses.Count; i++)
            {
                if (busses[i] == "x")
                {
                    continue;
                }

                N *= int.Parse(busses[i]);
            }

            return N;
        }

        private static long GetInverse(long nU, int cur)
        {
            var b = nU % cur;
            for (int i = 1; i < cur; i++)
            {
                if ((b * i) % cur == 1)
                {
                    return i;
                }
            }

            return 1;
        }

        private static long AbsoluteModulo(int v, int cur)
        {
            return ((v % cur) + cur) % cur;
        }
    }
}