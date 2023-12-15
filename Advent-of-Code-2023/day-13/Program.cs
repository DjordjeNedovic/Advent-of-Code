string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

Console.WriteLine("########## Day 13 2023 ##########");
Console.WriteLine($"Part one solution: {Solve(input, true)}");
Console.WriteLine($"Part two solution: {Solve(input, false)}");
Console.WriteLine("#################################");

long Solve(string[] input, bool isPartOne) 
{
    long result = 0;
    List<string> output = new List<string>();
    for (int i = 0; i < input.Length; i++) 
    {
        if (input[i] == "")
        {
            result += CalculateData(output.ToArray(), isPartOne);
            output.Clear();
        }
        else
        {
            output.Add(input[i]);
        }
    }

    result += CalculateData(output.ToArray(), isPartOne);

    return result;
}

long CalculateData(string[] output, bool isPartOne)
{
    bool isHorizontal = true;
    int dRow = CalculateReflection(output, isPartOne);
    if (dRow == -1)
    {
        isHorizontal = false;
        output = FlipReflection(output);
        dRow = CalculateReflection(output, isPartOne);
    }

    return isHorizontal ? (dRow + 1) * 100 : dRow + 1;
}

string[] FlipReflection(string[] output)
{
    string[] result = new string[output[0].Length];
    for (int i = 0; i < output[0].Length; i++)
    {
        var newRow = String.Join("", output.Select(x => x[i].ToString()).ToArray());
        result[i] = newRow;
    }

    return result;
}

int CalculateReflection(string[] output, bool isPartOne)
{
    bool isHorizontal = false;
    int dRow = 0;
    for (int i = 0; i < output.Length; i++)
    {
        if (i + 1 > output.Length - 1) break;
        var row1 = output[i];
        var row2 = output[i + 1];

        var diff = row1.Select((col, colIndex) => col == row2[colIndex] ? 0 : 1).Sum();
        bool smudgeFound = false;

        if ( (isPartOne && row1 == row2) || (!isPartOne && diff <= 1) )
        {
            dRow = i;
            int indexOfFirst = i;
            int indexOfLast = i + 1;
            isHorizontal = true;
            if (diff == 1)
                smudgeFound = true;

            for (int j = 1; ; j++)
            {
                if (indexOfFirst - j < 0 || indexOfLast + j > output.Length - 1) break;

                var trow1 = output[indexOfFirst - j];
                var trow2 = output[indexOfLast + j];

                if (isPartOne && trow1 == trow2)
                {
                    isHorizontal = true;
                }
                else if (isPartOne && trow1 != trow2)
                {
                    isHorizontal = false;
                    break;
                }
                else 
                {
                    var diff2 = trow1.Select((col, colIndex) => col == trow2[colIndex] ? 0 : 1).Sum();
                    if (diff2 <= 1)
                    {
                        if (diff2 == 1 && !smudgeFound)
                            smudgeFound = true;
                        else if (diff2 == 1 && smudgeFound)
                        {
                            isHorizontal = false;
                            break;
                        }
                        isHorizontal = true;
                    }
                    else
                    {
                        isHorizontal = false;
                        break;
                    }
                }
            }
        }

        if (isPartOne && isHorizontal)
        {
            break;
        }
        else 
        {
            if (isHorizontal && smudgeFound)
            {
                break;
            }

            isHorizontal = false;
        }
    }

    if (isHorizontal)
    {
        return dRow;
    }
    else
    {
        return -1;
    }
}