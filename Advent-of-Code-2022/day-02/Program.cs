namespace day_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 2 2022 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            int current = 0;

            foreach (string line in input)
            {
                string[] data = line.Split(' ');

                if (data[0] == "A") // ROCK
                {
                    if (data[1] == "X")
                    {
                        //ROCK vs ROCK = draw
                        current += 4;
                    }
                    else if (data[1] == "Y") 
                    {
                        //ROCK VS PAPER
                        current += 8;

                    }
                    else
                    {
                        //ROCK VS SCISORS
                        current += 3;
                    }

                }
                else if (data[0] == "B") //PAPER
                {
                    if (data[1] == "X")
                    {
                        //PAPER VS ROCK
                        current += 1;
                    }
                    else if (data[1] == "Y")
                    {
                        //PAPER VS PAPER
                        current += 5;
                    }
                    else
                    {
                        //PAPER VS SCISORS
                        current += 9;
                    }
                }
                else //scisors
                {
                    if (data[1] == "X")
                    {
                        //SCISORS VS ROCK
                        current += 7;
                    }
                    else if (data[1] == "Y")
                    {
                        //SCISORS VS Paper
                        current += 2;
                    }
                    else
                    {
                        //draw
                        current += 6;
                    }
                }
            }

            return current;
        }

        private static object SolvePartTwo(string[] input)
        {
            int current = 0;
            foreach (string line in input)
            {
                string[] data = line.Split(' ');

                if (data[0] == "A") // ROCK
                {
                    if (data[1] == "X") //LOSE
                    {
                        current += 3;
                    }
                    else if (data[1] == "Y") //DRAW
                    {
                        current += 4;
                    }
                    else //WIN
                    {
                        current += 8;
                    }
                }
                else if (data[0] == "B") //PAPER
                {
                    if (data[1] == "X") //LOSE
                    {
                        current += 1;
                    }
                    else if (data[1] == "Y") //DRAW
                    {
                        current += 5;
                    }
                    else //WIN
                    {
                        current += 9;
                    }
                }
                else //scisors
                {
                    if (data[1] == "X") //LOSE
                    {
                        current += 2;
                    }
                    else if (data[1] == "Y") //DRAW
                    {
                        current += 6;
                    }
                    else //WIN
                    {
                        current += 7;
                    }
                }
            }

            return current;
        }
    }
}
