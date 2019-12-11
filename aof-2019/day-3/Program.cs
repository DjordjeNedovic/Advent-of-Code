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

        static string firstPath = "R75,D30,R83,U83,L12,D49,R71,U7,L72";
        static string secoundPath = "U62,R66,U55,R34,D71,R55,D58,R83";

        static void Main(string[] args)
        {
            firstWire.Add(new Tuple<int, int>(0,0));
            CalculatePaths(firstPath, ref firstWire);
            secoundWire.Add(new Tuple<int, int>(0,0));
            CalculatePaths(secoundPath, ref secoundWire);

            List<Tuple<int, int>> corses = firstWire.Where(x => secoundWire.Any(y => y.Item1 == x.Item1 && y.Item2 == x.Item2)).Select(x => x).ToList();
            Tuple<int, int> corse = corses[1];
            int fewestSteps = 0;
            
            foreach (Tuple<int, int> course in corses) 
            {
                Console.WriteLine($"pair({course.Item1},{course.Item2})");
                if (Math.Abs(course.Item1) + Math.Abs(course.Item2) > 0 && Math.Abs(course.Item1) + Math.Abs(course.Item2) < Math.Abs(corse.Item1) + Math.Abs(corse.Item2))
                    corse = course;

                int indexA = firstWire.IndexOf(course);
                int indexB = secoundWire.IndexOf(course);
                if (fewestSteps == 0)
                {
                    fewestSteps = indexA + indexB;
                }
                else if (fewestSteps > indexA + indexB) 
                {
                    fewestSteps = indexA + indexB;
                }
            }

            int manhattanDistanceToTheClosestIntersection = Math.Abs(corse.Item1) + Math.Abs(corse.Item2);
            Console.WriteLine($"Fewest combined steps the wires must take to reach an intersection is {fewestSteps}");
            Console.WriteLine($"Manhattan distance from the central port to the closest intersection is {manhattanDistanceToTheClosestIntersection}");
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
        }
    }
}
