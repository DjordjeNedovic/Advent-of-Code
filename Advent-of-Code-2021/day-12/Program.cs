using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_12
{
    class Program
    {
        private static int counter = 0;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Dictionary<string, List<string>> connections = GetConnections(input);

            Console.WriteLine("########## Day 12 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(connections)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(connections)}");
            Console.WriteLine("################################");
        }

        
        private static object SolvePartOne(Dictionary<string, List<string>> connections)
        {
            Traverse("start", connections, new Stack<string>(), true);
            
            return counter;
        }

        private static object SolvePartTwo(Dictionary<string, List<string>> connections)
        {
            counter = 0;

            Traverse("start", connections, new Stack<string>(), false);

            return counter;
        }

        private static Dictionary<string, List<string>> GetConnections(string[] input)
        {
            Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
            foreach (string line in input)
            {
                string[] parts = line.Split("-");
                if (parts[1] != "start")
                {
                    if (connections.ContainsKey(parts[0]))
                    {
                        connections[parts[0]].Add(parts[1]);
                    }
                    else
                    {
                        connections.Add(parts[0], new List<string>() { parts[1] });
                    }
                }

                if (parts[0] != "start")
                {
                    if (connections.ContainsKey(parts[1]))
                    {
                        connections[parts[1]].Add(parts[0]);
                    }
                    else
                    {
                        connections.Add(parts[1], new List<string>() { parts[0] });
                    }
                }
            }

            return connections;
        }


        private static void Traverse(string cave, Dictionary<string, List<string>> connections, Stack<string> stack, bool isPartOne)
        {
            stack.Push(cave);
            if (connections[cave].Contains("end")) 
            {
                counter++; 
            }

            foreach (string futureCave in connections[cave]) 
            {
                if (isPartOne)
                {
                    if (futureCave != "end" && (!stack.Contains(futureCave) || char.IsUpper(futureCave[0])))
                    {
                        Traverse(futureCave, connections, new Stack<string>(stack), isPartOne);
                    }
                }
                else 
                {
                    var test = stack.Where(x => x == futureCave && char.IsLower(x[0])).GroupBy(x=>x).Count();
                    int num = 0;
                    if (test != 0) 
                    {
                        num = stack.Where(x => char.IsLower(x[0])).GroupBy(x => x).Max(x => x.Count());
                    }

                    if (futureCave != "end" && (num != 2 || !stack.Contains(futureCave) || char.IsUpper(futureCave[0])))
                    {
                        Traverse(futureCave, connections, new Stack<string>(stack), isPartOne);
                    }
                }
            }
        }
    }
}
