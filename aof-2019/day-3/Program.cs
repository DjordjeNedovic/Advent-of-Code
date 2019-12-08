using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_3
{
    class Program
    {
        static List<Tuple<int, int>> firstWire = new List<Tuple<int, int>>();
        static List<Tuple<int, int>> secoundWire = new List<Tuple<int, int>>();

        static string firstPath = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
        static string secoundPath = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";

        static void Main(string[] args)
        {
            firstWire.Add(new Tuple<int, int>(0,0));
            CalculatePaths(firstPath, ref firstWire);
            secoundWire.Add(new Tuple<int, int>(0,0));
            CalculatePaths(secoundPath, ref secoundWire);
            List<Tuple<int, int>> corses = firstWire.Where(x => secoundWire.Any(y => y.Item1 == x.Item1 && y.Item2 == x.Item2)).Select(x => x).ToList();
            var t = corses[1];
            int sh = 0;
            foreach (Tuple<int, int> course in corses) 
            {
                Console.WriteLine($"pair({course.Item1},{course.Item2})");
                if (Math.Abs(course.Item1) + Math.Abs(course.Item2) > 0 && Math.Abs(course.Item1) + Math.Abs(course.Item2) < Math.Abs(t.Item1) + Math.Abs(t.Item2))
                    t = course;

                int indexA = firstWire.IndexOf(course);
                int indexB = secoundWire.IndexOf(course);
                if (sh == 0)
                    sh = indexA + indexB;
                else if (sh > indexA + indexB)
                    sh = indexA + indexB;
                
            }

            Console.WriteLine($"index of a+b = {sh}");
            Console.WriteLine($"Shortest path is on ({t.Item1},{t.Item2}) with distance {Math.Abs(t.Item1) + Math.Abs(t.Item2)}");
        }

        private static void CalculatePaths(string path, ref List<Tuple<int, int>> wirePaths) 
        {

            string[] directions = path.Split(',');
            foreach (string direction in directions) 
            {
                Tuple<int,int> last = wirePaths.Last();
                int xValue = last.Item1;
                int yValue = last.Item2;
                Match replacedString = Regex.Match(direction, @"[0-9]+");
                if (direction.StartsWith("U")) 
                {
                    for (int i = 1; i < Int32.Parse(replacedString.Value)+1; i++) 
                    {
                        yValue += 1;
                        Tuple<int, int> p = new Tuple<int, int>(xValue, yValue);
                        wirePaths.Add(p);
                    }
                }
                if (direction.StartsWith("D"))
                {
                    for (int i = 1; i < Int32.Parse(replacedString.Value)+1; i++)
                    {
                        yValue -= 1;
                        Tuple<int, int> p = new Tuple<int, int>(xValue, yValue);
                        wirePaths.Add(p);
                    }
                }
                if (direction.StartsWith("R")) 
                {
                    for (int i = 1; i < Int32.Parse(replacedString.Value) + 1; i++)
                    {
                        xValue +=1;
                        Tuple<int, int> p = new Tuple<int, int>(xValue, yValue);
                        wirePaths.Add(p);
                    }
                }
                if (direction.StartsWith("L"))
                {
                    for (int i = 1; i < Int32.Parse(replacedString.Value) + 1; i++)
                    {
                        xValue -=1;
                        Tuple<int, int> p = new Tuple<int, int>(xValue, yValue);
                        wirePaths.Add(p);
                    }
                }

            }

            //return wirePaths;
        }
    }
}
