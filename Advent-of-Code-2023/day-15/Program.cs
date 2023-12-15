using System.Text;

string[] input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Split(',').Where(x=> x!= "").ToArray();

Console.WriteLine("########## Day 1 2023 ##########");
Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
Console.WriteLine("################################");

long SolvePartOne(string[] input)
{
    long result = 0;
    for (int i = 0; i < input.Length; i++)
    {
        long temp = 0;
        byte[] asciiBytes = Encoding.ASCII.GetBytes(input[i]);
        foreach (var @byte in asciiBytes) 
        {
            temp += @byte;
            temp *= 17;
            temp %= 256;
        }

        result+=temp;
    }

    return result;
}

long SolvePartTwo(string[] input)
{
    Dictionary<long, List<string>> boxes = new Dictionary<long, List<string>>();
    for (int i = 0; i < input.Length; i++)
    {
        long boxNumber = 0;
        for (int j = 0; j < input[i].Length; j++) 
        {
            if (input[i][j] == '=') 
            {
                var newVale = input[i].Replace('=', ' ');
                if (!boxes.ContainsKey(boxNumber))
                {
                    var tt = input[i].Replace('=', ' ');
                    boxes.Add(boxNumber, new List<string>() { tt });
                }
                else 
                {
                    var label = boxes[boxNumber].Where(x => x.StartsWith(input[i].Split('=')[0])).FirstOrDefault();
                    if (label != null)
                    {
                        var postionOfOldLabel = boxes[boxNumber].IndexOf(label);
                        boxes[boxNumber][postionOfOldLabel] = newVale;
                    }
                    else 
                    {
                        boxes[boxNumber].Add(newVale);    
                    }
                }

                break;
            }
            else if (input[i][j] == '-') 
            {
                if (boxes.ContainsKey(boxNumber))
                {
                    var oldLabel = boxes[boxNumber].Where(x => x.StartsWith(input[i].Split('-')[0])).FirstOrDefault();
                    if (oldLabel != null)
                    {
                        boxes[boxNumber].Remove(oldLabel);
                    }
                }

                break;
            }

            byte bits = Encoding.ASCII.GetBytes(input[i][j].ToString()).First();
            boxNumber += bits;
            boxNumber *= 17;
            boxNumber %= 256;
        }
    }

    long result = 0;
    foreach (var box in boxes) 
    {
        for(int i = 0; i<box.Value.Count; i++)
        {
            long focalLength = Int64.Parse(box.Value[i].Split(' ')[1]);

            result += ((box.Key + 1) * (i + 1) * focalLength);
        }
    }

    return result;
}