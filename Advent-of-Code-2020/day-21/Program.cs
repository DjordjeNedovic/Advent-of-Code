using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_21
{
    class Program
    {
        static Dictionary<string, List<string>> alergensRecepies = new Dictionary<string, List<string>>();
        static List<string> allIngredients = new List<string>();
        static List<string> allAlergens = new List<string>();

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("########## Day 21 2020 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo()}");
            Console.WriteLine("#################################");
        }

        private static int SolvePartOne(string[] input)
        {
            foreach (string line in input)
            {
                string[] ingredients = line.Split("(contains ")[0].Trim().Split(" ");
                allIngredients.AddRange(ingredients);
                string[] alergens = line.Split("(contains ")[1].Trim().Replace(")", "").Split(",");
                foreach (string alergen in alergens)
                {
                    if (alergensRecepies.ContainsKey(alergen.Trim()))
                    {
                        alergensRecepies[alergen.Trim()] = new List<string>(alergensRecepies[alergen.Trim()].Intersect(ingredients.ToList()));
                    }
                    else
                    {
                        alergensRecepies.Add(alergen.Trim(), ingredients.ToList());
                    }
                }
            }

            Dictionary<string, List<string>> orderedDic = alergensRecepies.OrderBy(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
            foreach (var entry in orderedDic)
            {
                if (entry.Value.Count == 1)
                {
                    allAlergens.AddRange(entry.Value);
                }
                else
                {
                    var filteredList = entry.Value.Except(allAlergens).ToList();
                    allAlergens.AddRange(filteredList);
                    alergensRecepies[entry.Key] = new List<string>(filteredList);
                }
            }

            foreach (string gg in allAlergens)
            {
                while (allIngredients.Contains(gg))
                {
                    allIngredients.Remove(gg);
                }
            }

            return allIngredients.Count;
        }

        private static string SolvePartTwo()
        {
            Dictionary<string, List<string>> orderedByName = alergensRecepies.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            string canonicalDangerousIngredientString = String.Empty;
            foreach (var entry in orderedByName)
            {
                if (canonicalDangerousIngredientString == String.Empty)
                {
                    canonicalDangerousIngredientString = string.Join("", entry.Value);
                }
                else 
                {
                    canonicalDangerousIngredientString = $"{canonicalDangerousIngredientString},{string.Join("", entry.Value)}";
                }
            }

            return canonicalDangerousIngredientString;
        }
    }
}
