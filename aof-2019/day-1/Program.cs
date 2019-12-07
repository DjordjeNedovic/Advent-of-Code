using System;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int finaly = 0;
            int[] ints = new int[] { 1969, 100756 };
            foreach (int num in ints)
            {
                var tt = num;
                bool isPostible = true;
                int mass = 0;
                while (isPostible)
                {
                    var t = Math.Floor(tt / 3.0);
                    int interval = Convert.ToInt32(t);
                    int reslut = interval - 2;

                    Console.WriteLine($"tt: {tt}");
                    mass += reslut;
                    Console.WriteLine(mass);
                    tt = reslut;

                    if (reslut / 3 < 3)
                    {
                        break;
                    }

                }

                finaly += mass;
            }

            Console.WriteLine($"final reslut is: {finaly}");
        }
    }
}