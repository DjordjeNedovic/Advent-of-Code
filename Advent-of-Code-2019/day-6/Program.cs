using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_6
{
    class Program
    {
        static Dictionary<string, List<string>> OrbitPairs = new Dictionary<string, List<string>>();
        static int numberOfOrbits = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("################### PART ONE ###################");
            string[] inputFromTxt = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            foreach (string mapping in inputFromTxt) 
            {
                string[] relation = mapping.Split(')');
                if (OrbitPairs.ContainsKey(relation[0]) == false)
                {
                    OrbitPairs.Add(relation[0], new List<string>());
                }

                OrbitPairs[relation[0]].Add(relation[1]);
            }

            List<string> listOfDirectOrbits = OrbitPairs["COM"];
            GetOrbits(listOfDirectOrbits, 0);
            Console.WriteLine($"Number of direct and indirect orbits is: {numberOfOrbits}");
            Console.WriteLine();
            Console.WriteLine("################### PART Two ###################");
            List<string> childs = FindPathTo(listOfDirectOrbits, 0, "SAN", new List<string>());
            List<string> childs2 = FindPathTo(listOfDirectOrbits, 0, "YOU", new List<string>());

            //Find uniqe items(planets) from both list, substract by two because YOU and SAN shoud not count
            List<string> uniqueA = childs.Except(childs2).ToList();
            List<string> uniqueB = childs2.Except(childs).ToList();
            List<string> finalPath = uniqueA.Union(uniqueB).ToList();

            Console.WriteLine($"Minimum number of orbital transfers required to move from the object YOU to the object SAN is: {finalPath.Count - 2}");
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
                
                //Console.WriteLine($"    planet: {element} has direct and indirect orbits: {result}");
            }
        }

        private static List<string> FindPathTo(List<string> listOfDirectOrbits, int result, string planet, List<string> childs)
        {
            foreach (var element in listOfDirectOrbits)
            {
                List<string> el = new List<string>();
                el.AddRange(childs);
                if (OrbitPairs.ContainsKey(element))
                {
                    el.Add(element);
                    List<String> sch = FindPathTo(OrbitPairs[element], result, planet, el);

                    if (sch.Contains(planet))
                        return sch;
                    //childs.Add(element);
                }

                if (element.Equals(planet)) 
                {
                    el.Add(planet);
                    return el;
                }
            }

            return childs;
        }
    }
 }