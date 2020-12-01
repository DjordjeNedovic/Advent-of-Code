using System;
using System.IO;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int solution = 0;
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzleInput.txt"));
            int[] modelues = Array.ConvertAll(input, s => Int32.Parse(s));
            foreach (int num in modelues)
            {
                int module = num;
                bool isPosible = true;
                int fuelPerModule = 0;
                while (isPosible)
                {
                    int reslut = Convert.ToInt32(Math.Floor(module / 3.0)) - 2;
                    fuelPerModule += reslut;
                    module = reslut;
                    if (reslut / 3 < 3)
                    {
                        break;
                    }
                }

                solution += fuelPerModule;
            }

            Console.WriteLine($"final reslut is: {solution}");
            Console.ReadKey();
        }
    }
}