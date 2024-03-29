﻿namespace day_08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            int[][] forest = GetForest(input);

            Console.WriteLine("########## Day 08 2022 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(forest)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(forest)}");
            Console.WriteLine("#################################");
        }

        private static object SolvePartOne(int[][] forest)
        {
            int counter = 0;
            for (int row = 1; row < forest.Length - 1; row++)
            {
                for (int column = 1; column < forest.Length - 1; column++)
                {
                    int currentTree = forest[row][column];

                    bool top = CheckTop(row, column, currentTree, forest);
                    if (top) 
                    {
                        counter++;
                        continue;
                    }

                    bool bottom = CheckBottom(row, column, currentTree, forest);
                    if (bottom)
                    {
                        counter++;
                        continue;
                    }

                    bool left = CheckLeft(row, column, currentTree, forest);
                    if (left) 
                    {
                        counter++;
                        continue;
                    }
                    
                    bool right = CheckRight(row, column, currentTree, forest);
                    if (right)
                    {
                        counter++;
                    }
                }
            }

            return counter + (forest.Length * 4 - 4);
        }

        private static object SolvePartTwo(int[][] forest)
        {
            int counter = 0;
            for (int row = 1; row < forest.Length - 1; row++)
            {
                for (int column = 1; column < forest.Length - 1; column++)
                {
                    int currentTree = forest[row][column];

                    bool top = CheckTop(row, column, currentTree, forest);
                    bool bottom = CheckBottom(row, column, currentTree, forest);
                    bool left = CheckLeft(row, column, currentTree, forest);
                    bool right = CheckRight(row, column, currentTree, forest);

                    if (top || bottom || left || right)
                    {
                        var visibleTop = VisibleTop(row, column, currentTree, forest);
                        var visibleBottom = VisibleBottom(row, column, currentTree, forest);
                        var visibleLeft = VisibleLeft(row, column, currentTree, forest);
                        int visibleRight = VisibleRight(row, column, currentTree, forest);

                        var sum = visibleTop * visibleBottom * visibleLeft * visibleRight;
                        if(sum>counter)
                            counter = sum;
                    }
                }
            }

            return counter;
        }

        private static int VisibleRight(int row, int column, int currentTree, int[][] forest)
        {
            int counter = 0;
            for (int i = column + 1; i < forest.Length; i++)
            {
                ++counter;
                if (forest[row][i] >= currentTree)
                {
                    break;
                }
            }

            return counter;
        }

        private static int VisibleLeft(int row, int column, int currentTree, int[][] forest)
        {
            int counter = 0;
            for (int i = column - 1; i >= 0; i--)
            {
                ++counter;
                if (forest[row][i] >= currentTree)
                {
                    break;
                }
            }

            return counter;
        }

        private static int VisibleBottom(int row, int column, int currentTree, int[][] forest)
        {
            int counter = 0;
            for (int i = row + 1; i < forest.Length; i++)
            {
                ++counter;

                if (forest[i][column] >= currentTree)
                {
                    break;
                }
            }

            return counter;
        }

        private static int VisibleTop(int row, int column, int currentTree, int[][] forest)
        {
            int counter = 0;
            for (int i = row -1; i >=0 ; i--)
            {
                ++counter;
                if (forest[i][column] >= currentTree)
                {
                    break;
                }
            }

            return counter;
        }

        private static int[][] GetForest(string[] input)
        {
            int[][] forest = new int[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                forest[i] = Array.ConvertAll(input[i].ToCharArray(), c => (int)Char.GetNumericValue(c));
            }

            return forest;
        }

        private static bool CheckRight(int row, int column, int currentTree, int[][] forest)
        {
            for (int i = column + 1; i < forest.Length; i++)
            {
                if (forest[row][i] >= currentTree)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckLeft(int row, int column, int currentTree, int[][] forest)
        {
            for (int i = 0; i < column; i++)
            {
                if (forest[row][i] >= currentTree)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckBottom(int row, int column, int currentTree, int[][] forest)
        {
            for (int i = row + 1; i < forest.Length; i++)
            {
                if (forest[i][column] >= currentTree)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckTop(int row, int column, int currentTree, int[][] forest)
        {
            for (int i = 0; i < row; i++)
            {
                if (forest[i][column] >= currentTree)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
