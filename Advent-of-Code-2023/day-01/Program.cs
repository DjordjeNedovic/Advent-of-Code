using System.Text.RegularExpressions;

namespace day_01;

internal class Program
{
    private static readonly List<string> numbers = new List<string>() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

        Console.WriteLine("########## Day 1 2023 ##########");
        Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
        Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
        Console.WriteLine("################################");
    }

    private static int SolvePartOne(string[] input)
    {
        string regexPattern = @"\d+";
        int result = 0;
        foreach (string line in input)
        {
            result += Int32.Parse(JustNumbers(regexPattern, line));
        }

        return result;
    }


    private static int SolvePartTwo(string[] input)
    {
        string regexPattern = @"\d+";
        int result = 0;

        foreach (string line in input)
        {
            string one = "";
            string two = "";

            Dictionary<string, List<int>> foundWords = FindNumbersInString(numbers, line);

            MatchCollection mc = Regex.Matches(line, regexPattern);
            if (foundWords.Count == 0) 
            {
                result += Int32.Parse(JustNumbers(regexPattern, line));
            }
            else if (mc.Count != 0)
            {
                var numRes = JustNumbers(regexPattern, line);
                int postionOfOneDigit = mc[0].Index;
                int postionOfSecoundNumber = mc[mc.Count - 1].Index;

                string smallestNumber, largetsNumber;
                int smallestNumberPosition, largetsNumberPosition;
                GetPositionsOfWordsInString(foundWords, out smallestNumber, out largetsNumber, out smallestNumberPosition, out largetsNumberPosition);

                one = postionOfOneDigit < smallestNumberPosition ? numRes.ElementAt(0).ToString() : Case(smallestNumber);
                two = postionOfSecoundNumber > largetsNumberPosition ? numRes.ElementAt(1).ToString() : Case(largetsNumber);

                result += Int32.Parse(one + two);
            }
            else
            {
                string smallestNumber, largetsNumber;
                int smallestNumberPosition, largetsNumberPosition;
                GetPositionsOfWordsInString(foundWords, out smallestNumber, out largetsNumber, out smallestNumberPosition, out largetsNumberPosition);

                result += Int32.Parse($"{Case(smallestNumber) + Case(largetsNumber)}");
            }
        }

        return result;
    }

    private static void GetPositionsOfWordsInString(Dictionary<string, List<int>> foundWords, out string smallestNumber, out string largetsNumber, out int smallestNumberPosition, out int largetsNumberPosition)
    {
        smallestNumber = "";
        smallestNumberPosition = -1;

        largetsNumber = "";
        largetsNumberPosition = -1;

        //find fist oocurens of number
        foreach (KeyValuePair<string, List<int>> kvp in foundWords)
        {
            var gmin = kvp.Value.Min();
            var gmax = kvp.Value.Max();

            if (smallestNumberPosition == -1 || gmin < smallestNumberPosition)
            {
                smallestNumber = kvp.Key;
                smallestNumberPosition = gmin;
            }

            if (largetsNumberPosition == -1 || gmax > largetsNumberPosition)
            {
                largetsNumber = kvp.Key;
                largetsNumberPosition = gmax;
            }
        }
    }

    private static string JustNumbers(string regexPattern, string line)
    {
        MatchCollection mc = Regex.Matches(line, regexPattern);

        var firstMatch = mc.First().Value;
        var firstDigit = firstMatch.Length > 1 ? firstMatch = firstMatch.ToArray().ElementAt(0).ToString() : firstMatch;

        var lastMatch = mc.Last().Value;
        var lastDigit = lastMatch.Length > 0 ? lastMatch = lastMatch.ToArray().ElementAt(lastMatch.Length - 1).ToString() : lastMatch;

        return $"{firstDigit}{lastDigit}";
    }

    private static Dictionary<string, List<int>> FindNumbersInString(List<string> numbers, string line)
    {
        Dictionary<string, List<int>> foundWords = new Dictionary<string, List<int>>();

        foreach (string num in numbers)
        {
            if (line.Contains(num))
            {
                MatchCollection mc = Regex.Matches(line, num);
                foundWords.Add(num, new List<int>());

                for (int i = 0; i <mc.Count;i++) 
                {
                    foundWords[num].Add(mc[i].Index);
                }
            }
        }

        return foundWords;
    }

    private static string Case(string word) 
    {
        switch (word) 
        {
            case "one": return "1";
            case "two": return "2";
            case "three": return "3";
            case "four": return "4";
            case "five": return "5";
            case "six": return "6";
            case "seven": return "7";
            case "eight": return "8";
            case "nine": return "9";
            default: throw new ArgumentException("Something wen wrong");
        }
    }
}
