using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine($"Part one solution:  { SolvePartOne(inputs)}");
            Console.WriteLine($"Part two solution:  { SolvePartTwo(inputs)}");
        }

        private static long SolvePartOne(string[] inputs)
        {
            Dictionary<long, long> resultSet = new Dictionary<long, long>();
            string regex = @"mem\[(?<adress>\d+)\] = (?<value>\d+)";
            string mask = string.Empty;
            foreach (string line in inputs)
            {
                if (line.Contains("mask"))
                {
                    mask = line.Split("=")[1].Trim();
                    continue;
                }
                else if (line.Contains("mem"))
                {
                    Match match = Regex.Match(line, regex);
                    long address = Int64.Parse(match.Groups["adress"].Value);
                    long value = Int64.Parse(match.Groups["value"].Value);

                    string s = Convert.ToString(value, 2);

                    long[] bits = s.PadLeft(36, '0') 
                                 .Select(c => Int64.Parse(c.ToString())) 
                                 .ToArray(); 

                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (mask.ElementAt(i) != 'X')
                        {
                            bits[i] = Int32.Parse(mask[i].ToString());
                        }
                    }

                    long calculatedValue = Convert.ToInt64(String.Join("", bits), 2);
                    if (resultSet.ContainsKey(address))
                    {
                        resultSet[address] = calculatedValue;
                    }
                    else
                    {
                        resultSet.Add(address, calculatedValue);
                    }
                }
            }

            return resultSet.Select(x => x.Value).Aggregate((long)0, (x, y) => x + y);
        }

        private static long SolvePartTwo(string[] inputs)
        {
            Dictionary<long, long> resultSet = new Dictionary<long, long>();
            string regex = @"mem\[(?<adress>\d+)\] = (?<value>\d+)";
            string mask = string.Empty;
            foreach (string line in inputs)
            {
                if (line.Contains("mask"))
                {
                    mask = line.Split("=")[1].Trim();
                    continue;
                }
                else if (line.Contains("mem"))
                {
                    Match match = Regex.Match(line, regex);
                    long address = Int64.Parse(match.Groups["adress"].Value);
                    long value = Int64.Parse(match.Groups["value"].Value);

                    string stringInBits = Convert.ToString(address, 2); 
                    char[] bits = stringInBits.PadLeft(36, '0').ToArray(); 
                    for (int i = 0; i < mask.Length; i++) 
                    {
                        if (mask.ElementAt(i) != '0')
                        {
                            bits[i] = mask[i];
                        }
                    }

                    List<string> addresses = GenerateAdresses(String.Join("", bits));
                    foreach (var sss in addresses) 
                    {
                        long newAddress = Convert.ToInt64(String.Join("", sss), 2);
                        if (resultSet.ContainsKey(newAddress))
                        {
                            resultSet[newAddress] = value;
                        }
                        else
                        {
                            resultSet.Add(newAddress, value);
                        }
                    }
                }
            }

            return resultSet.Select(x => x.Value).Aggregate((long)0, (x, y) => x + y);
        }

        private static List<string> GenerateAdresses(string searchString)
        {
            List<string> results = new List<string>();
            if (!searchString.Contains('X'))
            {
                results.Add(searchString);
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    string tempString = searchString;
                    results.AddRange(GenerateAdresses(ReplaceFirst(tempString, "X", i.ToString())));
                }
            }

            return results;
        }

        static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }

            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}