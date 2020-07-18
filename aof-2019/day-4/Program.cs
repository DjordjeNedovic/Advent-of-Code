using System;
using System.Collections.Generic;
using System.IO;

namespace day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            string[] values = input.Split('-');
            int start = Int32.Parse(values[0]);
            int end = Int32.Parse(values[1]);

            //get all number between start and end value
            List<string> numbers = new List<string>();
            for (int i = 1; i < end - start; i++)
            {
                numbers.Add((start + i).ToString());
            }

            List<string> partOneRequestSet = new List<string>();
            List<string> partTwoRequestSet = new List<string>();
            foreach (string number in numbers) 
            {
                if (PartOneRequest(number)) 
                {
                    partOneRequestSet.Add(number);
                }
                if (PartTwoRequest(number))
                {
                    partTwoRequestSet.Add(number);
                }
            }

            Console.WriteLine($"Result: {partOneRequestSet.Count}");
            Console.WriteLine($"Result: {partTwoRequestSet.Count}");
        }

        private static bool PartOneRequest(string number)
        {
            bool hasTwoSame = false;
            for (int index = 1; index < number.Length; index++) 
            {
                string currentNumber = number[index].ToString();
                string previousNumber = number[index-1].ToString();

                if (Int32.Parse(currentNumber) < Int32.Parse(previousNumber)) 
                {
                    return false;
                }

                if (Int32.Parse(currentNumber.ToString()) > Int32.Parse(previousNumber))
                {
                    continue;
                }

                if (Int32.Parse(currentNumber) == Int32.Parse(previousNumber)) 
                {
                    hasTwoSame = true;
                }
            }

            return hasTwoSame;
        }

        private static bool PartTwoRequest(string number)
        {
            bool hasTwoSame = false;
            int counter = 1;
            for (int index = 1; index < number.Length; index++)
            {
                string currentNumber = number[index].ToString();
                string previousNumber = number[index - 1].ToString();

                if (Int32.Parse(currentNumber) < Int32.Parse(previousNumber))
                {
                    return false;
                }

                if (Int32.Parse(currentNumber.ToString()) > Int32.Parse(previousNumber))
                {
                    if (counter == 2) 
                    {
                        hasTwoSame = true;
                    }

                    counter = 1;
                    continue;
                }

                if (Int32.Parse(currentNumber) == Int32.Parse(previousNumber))
                {
                    counter++;
                }

                if (index == 5 && counter == 2) 
                {
                    hasTwoSame = true;
                }
            }

            return hasTwoSame;
        }
    }
}