using System.Text.RegularExpressions;

string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
Dictionary<string, (string, string)> map = PopulateMap(input);

Console.WriteLine("########## Day 8 2023 ##########");
Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
Console.WriteLine("################################");

long SolvePartOne(string[] input)
{
    return SolveSetp(input, map, "AAA", true);
}

long SolvePartTwo(string[] input)
{
    var t = map.Select(x => x.Key).Where(x => x.Last() == 'A').ToList();
    long[] res = new long[t.Count];

    for (int i=0; i<t.Count;i++)
    {
        string current = t[i];
        long number = SolveSetp(input, map, current, false);
        res[i] = number;
    }

    return res.Aggregate(1L, (a,b) => Lcm(a,b));
}

static long SolveSetp(string[] input, Dictionary<string, (string, string)> dic, string current, bool isPartOne)
{
    long steps = 0;
    bool isFound = false;
    while (true)
    {
        foreach (char c in input[0])
        {
            steps++;
            if (c == 'L')
            {
                current = dic[current].Item1;
            }
            else
            {
                current = dic[current].Item2;
            }

            if (isPartOne)
            {
                if (current.EndsWith("ZZZ"))
                {
                    isFound = true;
                    break;
                }
            }
            else 
            {
                if (current.EndsWith("Z"))
                {
                    isFound = true;
                    break;
                }
            }
        }

        if (isFound)
        { break; }
    }

    return steps;
}

static Dictionary<string, (string, string)> PopulateMap(string[] input)
{
    Dictionary<string, (string, string)> map = new Dictionary<string, (string, string)>();
    string pattern = @"([A-Z]{3})";
    for (int i = 2; i < input.Length; i++)
    {
        MatchCollection mc = Regex.Matches(input[i], pattern);
        var node = mc[0];
        var left = mc[1];
        var right = mc[2];
        map.Add(node.Value, (left.Value, right.Value));
    }

    return map;
}

// thanks to follwoting answer:
//https://stackoverflow.com/a/20824923/8764150
// formula to calculate Lcm is:
long Gcf(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

long Lcm(long a, long b)
{
    return (a / Gcf(a, b)) * b;
}