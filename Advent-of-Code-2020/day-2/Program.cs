using System;
using System.Collections.Generic;
using System.IO;

namespace day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            int partOneSolution = PartOne(input);
            Console.WriteLine($"Part one: there are {partOneSolution} valid passwords");

            int partTwoSolution = PartTwo(input);
            Console.WriteLine($"Part two: there are {partTwoSolution} valid passwords");
        }


        private static int PartOne(string[] input)
        {
            int valid = 0;
            foreach (string dbRow in input)
            {
                int count = 0;

                string[] sp1 = dbRow.Split(":");
                string password = sp1[1].Trim();
                string[] sp2 = sp1[0].Split(" ");
                string code = sp2[1].Trim();
                string[] sp3 = sp2[0].Split("-");

                int min = Int32.Parse(sp3[0].Trim());
                int max = Int32.Parse(sp3[1].Trim());

                foreach (var t in password.ToCharArray())
                {
                    if (t.ToString() == code)
                    {
                        count++;
                    }
                }

                if (count >= min && count <= max)
                {
                    valid++;
                }

            }

            return valid;
        }

        private static int PartTwo(string[] input)
        {
            int valid = 0;
            foreach (string dbRow in input)
            {
                string[] sp1 = dbRow.Split(":");
                string[] sp2 = sp1[0].Split(" ");
                string[] sp3 = sp2[0].Split("-");

                string code = sp2[1].Trim();
                string password = sp1[1].Trim();
                int min = Int32.Parse(sp3[0].Trim());
                int max = Int32.Parse(sp3[1].Trim());

                string first = password[min - 1].ToString();
                string secound = password[max - 1].ToString();

                if ((first == code && secound != code) || (first != code && secound == code))
                {
                    valid++;
                }
            }

            return valid;
        }
    }
}
