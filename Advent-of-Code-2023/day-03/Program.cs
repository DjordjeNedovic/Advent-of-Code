namespace day_03;

internal class Program
{
    /*
     SPOILER ALERT
     VERY UGLY CODE
    */

    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

        Console.WriteLine("########## Day 3 2023 ##########");
        Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
        Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
        Console.WriteLine("################################");
    }

    private static int SolvePartOne(string[] input)
    {
        int result = 0;
        for(int lineIndex =0; lineIndex < input.Length; lineIndex++) 
        {
            string num = "";
            int position = 0;
            for(int i = 0; i < input[lineIndex].Length; i++) 
            {
                if (Char.IsDigit(input[lineIndex][i])) 
                {
                    num = num + input[lineIndex][i];
                }

                if (!Char.IsDigit(input[lineIndex][i])) 
                {
                    if (num != "") 
                    {
                        position = i - 1;
                        if (CalculatePartNumber(input, position, lineIndex, num.Length))
                        {
                            result += Int32.Parse(num);
                        }

                        num = "";
                        position = 0;
                    }
                }

                if (i + 1 == input[lineIndex].Length && num != "") 
                {
                    position = i;
                    if (CalculatePartNumber(input, position, lineIndex, num.Length))
                    {
                        result += Int32.Parse(num);
                    }

                    num = "";
                    position = 0;
                }
            }
        }

        return result;
    }

    private static bool CalculatePartNumber(string[] input, int x, int y, int numSize)
    {
        int startX = (x - numSize - 1 < 0) ? 0 : x-numSize;
        int startY = y == 0 ? 0 : y-1;

        for (int dY = startY; dY <= y + 1; dY++)
        {
            for (int dX = startX; dX <= x + 1; dX++) 
            {
                if (dY > input.Length - 1 || dX > input[0].Length - 1 || Char.IsDigit(input[dY][dX]) || input[dY][dX] == '.')
                {
                    continue;
                }
                else
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static int SolvePartTwo(string[] input)
    {
        int result = 0;
        for (int lineIndex = 0; lineIndex < input.Length; lineIndex++)
        {
            if (input[lineIndex].Contains('*')) 
            {
                for (int i = 0; i < input[lineIndex].Length; i++)
                {
                    if (input[lineIndex][i] == '*') 
                    {
                        int x1, x2, y1, y2;
                        GetPositions(input, lineIndex, i, out x1, out x2, out y1, out y2);

                        if (x1 == -1 || x2 == -1)
                        {
                            continue;
                        }

                        int firstNumber = FindNumberBasedOnOperatorPosition(input[y1], x1);
                        int secondNumber = FindNumberBasedOnOperatorPosition(input[y2], x2);

                        result += (firstNumber * secondNumber);
                    }
                }
            }
        }

        return result;
    }

    private static void GetPositions(string[] input, int starY, int starX, out int x1, out int x2, out int y1, out int y2)
    {
        x1 = -1;
        x2 = -1;
        y1 = -1;
        y2 = -1;

        //above
        if (starY != 0) 
        {
            if (starX !=0 && Char.IsDigit(input[starY - 1][starX - 1]))
            {
                y1 = starY - 1;
                x1 = starX - 1;
            }

            if (Char.IsDigit(input[starY - 1][starX]))
            {
                if (x1 +1 == starX)
                {
                    y1 = starY - 1;
                    x1 = starX;
                }
            }

            if (starX < input[0].Length - 1 && Char.IsDigit(input[starY - 1][starX + 1]))
            {
                if (x1 == -1 || x1 == starX)
                {
                    y1 = starY - 1;
                    x1 = starX + 1;
                }
                else if (y1 == starY - 1 && starX != x1)
                {
                    y2 = starY - 1;
                    x2 = starX + 1;
                }
            }
        }

        //left
        if (starX != 0 && Char.IsDigit(input[starY][starX - 1])) 
        {
            if (x1 == -1)
            {
                y1 = starY;
                x1 = starX - 1;
            }
            else if (x2 == -1) 
            {
                y2 = starY;
                x2 = starX - 1;
            }
            else 
            {
                x1 = -1;
                x2 = -1;
                y1 = -1;
                y2 = -1;
                return;
            }
        }

        //right
        if (starX < input[0].Length - 1 && Char.IsDigit(input[starY][starX + 1])) 
        {
            if (x1 == -1)
            {
                y1 = starY;
                x1 = starX + 1;
            }
            else if (x2 == -1)
            {
                y2 = starY;
                x2 = starX + 1;
            }
            else
            {
                x1 = -1;
                x2 = -1;
                return;
            }
        }

        //down 
        if (starY != input.Length - 1) 
        {
            //down left
            if (starX != 0 && Char.IsDigit(input[starY + 1][starX - 1]))
            {
                if (x1 == -1)
                {
                    y1 = starY + 1;
                    x1 = starX - 1;
                }
                else if (x2 == -1)
                {
                    y2 = starY + 1;
                    x2 = starX - 1;
                }
                else
                {
                    x1 = -1;
                    x2 = -1;
                    return;
                }
            }

            if (Char.IsDigit(input[starY + 1][starX]))
            {
                if (x1 == -1 || (x1 +1 == starX && starY + 1 == y1))
                {
                    y1 = starY + 1;
                    x1 = starX;
                }
                else if (x2 == -1 || x2+1 ==starX)
                {
                    y2 = starY + 1;
                    x2 = starX;
                }
                else
                {
                    x1 = -1;
                    x2 = -1;
                    return;
                }
            }

            if (starX < input[0].Length - 1 && Char.IsDigit(input[starY + 1][starX + 1]))
            {
                var numb = input[starY + 1][starX + 1];
                if (x1 == -1 || (x1 == starX && starY + 1 == y1))
                {
                    y1 = starY + 1;
                    x1 = starX + 1;
                }
                else if (x2 == -1 || x2 == starX)
                {
                    y2 = starY + 1;
                    x2 = starX + 1;
                }
                else
                {
                    x1 = -1;
                    x2 = -1;
                    return;
                }
            }
        }
    }

    private static int FindNumberBasedOnOperatorPosition(string row, int x1)
    {
        bool isFound = false;
        string num = "";
        for (int i = 0; i < row.Length; i++) 
        {
            if (i == x1) 
            {
                isFound = true;
            }

            if (Char.IsDigit(row[i])) 
            {
                num+= row[i];
                continue;
            }

            if (!Char.IsDigit(row[i]) && num != "" && isFound)
            {
                //found it
                break;
            }
            else 
            {
                num = "";
            }
        }

        return Int32.Parse(num);
    }
}