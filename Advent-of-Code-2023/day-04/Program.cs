namespace day_04;

internal class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

        Console.WriteLine("########## Day 4 2023 ##########");
        Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
        Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
        Console.WriteLine("################################");
    }

    private static int SolvePartOne(string[] input)
    {
        int result = 0;

        foreach(string s in input) 
        {
            string[] gotNumbers = s.Split('|')[1].Split(' ').Where(x=> x!= "").ToArray();
            int[] gotNUmberss = Array.ConvertAll(gotNumbers, s => Int32.Parse(s));
            string[] gotNumbers2 = s.Split('|')[0].Split(':')[1].Split(' ').Where(x => x != "").ToArray();
            int[] winigNum = Array.ConvertAll(gotNumbers2, s => Int32.Parse(s));

            int res = 0;
            foreach(int s2 in gotNUmberss) 
            {
                if (winigNum.Contains(s2)) 
                {
                    if(res == 0) { res = 1; }
                    else { res = res * 2; }
                }
            }

            result += res;
        }

        return result;
    }

    private static object SolvePartTwo(string[] input)
    {
        Dictionary<int, int> pairs = new Dictionary<int, int>();

        foreach (string s in input)
        {
            string[] gotNumbers = s.Split('|')[1].Split(' ').Where(x => x != "").ToArray();
            int[] gotNUmberss = Array.ConvertAll(gotNumbers, s => Int32.Parse(s));
            string[] gotNumbers2 = s.Split('|')[0].Split(':')[1].Split(' ').Where(x => x != "").ToArray();
            int[] winigNum = Array.ConvertAll(gotNumbers2, s => Int32.Parse(s));

            var GameNum = Int32.Parse(s.Split(':')[0].Split(" ").Where(x => x != "").ElementAt(1));

            if (!pairs.ContainsKey(GameNum))
            {
                pairs.Add(GameNum, 1);
            }
            else 
            {
                pairs[GameNum]++;
            }


            for (int copies = 0; copies < pairs[GameNum]; copies++) 
            {
                var current = GameNum;
                foreach (int s2 in gotNUmberss)
                {
                    if (winigNum.Contains(s2))
                    {
                        ++current;
                        if (!pairs.ContainsKey(current))
                        {
                            pairs.Add(current, 1);
                        }
                        else
                        {
                            pairs[current] += 1;
                        }
                    }
                }
            }
        }

        return pairs.Values.Sum(c=>c);
    }
}