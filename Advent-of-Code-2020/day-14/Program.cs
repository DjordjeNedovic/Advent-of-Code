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
            SolvePartOne(inputs);
            SolvePartTwo(inputs);
        }

        private static void SolvePartOne(string[] inputs)
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

                    string s = Convert.ToString(value, 2); //Convert to binary in a string

                    long[] bits = s.PadLeft(36, '0') // Add 0's from left
                                 .Select(c => Int64.Parse(c.ToString())) // convert each char to int
                                 .ToArray(); // Convert IEnumerable from select to Array

                    for (int i = 0; i < mask.Length; i++)
                    {
                        var t = mask.ElementAt(i);
                        if (t != 'X')
                        {
                            bits[i] = Int32.Parse(mask[i].ToString());
                        }
                    }

                    Console.WriteLine(String.Join("", bits));

                    var sss = Convert.ToInt64(String.Join("", bits), 2);
                    if (resultSet.ContainsKey(address))
                    {
                        resultSet[address] = sss;
                    }
                    else
                    {
                        resultSet.Add(address, sss);
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            var result = resultSet.Select(x => x.Value).Aggregate((long)0, (x, y) => (long)x + (long)y);
            Console.WriteLine(result);
        }

        private static void SolvePartTwo(string[] inputs)
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

                    //convertAddress
                    string s = Convert.ToString(address, 2); //Convert to binary in a string

                    char[] bits = s.PadLeft(36, '0') // Add 0's from left
                                   .ToArray(); // Convert IEnumerable from select to Array

                    for (int i = 0; i < mask.Length; i++) 
                    {
                        if (mask.ElementAt(i) != '0')
                        {
                            bits[i] = mask[i];
                        }
                    }

                    string t = string.Empty;
                    //Console.WriteLine(String.Join("", bits));
                    var ss = GenerateAdresses(String.Join("", bits));

                    foreach (var sss in ss) 
                    {
                        long newAddress = Convert.ToInt64(String.Join("", sss), 2);
                        //Console.WriteLine(newAddress);

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
                else
                {
                    throw new NotImplementedException();
                }
            }
            var result = resultSet.Select(x => x.Value).Aggregate((long)0, (x, y) => (long)x + (long)y);
            Console.WriteLine(result);
        }

        private static List<string> GenerateAdresses(string v)
        {
            //Console.WriteLine(v);
            List<String> results = new List<string>();
            if (!v.Contains('X'))
            {
                results.Add(v);
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    string vTemp = v;
                    results.AddRange(GenerateAdresses(ReplaceFirst(vTemp, "X", i.ToString())));
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