using System;
using System.IO;
using System.Text.RegularExpressions;

namespace day_18
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            long resultPartOne = 0;
            long resultPartTwo = 0;
            foreach (string line in input)
            {
                resultPartOne += EvalatePartOne(line, false);
                resultPartTwo += EvalatePartOne(line, true);
            }

            Console.WriteLine("########## Day 18 2020 ##########");
            Console.WriteLine($"Part one solution: {resultPartOne}");
            Console.WriteLine($"Part two solution: {resultPartTwo}");
            Console.WriteLine("#################################");
        }

        private static long EvalatePartOne(string line, bool isPartTwo)
        {
            while (line.Contains("(")) 
            {
                int level = 0;
                int rightParantesesIndex = 0;
                int leftParantesesIndex = 0;
                bool isLeftSet = false;
                for (int i = line.IndexOf('('); i < line.Length; i++)
                {
                    char currentChar = line[i];
                    if (currentChar == '(')
                    {
                        if (!isLeftSet) 
                        {
                            leftParantesesIndex = i;
                            isLeftSet = true;
                        }

                        level++;
                    }
                    if (currentChar == ')')
                    {
                        level--;
                    }
                    if (level == 0)
                    {
                        rightParantesesIndex = i;
                        break;
                    }
                }

                int phaseStartIndex = leftParantesesIndex + 1;
                int phaseLenght = rightParantesesIndex - leftParantesesIndex - 1;
                long phaseResult = EvalatePartOne(line.Substring(phaseStartIndex, phaseLenght), isPartTwo);
                line = line.Substring(0, leftParantesesIndex) + phaseResult + line.Substring(rightParantesesIndex + 1);
            }

            if (isPartTwo) 
            {
                string regex = @"(\d+) \+ (\d+)";
                Regex rgx = new Regex(regex);
                while (Regex.IsMatch(line, regex))
                {
                    Match m = Regex.Match(line, regex);
                    long t1 = long.Parse(m.Groups[1].Value);
                    long t2 = long.Parse(m.Groups[2].Value);
                    line = rgx.Replace(line, $"{t1 + t2}", 1);
                }
            }

            long counter = 0;
            bool isFirst = true;
            string[] operators = line.Trim().Split(" ");
            for (int i = 0; i < operators.Length; i++)
            {
                string current = operators[i];
                if (current == "+")
                {
                    counter += long.Parse(operators[i+1]);
                }
                else if (current == "*") 
                {
                    counter *= long.Parse(operators[i + 1]);
                }
                else
                {
                    if (isFirst) 
                    {
                        counter = long.Parse(current);
                        isFirst = false;
                    }
                }
            }

            return counter;
        }
    }
}