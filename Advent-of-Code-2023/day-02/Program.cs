using System.Text.RegularExpressions;

namespace day_02;

internal class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

        Console.WriteLine("########## Day 2 2023 ##########");
        Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
        Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
        Console.WriteLine("################################");
    }

    private static object SolvePartOne(string[] input)
    {
        string regexPattern = @"((?<redColor>\d+) red)|((?<greenColor>\d+) green)|((?<blueColor>\d+) blue)";
        int result = 0;
        foreach (var line in input) 
        {
            bool isItPosible = true;
            MatchCollection mc = Regex.Matches(line, regexPattern);
            for (int i = 0; i < mc.Count; i++) 
            {
                var color = mc[i].Value;
                GroupCollection groups = mc[i].Groups;
                if (color.Contains("red"))
                {
                    var num = Int32.Parse(groups["redColor"].Value);
                    if (num > 12) 
                    {
                        isItPosible=false;
                        break;
                    }
                }
                else if (color.Contains("green"))
                {
                    var num = Int32.Parse(groups["greenColor"].Value);
                    if (num > 13)
                    {
                        isItPosible = false;
                        break;
                    }
                }
                else if (color.Contains("blue"))
                {
                    var num = Int32.Parse(groups["blueColor"].Value);
                    if (num > 14)
                    {
                        isItPosible = false;
                        break;
                    }
                }
            }

            if (isItPosible) 
            {
                var gamePattern = @"Game (?<gameId>\d+)";
                MatchCollection gmc = Regex.Matches(line, gamePattern);
                result += Int32.Parse(gmc[0].Groups["gameId"].Value);
            }
        }

        return result;
    }

    private static object SolvePartTwo(string[] input)
    {
        string regexPattern = @"((?<redColor>\d+) red)|((?<greenColor>\d+) green)|((?<blueColor>\d+) blue)";
        int result = 0;
        foreach (var line in input)
        {
            int maxRed = 0;
            int maxGreen = 0;
            int maxBlue = 0;
            MatchCollection mc = Regex.Matches(line, regexPattern);
            for (int i = 0; i < mc.Count; i++)
            {
                var color = mc[i].Value;
                GroupCollection groups = mc[i].Groups;
                if (color.Contains("red"))
                {
                    var num = Int32.Parse(groups["redColor"].Value);
                    if (num > maxRed)
                    {
                        maxRed = num;
                    }
                }
                else if (color.Contains("green"))
                {
                    var num = Int32.Parse(groups["greenColor"].Value);
                    if (num > maxGreen)
                    {
                        maxGreen = num;
                    }
                }
                else if (color.Contains("blue"))
                {
                    var num = Int32.Parse(groups["blueColor"].Value);
                    if (num > maxBlue)
                    {
                        maxBlue = num;
                    }
                }
            }

            result += (maxRed * maxGreen * maxBlue);
        }

        return result;
    }
}