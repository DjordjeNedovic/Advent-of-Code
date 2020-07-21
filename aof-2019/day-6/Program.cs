using System;
using System.Collections.Generic;
using System.IO;

namespace day_6
{
    class Program
    {
        static Dictionary<string, List<string>> OrbitPairs = new Dictionary<string, List<string>>();
        static int numberOfOrbits = 0;

        static void Main(string[] args)
        {
            string[] inputFromTxt = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            foreach (string mapping in inputFromTxt) 
            {
                string[] relation = mapping.Split(')');
                Console.WriteLine($"{relation[1]} is in orbit around {relation[0]}");
                if (OrbitPairs.ContainsKey(relation[0]) == false)
                {
                    OrbitPairs.Add(relation[0], new List<string>());
                }

                OrbitPairs[relation[0]].Add(relation[1]);
            }

            List<string> listOfDirectOrbits = OrbitPairs["COM"];
            GetOrbits(listOfDirectOrbits, 0);
            Console.WriteLine($"Number of direct and indirect orbits is: {numberOfOrbits}");
        }

        private static void GetOrbits(List<string> listOfDirectOrbits, int result)
        {
            result++;
            foreach (var element in listOfDirectOrbits) 
            {
                if (OrbitPairs.ContainsKey(element))
                {
                    GetOrbits(OrbitPairs[element], result);
                }

                numberOfOrbits += result;
                Console.WriteLine($"    planet: {element} has direct and indirect orbits: {result}");
            }
        }
    }
 }