namespace day_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = Int32.Parse(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")));

            Console.WriteLine("########## Day 11 2018 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(int input)
        {
            int[][] grid = GetGrid(input);

            Dictionary<string, int> matrix3x3values = new Dictionary<string, int>();
            for (int yD = 0; yD < 297; yD++)
            {
                for (int xD = 0; xD < 279; xD++)
                {
                    string key = $"{xD},{yD}";

                    int score = 0;

                    score += grid[yD][xD];
                    score += grid[yD][xD + 1];
                    score += grid[yD][xD + 2];

                    score += grid[yD + 1][xD];
                    score += grid[yD + 1][xD + 1];
                    score += grid[yD + 1][xD + 2];

                    score += grid[yD + 2][xD];
                    score += grid[yD + 2][xD + 1];
                    score += grid[yD + 2][xD + 2];

                    matrix3x3values.Add(key, score);
                }
            }

            return matrix3x3values.MaxBy(kvp => kvp.Value).Key;
        }

        private static int[][] GetGrid(int input)
        {
            int[][] grid = new int[300][];
            for (int y = 0; y < 300; y++)
            {
                grid[y] = new int[300];
                for (int x = 0; x < 300; x++)
                {
                    int rackId = x + 10;
                    int powerLevel = rackId * y;
                    int addSerial = powerLevel + input;

                    int rule4 = addSerial * rackId;

                    string t = rule4.ToString();
                    int hunderds = 0;
                    if (t.ToCharArray().Length > 2)
                    {
                        hunderds = Int32.Parse(t.ToCharArray().ElementAt(t.ToCharArray().Length - 3).ToString());
                    }

                    int rule6 = hunderds - 5;

                    grid[y][x] = rule6;
                }
            }

            return grid;
        }

        private static object SolvePartTwo(int input)
        {
            int[][] grid = GetGrid(input);
            Dictionary<string, int> matrix3x3values = new Dictionary<string, int>();
            for (int squareSize = 3; squareSize < 300; squareSize++)
            {
                for (int yD = 0; yD < 300 - squareSize; yD++)
                {
                    for (int xD = 0; xD < 300 - squareSize; xD++)
                    {
                        string key = $"{xD},{yD},{squareSize}";

                        int score = 0;
                        for (int deltaY = yD; deltaY < yD + squareSize; deltaY++)
                        {
                            for (int deltaX = xD; deltaX < xD + squareSize; deltaX++)
                            {
                                score += grid[deltaY][deltaX];
                            }
                        }

                        if (score > 1)
                            matrix3x3values.Add(key, score);
                    }
                }   
            }

            return matrix3x3values.MaxBy(kvp => kvp.Value).Key;
        }
    }
}