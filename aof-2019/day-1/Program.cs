using System;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int solution = 0;
            int[] modelues = new int[] { 1969, 100756 };
            foreach (int num in modelues)
            {
                int module = num;
                bool isPostible = true;
                int fuelPerModule = 0;
                while (isPostible)
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
        }
    }
}