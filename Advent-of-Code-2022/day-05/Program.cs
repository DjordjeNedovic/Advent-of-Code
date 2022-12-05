using System.Text.RegularExpressions;

namespace day_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 5 2022 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            Stack<string> s1 = new Stack<string>(new[] { "S", "T", "H", "F", "W", "R" });
            Stack<string> s2 = new Stack<string>(new[] { "S", "G", "D", "Q", "W" });
            Stack<string> s3 = new Stack<string>(new[] { "B", "T", "W" });
            Stack<string> s4 = new Stack<string>(new[] { "D", "R", "W", "T", "N", "Q", "Z", "J" });
            Stack<string> s5 = new Stack<string>(new[] { "F", "B", "H", "G", "L", "V", "T", "Z" });
            Stack<string> s6 = new Stack<string>(new[] { "L", "P", "T", "C", "V", "B", "S", "G" });
            Stack<string> s7 = new Stack<string>(new[] { "Z", "B", "R", "T", "W", "G", "P" });
            Stack<string> s8 = new Stack<string>(new[] { "N", "G", "M", "T", "C", "J", "R" });
            Stack<string> s9 = new Stack<string>(new[] { "L", "G", "B", "W" });

            Dictionary<int, Stack<string>> dic = new Dictionary<int, Stack<string>>();
            dic.Add(1, s1);
            dic.Add(2, s2);
            dic.Add(3, s3);
            dic.Add(4, s4);
            dic.Add(5, s5);
            dic.Add(6, s6);
            dic.Add(7, s7);
            dic.Add(8, s8);
            dic.Add(9, s9);

            string regex = @"move (\d+) from (\d+) to (\d+)";
            foreach (string line in input)
            {
                Match match = Regex.Match(line, regex);
                var amount = Int32.Parse(match.Groups[1].Value);
                var from = Int32.Parse(match.Groups[2].Value);
                var to = Int32.Parse(match.Groups[3].Value);

                var fromStack = dic[from];
                var toStack = dic[to];

                for (int i = 0; i < amount; i++)
                {
                    var t = fromStack.Pop();
                    toStack.Push(t);
                }
            }

            string ttt = "";
            foreach (var t in dic)
            {
                ttt += t.Value.Pop();
            }

            return ttt;
        }

        private static object SolvePartTwo(string[] input)
        {
            Stack<string> s1 = new Stack<string>(new[] { "S", "T", "H", "F", "W", "R" });
            Stack<string> s2 = new Stack<string>(new[] { "S", "G", "D", "Q", "W" });
            Stack<string> s3 = new Stack<string>(new[] { "B", "T", "W" });
            Stack<string> s4 = new Stack<string>(new[] { "D", "R", "W", "T", "N", "Q", "Z", "J" });
            Stack<string> s5 = new Stack<string>(new[] { "F", "B", "H", "G", "L", "V", "T", "Z" });
            Stack<string> s6 = new Stack<string>(new[] { "L", "P", "T", "C", "V", "B", "S", "G" });
            Stack<string> s7 = new Stack<string>(new[] { "Z", "B", "R", "T", "W", "G", "P" });
            Stack<string> s8 = new Stack<string>(new[] { "N", "G", "M", "T", "C", "J", "R" });
            Stack<string> s9 = new Stack<string>(new[] { "L", "G", "B", "W" });

            Dictionary<int, Stack<string>> dic = new Dictionary<int, Stack<string>>();
            dic.Add(1, s1);
            dic.Add(2, s2);
            dic.Add(3, s3);
            dic.Add(4, s4);
            dic.Add(5, s5);
            dic.Add(6, s6);
            dic.Add(7, s7);
            dic.Add(8, s8);
            dic.Add(9, s9);

            string regex = @"move (\d+) from (\d+) to (\d+)";
            foreach (string line in input)
            {
                Match match = Regex.Match(line, regex);
                var amount = Int32.Parse(match.Groups[1].Value);
                var from = Int32.Parse(match.Groups[2].Value);
                var to = Int32.Parse(match.Groups[3].Value);

                var fromStack = dic[from];
                var toStack = dic[to];

                List<string> strings = new List<string>();
                for (int i = 0; i < amount; i++)
                {
                    var t = fromStack.Pop();
                    strings.Add(t);
                }

                while (strings.Count != 0)
                {
                    toStack.Push(strings.Last());

                    strings.RemoveAt(strings.Count - 1);
                }
            }

            string ttt = "";
            foreach (var t in dic)
            {
                ttt += t.Value.Pop();
            }

            return ttt;
        }
    }
}
