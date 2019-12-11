using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] map = File.ReadAllLines("TextFile1.txt");
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            foreach (string orbit in map) 
            {
                string[] objectsInOrbit = orbit.Split(')');
                string COM = objectsInOrbit[0];
                string objectInOrbit = objectsInOrbit[1];

                Console.WriteLine($"{objectInOrbit} is in orbit of {COM}");

                if (!dictionary.ContainsKey(objectInOrbit))
                {
                    List<string> list = new List<string>();
                    list.Add(COM);
                    dictionary.Add(objectInOrbit, list);
                }
                else 
                {
                    dictionary[objectInOrbit].Add(COM);
                }
                if (!dictionary.ContainsKey(COM))
                {
                    List<string> list = new List<string>();
                    dictionary.Add(COM, list);
                }
                else
                {
                    dictionary[objectInOrbit].AddRange(dictionary[COM]);
                }

            }

            int number = 0;
            foreach (var tt in dictionary.Values) 
            {
                number += tt.Count;
                if (tt.Count == 0) 
                {
                    Console.WriteLine("ROOT");
                }
            }

            //(You guessed 2221.)
            Console.WriteLine($"otal number of direct and indirect orbits is {number}");
        }
    }
}
