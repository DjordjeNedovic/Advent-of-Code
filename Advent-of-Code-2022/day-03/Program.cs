using System.Text;

namespace day_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 3 2022 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            List<string> priorities = new List<string>();
            foreach (string line in input) 
            {
                var first = line.Substring(0, (line.Length / 2));
                var last = line.Substring((line.Length / 2), (line.Length / 2));

                byte[] fistBytes = Encoding.ASCII.GetBytes(first);
                byte[] lastBytes = Encoding.ASCII.GetBytes(last);

                priorities.Add(Encoding.ASCII.GetString(fistBytes.Intersect(lastBytes).ToArray()));
            }

            return CalculatePriority(priorities);
        }

        private static object SolvePartTwo(string[] input)
        {
            List<string> groupOfRucksacks = new List<string>();
            List<string> priorities = new List<string>();    
            foreach (string line in input) 
            {
                groupOfRucksacks.Add(line);
                if (groupOfRucksacks.Count==3) 
                {
                    byte[] fistBytes = Encoding.ASCII.GetBytes(groupOfRucksacks.ElementAt(0));
                    byte[] secoundByters = Encoding.ASCII.GetBytes(groupOfRucksacks.ElementAt(1));
                    byte[] thirdBydes = Encoding.ASCII.GetBytes(groupOfRucksacks.ElementAt(2));

                    priorities.Add(Encoding.ASCII.GetString(fistBytes.Intersect(secoundByters).Intersect(thirdBydes).ToArray()));

                    groupOfRucksacks.Clear();
                }
            }

            return CalculatePriority(priorities);
        }

        private static object CalculatePriority(List<string> priorities)
        {
            string letters = "abcdefghijklmnopqrstuvwxyz";
            int sum = 0;
            foreach (string priority in priorities)
            {
                char letter = char.Parse(priority);

                if (Char.IsUpper(letter))
                {
                    sum += 26;
                }

                letter = char.ToLower(letter);
                sum = sum + letters.IndexOf(letter) + 1;
            }

            return sum;
        }
    }
}
