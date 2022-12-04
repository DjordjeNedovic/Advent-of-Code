using System.Text;

namespace day_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 4 2022 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            List<string> tt = new List<string>();
            foreach (string line in input) 
            {
                var first = line.Substring(0, (int)(line.Length / 2));
                var last = line.Substring((int)(line.Length / 2), (int)(line.Length / 2));

                byte[] fistBytes = Encoding.ASCII.GetBytes(first);
                byte[] lastBytes = Encoding.ASCII.GetBytes(last);

                string str = System.Text.Encoding.ASCII.GetString(fistBytes.Intersect(lastBytes).ToArray());
                tt.Add(str);
            }

            return CalculatePriority(tt);
        }

        private static object CalculatePriority(List<string> tt)
        {
            string small = "abcdefghijklmnopqrstuvwxyz";
            int sum = 0;
            foreach (string t in tt) 
            {
                int curr = 0;

                char stt = char.Parse(t);

                if (Char.IsUpper(stt)) 
                {
                    curr += 26;
                }

                stt = char.ToLower(stt);
                var gg = small.IndexOf(stt)+1;
                curr = curr + gg;

                sum += curr;
            }

            return sum;
        }

        private static object SolvePartTwo(string[] input)
        {
            List<string> strings = new List<string>();
            List<string> parts = new List<string>();    
            foreach (string line in input) 
            {
                strings.Add(line);
                if (strings.Count==3) 
                {
                    byte[] fistBytes = Encoding.ASCII.GetBytes(strings.ElementAt(0));
                    byte[] secoundByters = Encoding.ASCII.GetBytes(strings.ElementAt(1));
                    byte[] thirdBydes = Encoding.ASCII.GetBytes(strings.ElementAt(2));

                    parts.Add(Encoding.ASCII.GetString(fistBytes.Intersect(secoundByters).Intersect(thirdBydes).ToArray()));

                    strings.Clear();
                    
                }

            }
            return CalculatePriority(parts);
        }
    }
}
