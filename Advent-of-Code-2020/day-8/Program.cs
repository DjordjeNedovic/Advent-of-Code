using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace day_8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 8 2020 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartTwo(string[] commands)
        {
            int battery = 0;
            List<int> commandsIndexes = new List<int>();
            string regex = @"(?<command>\w+) (\+|-)(?<value>\d+)";
            int i = 0;
            List<int> changedIndexes = new List<int>();
            bool isItChanged = false;
            while (true)
            {
                if (i == commands.Length) 
                {
                    break;
                }

                Match match = Regex.Match(commands[i], regex);
                string command = match.Groups["command"].Value;
                string sign = match.Groups[1].Value;
                int neededSize = Int32.Parse(match.Groups["value"].Value);

                neededSize = sign == "+" ? neededSize : -neededSize;

                if (commandsIndexes.Contains(i))
                {
                    //restart
                    i = 0;
                    isItChanged = false;
                    battery = 0;
                    commandsIndexes.Clear();
                    continue;
                }

                commandsIndexes.Add(i);

                if (command == "acc")
                {
                    battery += neededSize;
                    i++;
                }
                else if (command == "jmp")
                {
                    if (ShouldThisCommandChange(i, changedIndexes, isItChanged, neededSize))
                    {
                        isItChanged = true;
                        changedIndexes.Add(i);
                        i++;

                        continue;
                    }

                    i += neededSize;
                }
                else
                {
                    if (ShouldThisCommandChange(i, changedIndexes, isItChanged, neededSize))
                    {
                        isItChanged = true;
                        changedIndexes.Add(i);
                        i += neededSize;

                        continue;
                    }

                    i++;
                }
            }

            return battery;
        }

        private static bool ShouldThisCommandChange(int i, List<int> changedIndexes, bool isItChanged, int neededSize)
        {
            return !isItChanged && !changedIndexes.Contains(i) && neededSize != 0;
        }

        private static int SolvePartOne(string[] commands)
        {
            int battery = 0;
            List<int> commandsIndexes = new List<int>();
            string regex = @"(?<command>\w+) (\+|-)(?<value>\d+)";
            int i = 0;
            while (true)
            {
                Match match = Regex.Match(commands[i], regex);
                string command = match.Groups["command"].Value;
                string sign = match.Groups[1].Value;
                int neededSize = Int32.Parse(match.Groups["value"].Value);
                neededSize = sign == "+" ? neededSize : -neededSize;
                
                if (commandsIndexes.Contains(i))
                { 
                    break;
                }

                commandsIndexes.Add(i);

                if (command == "acc")
                {
                    battery += neededSize;
                    i++;
                }
                else if (command == "jmp")
                {
                    i += neededSize;
                }
                else
                {
                    i++;
                }
            }

            return battery;
        }
    }
}