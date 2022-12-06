namespace day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("########## Day 5 2018 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(string input)
        {
            int index = 0;
            while (index < input.Length-1) 
            {
                var t1 = input[index].ToString();
                var t2 = input[index+1].ToString();
                if (t1 != t2 &&t1.ToLower() == t2.ToLower())
                {
                    var g = input.ToCharArray().ToList();
                    g.RemoveAt(index + 1);
                    g.RemoveAt(index);

                    string newString = new string(g.ToArray());

                    input = newString;
                    index -= 2;
                    if (index <= 0) 
                    {
                        index = 0;
                    }
                }
                else 
                {
                    index++;
                }
            }

            return input.Length;
        }

        private static object SolvePartTwo(string input)
        {
            Dictionary<string, int> pairs = new Dictionary<string, int>();

            List<string> charUsed = input.Select(c => c.ToString().ToLower()).Distinct().ToList();
            foreach(var currentChar in charUsed)
            {
                var newInput = input.Replace(currentChar, "");
                newInput = newInput.Replace(currentChar.ToUpper(), "");

                pairs.Add(currentChar, SolvePartOne(newInput));
            }

            return pairs.Aggregate((l, r) => l.Value < r.Value ? l : r).Value;
        }
    }
}
