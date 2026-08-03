using System.Text;


Dictionary<int, char> plants;
Dictionary<string, char> rules;

SetInitValues(out plants, out rules);

Console.WriteLine("########## Day 11 2018 ##########");
Console.WriteLine($"Part one solution: {PartOne(plants, rules)}");
SetInitValues(out plants, out rules);
Console.WriteLine($"Part two solution: {PartTwo(plants, rules)}");
Console.WriteLine("################################");

string GenerateState(Dictionary<int, char> plants, int key)
{
    StringBuilder sb = new StringBuilder();
    sb.Append(plants.ContainsKey(key - 2) ? plants[key - 2] : '.');
    sb.Append(plants.ContainsKey(key - 1) ? plants[key - 1] : '.');
    sb.Append(plants.ContainsKey(key) ? plants[key] : '.');
    sb.Append(plants.ContainsKey(key + 1) ? plants[key + 1] : '.');
    sb.Append(plants.ContainsKey(key + 2) ? plants[key + 2] : '.');
    return sb.ToString();
}

static void SetInitValues(out Dictionary<int, char> plants, out Dictionary<string, char> rules)
{
    string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

    string initialState = input[0].Split(": ")[1];

    plants = new Dictionary<int, char>();
    for (int i = 0; i < initialState.Length; i++)
    {
        if (initialState[i] == '#')
            plants[i] = '#';
        else
            plants[i] = '.';
    }

    rules = new Dictionary<string, char>();
    for (int i = 2; i < input.Length; i++)
    {
        if (string.IsNullOrWhiteSpace(input[i]))
            continue;

        var parts = input[i].Split(" => ");
        rules[parts[0]] = parts[1][0];
    }
}

int PartOne(Dictionary<int, char> plants, Dictionary<string, char> rules)
{
    for (int i = 0; i < 20; i++)
    {
        var newGeneration = new Dictionary<int, char>();
        int min = plants.Keys.Min() - 2;
        int max = plants.Keys.Max() + 2;

        for (int j = min; j < max; j++)
        {
            newGeneration[j] = '.';
            string state = GenerateState(plants, j);
            if (rules.ContainsKey(state) && rules[state] == '#')
            {
                newGeneration[j] = '#';
            }
        }

        plants = newGeneration;
    }

    return plants.Where(x => x.Value == '#').Select(x => x.Key).Sum();
}

long PartTwo(Dictionary<int, char> plants, Dictionary<string, char> rules)
{
    const int requiredStableCount = 5;
    const long target = 50000000000;

    long lastSum = plants.Where(x => x.Value == '#').Select(x => (long)x.Key).Sum();
    long lastDiff = 0;
    int stableCount = 0;
    long generation = 0;

    while (true)
    {
        var newGeneration = new Dictionary<int, char>();
        int min = plants.Keys.Min() - 2;
        int max = plants.Keys.Max() + 2;

        for (int j = min; j < max; j++)
        {
            newGeneration[j] = '.';
            string state = GenerateState(plants, j);
            if (rules.ContainsKey(state) && rules[state] == '#')
            {
                newGeneration[j] = '#';
            }
        }

        plants = newGeneration;
        generation++;

        long currentSum = plants.Where(x => x.Value == '#').Select(x => (long)x.Key).Sum();
        long currentDiff = currentSum - lastSum;

        if (currentDiff == lastDiff)
        {
            stableCount++;
        }
        else
        {
            stableCount = 0;
        }

        lastDiff = currentDiff;
        lastSum = currentSum;

        if (stableCount >= requiredStableCount)
        {
            long remaining = target - generation;
            return currentSum + remaining * currentDiff;
        }
    }
}