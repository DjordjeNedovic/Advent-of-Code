string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

Console.WriteLine("########## Day 7 2023 ##########");
Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
Console.WriteLine("################################");

long SolvePartOne(string[] input)
{
    Dictionary<string, (int, CombinationType)> combinations = new Dictionary<string, (int, CombinationType)>();

    foreach (string s in input)
    {
        var t = AddCombination(s);
        combinations.Add(t.hand, (t.Item2.handStrength, t.Item2.combinationType));
    }

    int solution = 0;
    int index = 1;
    var grouped = combinations.OrderBy(x=>x.Value.Item2).GroupBy(x => (int)x.Value.Item2);
    foreach ( var group in grouped )
    {
        var newgroup = group.Select(x=>x.Key).ToList();
        newgroup.Sort((a, b) => CustomSort(a, b, true));

        foreach (var item in newgroup) 
        {
            solution += group.First(group => group.Key == item).Value.Item1 * index++;
        }
    }

    return solution;
}

long SolvePartTwo(string[] input)
{
    Dictionary<string, (int, CombinationType)> combinations = new Dictionary<string, (int, CombinationType)>();

    foreach (string c in input)
    {
        var pair = c.Split(' ');
        string hand = pair[0];

        if (hand.Contains('J'))
        {
            int numberOfJokers = hand.Count(x => x == 'J');
            if (numberOfJokers == 4 || numberOfJokers == 5)
            {
                combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.AllSame));
            }
            else if (numberOfJokers == 3)
            {
                var gr = hand.GroupBy(x => x).Count();
                if (gr == 3)
                {
                    combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.Poker));
                }
                else
                {
                    combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.AllSame));
                }
            }
            else if (numberOfJokers == 2)
            {
                var gr = hand.GroupBy(x => x).Count();
                if (gr == 4)
                {
                    combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.Triling));
                }
                else if (gr == 3)
                {
                    combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.Poker));
                }
                else
                {
                    combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.AllSame));
                }
            }
            else 
            {
                var gr = hand.GroupBy(x => x).Count();
                if (gr == 2)
                {
                    combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.AllSame));
                }
                else if (gr == 3)
                {
                    var sorted = hand.GroupBy(x => x).OrderBy(x => x.Count());
                    if (sorted.ElementAt(1).Count() == 2)
                    {
                        combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.FullHouse));
                    }
                    else 
                    {
                        combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.Poker));
                    }

                }
                else if (gr == 4)
                {
                    combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.Triling));
                }
                else 
                {
                    combinations.Add(hand, (Int32.Parse(pair[1]), CombinationType.OnePair));
                }
            }
        }
        else 
        {
            var newValue = AddCombination(c);
            combinations.Add(newValue.hand, (newValue.Item2.handStrength, newValue.Item2.combinationType));
        }
    }

    int solution = 0;
    int index = 1;
    var grouped = combinations.OrderBy(x => x.Value.Item2).GroupBy(x => (int)x.Value.Item2);
    foreach (var group in grouped)
    {
        var newgroup = group.Select(x => x.Key).ToList();
        newgroup.Sort((a, b) => CustomSort(a, b, false));

        foreach (var item in newgroup)
        {
            solution += group.First(group => group.Key == item).Value.Item1 * index++;
        }
    }

    return solution;
}

int CalculateCardStrength(char card, bool isPartOne) 
{
    int strength = 0;
    switch(card) 
    {
        case 'A':
            return 14;
        case 'K':
            return 13;
        case 'Q':
            return 12;
        case 'J':
            return isPartOne ? 11 : 0;
        case 'T':
            return 10;
        default: 
            return (int)Char.GetNumericValue(card);
    }
}

static (string hand, (int handStrength, CombinationType combinationType)) AddCombination(string s)
{
    var parts = s.Split(' ');
    var cardCombinations = parts[0].GroupBy(x => x);
    if (cardCombinations.Count() == 1)
    {
        return (parts[0], (Int32.Parse(parts[1]), CombinationType.AllSame));
    }
    else if (cardCombinations.Count() == 2)
    {
        var sorted = cardCombinations.OrderBy(x => x.Count());
        if (sorted.ElementAt(1).Count() == 4)
        {
            return (parts[0], (Int32.Parse(parts[1]), CombinationType.Poker));
        }
        else
        {
            return (parts[0], (Int32.Parse(parts[1]), CombinationType.FullHouse));
        }
    }
    else if (cardCombinations.Count() == 3)
    {
        var sorted = cardCombinations.OrderBy(x => x.Count());
        var max = sorted.Max(x => x.Count());
        if (max == 3)
        {
            return (parts[0], (Int32.Parse(parts[1]), CombinationType.Triling));
        }
        else
        {
            return (parts[0], (Int32.Parse(parts[1]), CombinationType.TwoPairs));
        }
    }
    else if (cardCombinations.Count() == 4)
    {
        return (parts[0], (Int32.Parse(parts[1]), CombinationType.OnePair));
    }
    else
    {
        return (parts[0], (Int32.Parse(parts[1]), CombinationType.HighCard));
    }
}

int CustomSort(string a, string b, bool isPartOne)
{
    for (int j = 0; j < a.Length; j++)
    {
        var ac = CalculateCardStrength(a[j], isPartOne);
        var bc = CalculateCardStrength(b[j], isPartOne);

        if (ac > bc)
            return 1;
        else if (ac < bc)
            return -1;
    }

    return 0;
}

enum CombinationType 
{
    Unknown = 0,
    HighCard = 1,
    OnePair = 2,
    TwoPairs = 3,
    Triling = 4,
    FullHouse = 5,
    Poker = 6,
    AllSame = 7,
}