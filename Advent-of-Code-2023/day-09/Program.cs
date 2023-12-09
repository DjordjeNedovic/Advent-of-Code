string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

Console.WriteLine("########## Day 9 2023 ##########");
Console.WriteLine($"Part one solution: {Solve(input, true)}");
Console.WriteLine($"Part two solution: {Solve(input, false)}");
Console.WriteLine("################################");

int Solve(string[] input, bool isPartOne)
{
    int result = 0;
    foreach (string s in input) 
    {
        List<int[]> listOfCalculatedHistory = new List<int[]>();
        int[] currentHistory = Array.ConvertAll(s.Split(" "), c=> Int32.Parse(c));
        listOfCalculatedHistory.Add(currentHistory);
        
        while (true) 
        {
            int[] currentHistoryCopy = listOfCalculatedHistory.Last();
            int size = currentHistoryCopy.Length - 1;

            int[] diff = new int[size];
            for (int i = 0; i < size; i++) 
            {
                int a = currentHistoryCopy[i];
                int b = currentHistoryCopy[i + 1];
                diff[i] = b - a;
            }

            var diffNumbers = diff.Distinct().ToArray();
            if (diffNumbers.Length == 1)
            {
                result += RetrunLastHistoryResult(listOfCalculatedHistory, diffNumbers[0], isPartOne);
                break;
            }
            else 
            {
                listOfCalculatedHistory.Add(diff);
            }
        }
    }

    return result;
}

int RetrunLastHistoryResult(List<int[]> ints, int v, bool isPartOne)
{
    int counter = v;
    var orderByLengthAsc = ints.OrderBy(i => i.Length).ToArray();
    if (isPartOne)
    {
        foreach (var current in orderByLengthAsc)
        {
            var lastElement = current.Last();
            counter += lastElement;
        }
    }
    else 
    {
        foreach (var current in orderByLengthAsc)
        {
            var lastElement = current.First();
            counter = lastElement - counter;
        }
    }

    return counter;
}