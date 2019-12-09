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
            List<string> solution = new List<string>();
            for (int i = 1; i < end-start; i++) 
            {
                numbers.Add((start + i).ToString());
            }

            foreach (string n in numbers) 
            {

                if (meth(n)) 
                {
                    solution.Add(n);
                }
            }

            Console.WriteLine($"Result: {solution.Count}");
        }

        private static bool meth(string n) 
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