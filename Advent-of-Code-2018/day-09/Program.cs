using System.Text.RegularExpressions;

string input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

var numbers = Regex.Matches(input, @"\d+")
                    .Select(m => int.Parse(m.Value))
                    .ToArray();

int players = numbers[0];
int lastMarble = numbers[1];

Console.WriteLine("########## Day 9 2018 ##########");
Console.WriteLine($"Part one solution: {SolvePartOne(players, lastMarble)}");
Console.WriteLine($"Part two solution: {SolvePartTwo(players, lastMarble)}");
Console.WriteLine("################################");

int SolvePartOne(int players, int lastMarble)
{
    int[] next = new int[lastMarble + 1];
    int[] prev = new int[lastMarble + 1];
    int[] score = new int[players + 1];
    next[0] = 0;
    prev[0] = 0;

    int current = 0;
    for (int i = 1; i < lastMarble; i++) 
    {
        if (i % 23 == 0)
        {
            int t = prev[prev[prev[prev[prev[prev[prev[current]]]]]]];
            score[i % players] += (i+t);

            next[prev[t]] = next[t];
            prev[next[t]] = prev[t];

            current = next[t];

            continue;
        }

        int A = next[current];
        int B = next[A];

        next[A] = i;
        prev[i] = A;
        next[i] = B;
        prev[B] = i;

        current = i;
    }

    return score.Max();
}

long SolvePartTwo(int players, int lastMarble)
{
    int rounds = lastMarble * 100 + 1;
    int[] next = new int[rounds + 1];
    int[] prev = new int[rounds + 1];
    long[] score = new long[players + 1];
    next[0] = 0;
    prev[0] = 0;

    int current = 0;
    for (int i = 1; i < rounds; i++)
    {
        if (i % 23 == 0)
        {
            int t = prev[prev[prev[prev[prev[prev[prev[current]]]]]]];
            score[i % players] += (i + t);

            next[prev[t]] = next[t];
            prev[next[t]] = prev[t];

            current = next[t];

            continue;
        }

        int A = next[current];
        int B = next[A];

        next[A] = i;
        prev[i] = A;
        next[i] = B;
        prev[B] = i;

        current = i;
    }

    return score.Max();
}