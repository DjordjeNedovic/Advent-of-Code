using System;
using System.Collections.Generic;

namespace day_4
{
    class Program
    {
        private static string input = "130254-678275";

        static void Main(string[] args)
        {
            string[] values = input.Split('-');
            int start = Int32.Parse(values[0]);
            int end = Int32.Parse(values[1]);

            List<string> numbers = new List<string>();
            List<string> partOneSolutions = new List<string>();
            List<string> n = new List<string>();
            for (int i = 1; i < end-start; i++) 
            {
                numbers.Add((start + i).ToString());
            }

            foreach (string number in numbers) 
            {
                if (IsNumberForSolutionForPartOne(number)) 
                {
                    partOneSolutions.Add(number);
                }
            }

            foreach (string nu in numbers) 
            {
                if (IsNumberForSolutionForPartTwo(nu))
                {
                    n.Add(nu);
                }
            }

            //(You guessed 1306<x<1801)your answer is too low
            Console.WriteLine($"Result: {partOneSolutions.Count}");
            Console.WriteLine($"Result: {n.Count}");
            Console.WriteLine(String.Join( ",",n));
        }

        private static bool IsNumberForSolutionForPartTwo(string n)
        {
            int counter = 1;
            bool hasMoreSame = false;
            bool hasPair = false;
            for (int i = 0; i < n.Length; i++)
            {
                if (i == 0)
                    continue;
                var curetn = n[i].ToString();
                var last = n[i - 1].ToString();

                if (Int32.Parse(curetn) < Int32.Parse(last))
                    return false;
                if (Int32.Parse(curetn) == Int32.Parse(last))
                {
                    if (!hasMoreSame)
                    {
                        hasMoreSame = !hasMoreSame;
                        if (!hasPair)
                            hasPair = !hasPair;
                    }

                    counter++;
                }
                if (Int32.Parse(curetn) > Int32.Parse(last))
                {
                    if (counter == 1)
                        continue;
                    if (counter > 2)
                        hasPair = false;
                    hasMoreSame = false;
                    counter = 1;
                }

            }

            return hasPair;
        }

        private static bool IsNumberForSolutionForPartOne(string n) 
        {
            bool hasTwoSame = false;
            for (int i = 0; i < n.Length; i++)
            { 
                if (i == 0)
                    continue;
                var curetn = n[i].ToString();
                var last = n[i - 1].ToString();

                if (Int32.Parse(curetn) < Int32.Parse(last))
                    return false;

                if (Int32.Parse(curetn) > Int32.Parse(last)) 
                {
                    continue;
                }

                if (Int32.Parse(curetn) == Int32.Parse(last))
                {
                    if(!hasTwoSame)
                        hasTwoSame = !hasTwoSame;
                }
            }

            return hasTwoSame;
        }
    }
}