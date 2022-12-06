namespace day_02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("########## Day 2 2018 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(string[] inputs)
        {
            Dictionary<int, int> checksumContainer = new Dictionary<int, int>();
            foreach (string input in inputs)
            {
                var checksum = input.GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).Where(c => c.Count > 1).ToList();
                if (checksum.Count != 0)
                {
                    var charGroup = checksum.GroupBy(x => x.Count).ToList();
                    foreach (var group in charGroup)
                    {
                        if (checksumContainer.ContainsKey(group.Key))
                        {
                            checksumContainer[group.Key] += 1;
                        }
                        else
                        {
                            checksumContainer.Add(group.Key, 1);
                        }
                    }
                }
            }

            return checksumContainer.Select(x => x.Value).Aggregate(1, (x, y) => x * y);
        }

        private static string SolvePartTwo(string[] input)
        {
            string text = String.Empty;
            int iteration = 0;
            foreach (string t in input)
            {
                iteration++;
                for (int i = iteration + 1; i < input.Length; i++) 
                {
                    int diffNumber = NumberOfDifChars(t, input[i]);
                    if (diffNumber == 1) 
                    {
                        int difIndex = DiffIndex(t, input[i]);
                        text = t.Remove(difIndex, 1);
                    }
                }
            }

            return text;
        }

        public static int DiffIndex(string t, string v) 
        {
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] != v[i])
                { 
                    return i;
                }
            }

            return 0;
        }

        private static int NumberOfDifChars(string t, string v)
        {
            int counter = 0;
            for (int i = 0; i < t.Length; i++) 
            {
                if (t[i] != v[i]) 
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
