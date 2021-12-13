using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_14
{
    class Program
    {
        static long oreCounter = 0;
        static Dictionary<string, long> stock = new Dictionary<string, long>();
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Dictionary<string, List<string>> fuelRecepies = ParseRecepies(input);
            PartOne(fuelRecepies);
            Console.WriteLine(oreCounter);
        }

        private static Dictionary<string, List<string>> ParseRecepies(string[] input)
        {
            Dictionary<string, List<string>> bagsRecepies = new Dictionary<string, List<string>>();
            foreach (string b in input)
            {
                string[] fuelRecepieContent = b.Split(" => ");
                List<string> bagsChilds = fuelRecepieContent[0].Split(",").Select(x => x.Trim()).ToList();
                if (!bagsRecepies.ContainsKey(fuelRecepieContent[1]))
                {
                    bagsRecepies.Add(fuelRecepieContent[1], bagsChilds);
                }
                else
                {
                    bagsRecepies[fuelRecepieContent[1]].AddRange(bagsChilds);
                }
            }

            return bagsRecepies.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }


        private static void PartOne(Dictionary<string, List<string>> fuelRecepies)
        {
            var fuelIngridience = fuelRecepies["1 FUEL"];
            Console.WriteLine("1 FUEL");
            foreach (string ingridience in fuelIngridience)
            {
                Console.WriteLine("\t" + ingridience);
                RecursivePartOne(ingridience, fuelRecepies, 2);
            }
        }

        private static void RecursivePartOne(string ingridience, Dictionary<string, List<string>> fuelRecepies, long tabs)
        {
            string tab = String.Empty;
            for (long i = 0; i < tabs; i++)
            {
                tab += "\t";
            }

            string regex = @"(?<value>\d+) (?<name>\w+)";

            //provided ingridient
            Match match = Regex.Match(ingridience, regex);
            string name = match.Groups["name"].Value;
            long neededSize = Int64.Parse(match.Groups["value"].Value);

            //recepie for provided ingridient
            string ingridienceKey = fuelRecepies.Keys.Where(x => x.Split(" ")[1].Equals(name)).First();
            List<string> recepieIngridiance = fuelRecepies[ingridienceKey];
            Match match2 = Regex.Match(ingridienceKey, regex);
            long createdSize = Int64.Parse(match2.Groups["value"].Value);

            if (stock.ContainsKey(name))
            {
                if (stock[name] > neededSize)
                {
                    stock[name] -= neededSize;
                    return;
                }
                else
                {
                    neededSize -= stock[name];
                    stock[name] = 0;
                }
            }

            long toMake = (long)Math.Ceiling((double)neededSize / createdSize);

            if (createdSize * toMake > neededSize)
            {
                long remainder = createdSize * toMake - neededSize;
                if (stock.ContainsKey(name))
                {
                    stock[name] += remainder;
                }
                else
                {
                    stock.Add(name, remainder);
                }

                Console.WriteLine($"\t{tab}[{(String.Join(" / ", stock.Select(i => $"{i.Key}: {i.Value}").ToList()))}]");
            }

            foreach (string recepie in recepieIngridiance)
            {
                Console.WriteLine(tab + recepie);

                Match match3 = Regex.Match(recepie, regex);
                string nameFromRecepie = match3.Groups["name"].Value;
                long size = Int64.Parse(match3.Groups["value"].Value);

                var resecpirNewSize = $"{size * toMake} {nameFromRecepie}";

                if (nameFromRecepie == "ORE")
                {
                    if (toMake <= 1)
                    {
                        oreCounter += size;
                    }
                    else
                    {
                        oreCounter += size * toMake;
                    }

                    return;
                }


                RecursivePartOne(resecpirNewSize, fuelRecepies, tabs + 1);
            }
        }
    }
}
