using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace day_8
{
    class Program
    {
        private static string wholeWordRegex = @"(\w+)";

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 8 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            int result = 0;
            foreach (string line in input) 
            {
                string numbers = line.Split("|")[1];
                MatchCollection matches = Regex.Matches(numbers, wholeWordRegex);
                for (int i = 0; i < matches.Count; i++)
                {
                    var match = matches[i].Value;
                    if (match.Length == 2 || match.Length == 4 || match.Length == 3 || match.Length == 7)  
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private static object SolvePartTwo(string[] input)
        {
            int result = 0;
            foreach (string line in input)
            {
                string decode = line.Split("|")[0];
                MatchCollection matchesDecode = Regex.Matches(decode, wholeWordRegex);
                string onePattern = "";
                string fourPattern = "";
                string sevenPattern = "";
                string eightPattern = "";
                for (int i = 0; i < matchesDecode.Count; i++)
                {
                    var match = matchesDecode[i].Value;
                    if (match.Length == 2)
                    {
                        onePattern = match;
                    }
                    else if (match.Length == 3)
                    {
                        sevenPattern = match;
                    }
                    else if (match.Length == 4)
                    {
                        fourPattern = match;
                    }
                    else if (match.Length == 7)
                    {
                        eightPattern = match;
                    }
                }

                char[] fourAndEightDiff = eightPattern.ToCharArray().Where(x => !fourPattern.Contains(x)).ToArray();
                string numbers = line.Split("|")[1];
                StringBuilder sb = new StringBuilder();
                MatchCollection matchesNumbers = Regex.Matches(numbers, wholeWordRegex);
                for (int i = 0; i < matchesNumbers.Count; i++)
                {
                    var match = matchesNumbers[i].Value;
                    switch (match.Length) 
                    {
                        case 2:
                            sb.Append("1");
                            break;
                        case 3:
                            sb.Append("7");
                            break;
                        case 4:
                            sb.Append("4");
                            break;
                        case 5:
                            sb.Append(twoThreeOrFive(onePattern, fourAndEightDiff, match));
                            break;
                        case 6:
                            sb.Append(zeroSixOrNine(fourPattern, sevenPattern, match));
                            break;
                        case 7:
                            sb.Append("8");
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }

                result += Int32.Parse(sb.ToString());
            }

            return result;
        }

        private static string twoThreeOrFive(string onePattern, char[] fourAndEightDiff, string numberSequence)
        {
            //if the numberSequence contains all items from the 1 pattern, the numberSequence must be 3
            if (onePattern.ToCharArray().All(x => numberSequence.Contains(x)))
            {
                return "3";
            }
            //if the numberSequence is not 3 and contains difference of 8 and 4, the numberSequence must be 2
            else if (fourAndEightDiff.All(x=> numberSequence.ToCharArray().Contains(x)))
            {
                return "2";
            }
            //else the numberSequence must be 5
            else
            {
                return "5";
            }

        }
        private static string zeroSixOrNine(string fourPattern, string sevenPattern, string numberSequence)
        {
            //if the numberSequence contains all items from the 4 pattern, the numberSequence must be 9 
            if (fourPattern.ToCharArray().All(x => numberSequence.Contains(x)))
            {
                return "9";
            }
            //if the numberSequence is not 9 and contains all items from the 7 pattern, the numberSequence must be 0
            else if (sevenPattern.ToCharArray().All(x => numberSequence.Contains(x)))
            {
                return "0";
            }
            //else the numberSequence must be 6
            else
            {
                return "6";
            }
        }
    }
}
