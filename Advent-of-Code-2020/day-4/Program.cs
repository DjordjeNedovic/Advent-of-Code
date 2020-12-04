using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Split(Environment.NewLine);
            List<string> candidates = FindPassowrdsCandidates(input);

            Console.WriteLine($"Part one: there are {SolvePartOne(candidates)} valid passports");

            Console.WriteLine($"Part two: there are {SolvePartTow(candidates)} valid passports");

        }

        private static int SolvePartOne(List<string> candidates)
        {
            int count = 0;
            foreach (string password in candidates)
            {
                if (DoesPasswordHaveAllFields(password))
                {
                    count++;
                }
            }

            return count;
        }
        
        private static int SolvePartTow(List<string> candidates)
        {
            int count = 0;
            foreach (string password in candidates)
            {
                if (IsDataValid(password))
                {
                    count++;
                }
            }

            return count;
        }

        private static List<string> FindPassowrdsCandidates(string[] input)
        {
            List<string> passwords = new List<string>();
            string passwordData = String.Empty;
            foreach (string data in input)
            {
                if (data == String.Empty)
                {
                    passwordData.Trim();
                    passwords.Add(passwordData);
                    passwordData = String.Empty;
                    continue;
                }

                passwordData = passwordData + " " + data;
            }

            passwords.Add(passwordData);

            return passwords.Where(x => x.Split(" ").Length > 7).Select(x => x).ToList(); ;
        }

        private static bool IsDataValid(string password)
        {
            List<string> data = new List<string>() { @"byr:(19[2-9][0-9]|200[0-2])",
                                                     @"iyr:(201[0-9]|2020)",
                                                     @"eyr:(202[0-9]|2030|)",
                                                     @"hgt:((1[5-8][0-9]|19[0-3])cm)|hgt:(7[0-6]|59|6[0-9])in",
                                                     @"hcl:(#[0-9a-f]{6})",
                                                     @"ecl:(amb|blu|brn|gry|grn|hzl|oth)",
                                                     @"pid:(\d{9}\b)" };

            foreach (string regex in data)
            {
                MatchCollection matches = Regex.Matches(password, regex);
                if (matches.Count == 0) 
                {
                    return false;
                }
            }
            
            return true;
        }

        private static bool DoesPasswordHaveAllFields(string password)
        {
            List<string> data = new List<string>() { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };
            foreach (string nData in data) 
            {
                if (!password.Contains(nData)) 
                {
                    return false;
                }
            }

            return true;
        }
    }
}
