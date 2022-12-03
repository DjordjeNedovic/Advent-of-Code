namespace day_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 1 2022 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(string[] input)
        {
            int max = 0;
            int current = 0;
            foreach (string line in input) 
            {
                if (line == "") 
                {
                    if (max < current)
                        max = current;

                    current = 0;
                    continue;
                }
                
                current += Int32.Parse(line);
            }

            return max;
        }

        private static int SolvePartTwo(string[] input)
        {
            List<int> food = new List<int>();
            int current = 0;
            foreach (string line in input)
            {
                if (line == "")
                {
                    food.Add(current);

                    current = 0;
                    continue;
                }

                current += Int32.Parse(line);
            }

            food.Sort();

            return food.Skip(Math.Max(0, food.Count() - 3)).Sum();
        }
    }
}
