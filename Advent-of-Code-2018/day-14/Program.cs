string input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
int target = int.Parse(input);

Console.WriteLine("########## Day 14 2018 ##########");
Console.WriteLine($"Part one solution: {SolvePartOne(target)}");
Console.WriteLine($"Part one solution: {SolvePartTwo(input)}");
Console.WriteLine("################################");

static string SolvePartOne(int target)
{
    int elfOne = 0;
    int elfTwo = 1;
    var recepies = new List<int>() { 3, 7 }; 
    while(recepies.Count < target + 10)
    {
        var recOne = recepies.ElementAt(elfOne);
        var recTwo = recepies.ElementAt(elfTwo);
        int newRecepie = recOne + recTwo;
        if (newRecepie > 9)
        {
            foreach (char digit in newRecepie.ToString())
            {
                recepies.Add(digit - '0');
            }
        }
        else 
        {
            recepies.Add(newRecepie);
        }

        elfOne = (elfOne + 1 + recOne) % recepies.Count();
        elfTwo = (elfTwo + 1 + recTwo) % recepies.Count();
    }

    return string.Join("", recepies.Slice(target, 10).ToArray());
}


static int SolvePartTwo(string input)
{
    int elfOne = 0;
    int elfTwo = 1;
    var recepies = new List<int>() { 3, 7 };
    int lastlookingDigit = input.ToCharArray().Last() - '0';
    bool run = true;
    while (run)
    {
        var recOne = recepies.ElementAt(elfOne);
        var recTwo = recepies.ElementAt(elfTwo);
        int newRecepie = recOne + recTwo;
        if (newRecepie > 9)
        {
            foreach (char digit in newRecepie.ToString())
            {
                int d = digit - '0';
                recepies.Add(d);
                if (d == lastlookingDigit)
                {
                    string result = string.Join("", recepies.TakeLast(input.Length));
                    if (result == input)
                    {
                        run = false;
                        break;
                    }
                }
            }
        }
        else
        {
            recepies.Add(newRecepie);
            if (newRecepie == lastlookingDigit)
            {
                string result = string.Join("", recepies.TakeLast(input.Length));
                if (result == input)
                {
                    run = false;
                    break;
                }
            }
        }

        elfOne = (elfOne + 1 + recOne) % recepies.Count();
        elfTwo = (elfTwo + 1 + recTwo) % recepies.Count();
    }

    return recepies.Count - input.Length;
}