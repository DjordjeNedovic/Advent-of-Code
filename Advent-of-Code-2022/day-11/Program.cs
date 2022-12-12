using System.Text.RegularExpressions;

namespace day_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 11 2022 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            List<Monkey> start = GetStartValues(input);
            for (int round = 0; round < 20; round++)
            {
                foreach (Monkey monkey in start)
                {
                    for (int i = 0; i < monkey.Items.Count; i++)
                    {
                        monkey.Inspected++;
                        double newValue = GetNewValue(monkey.Operation, monkey.Items.ElementAt(i));

                        var newWorryLevel = Math.Floor(newValue / 3.0);
                        if (newWorryLevel % monkey.Devided == 0)
                        {
                            start.Where(x => x.Name == monkey.TestSuccess).First().Items.Add((int)newWorryLevel);
                        }
                        else
                        {
                            start.Where(x => x.Name == monkey.TestFailed).First().Items.Add((int)newWorryLevel);
                        }
                    }

                    monkey.Items.Clear();
                }
            }

            return start.Select(x => x.Inspected).OrderByDescending(x => x).Take(2).Aggregate((a, x) => a * x);
        }

        private static object SolvePartTwo(string[] input)
        {
            List<Monkey> start = GetStartValues(input);
            double module = start.Select(x => x.Devided).Aggregate((a, x) => a * x);

            for (int round = 0; round < 10000; round++)
            {
                foreach (Monkey m in start)
                {
                    for (int i = 0; i < m.Items.Count; i++)
                    {
                        m.Inspected++;
                        double newValue = GetNewValue(m.Operation, m.Items.ElementAt(i));

                        var newWorryLevel = newValue % module;
                        if (newWorryLevel % m.Devided == 0)
                        {
                            start.Where(x => x.Name == m.TestSuccess).First().Items.Add((int)newWorryLevel);
                        }
                        else
                        {
                            start.Where(x => x.Name == m.TestFailed).First().Items.Add((int)newWorryLevel);
                        }
                    }

                    m.Items.Clear();
                }
            }

            return start.Select(x => x.Inspected).OrderByDescending(x => x).Take(2).Aggregate((a, x) => a * x);
        }

        private static double GetNewValue(string operation, double v)
        {
            string regex = @"old (\*|\+) (\d+|old)";
            Match match = Regex.Match(operation, regex);
            if (match.Success)
            {
                double secound = 0;
                if (match.Groups[2].Value == "old")
                {
                    secound = v;
                }
                else
                {
                    secound = int.Parse(match.Groups[2].Value);
                }

                if (match.Groups[1].Value == "+")
                {
                    return secound + v;
                }
                else
                {
                    return secound * v;
                }
            }

            return 0;
        }

        private static List<Monkey> GetStartValues(string[] input)
        {
            List<Monkey> result = new List<Monkey>();
            Monkey currentMonkey = null;
            string regexForNumbers = @"(\d+)";
            foreach (string line in input)
            {
                var trimmedLine = line.Trim();

                if (trimmedLine.Equals(""))
                {
                    result.Add(currentMonkey);
                    currentMonkey = null;
                    continue;
                }

                if (trimmedLine.StartsWith("Monkey"))
                {
                    currentMonkey = new Monkey();
                    Match match = Regex.Match(line, regexForNumbers);
                    currentMonkey.Name = Int32.Parse(match.Groups[1].Value);
                    currentMonkey.Items = new List<double>();
                    currentMonkey.Inspected = 0;
                    continue;
                }

                if (trimmedLine.StartsWith("Starting items"))
                {
                    foreach (var t in trimmedLine.Split(':')[1].Split(','))
                    {
                        currentMonkey.Items.Add(int.Parse(t));
                    }

                    continue;
                }

                if (trimmedLine.StartsWith("Operation"))
                {
                    currentMonkey.Operation = trimmedLine;
                    continue;
                }

                if (trimmedLine.StartsWith("Test"))
                {
                    Match match = Regex.Match(trimmedLine, regexForNumbers);
                    currentMonkey.Devided = Int32.Parse(match.Groups[1].Value);
                    continue;
                }

                if (trimmedLine.StartsWith("If true"))
                {
                    Match match = Regex.Match(trimmedLine, regexForNumbers);
                    currentMonkey.TestSuccess = Int32.Parse(match.Groups[1].Value);
                    continue;
                }
                else
                {
                    Match match = Regex.Match(trimmedLine, regexForNumbers);
                    currentMonkey.TestFailed = Int32.Parse(match.Groups[1].Value);
                    continue;
                }
            }

            result.Add(currentMonkey);

            return result;
        }
    }

    internal class Monkey
    {
        public int Name { get; set; }
        public List<double> Items { get; set; }
        public string Operation { get; set; }
        public double Devided { get; set; }
        public int TestFailed { get; set; }
        public int TestSuccess { get; set; }
        public double Inspected { get; set; }
    }
}
