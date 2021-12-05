using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            string[] combinationString = input[0].Split(',');
            int[] inputs = (Array.ConvertAll(combinationString, s => Int32.Parse(s)));
            List<string[][]> final = GetBoards(input);
            Console.WriteLine("########## Day 4 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(final, inputs)}");
            final = GetBoards(input);
            Console.WriteLine($"Part two solution: {SolvePartTwo(final, inputs)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(List<string[][]> boadrs, int[] combinations)
        {
            int result = 0;
            for (int number = 0; number < combinations.Length; number++) 
            {
                int boardNumber = 0;
                foreach (string[][] board in boadrs)
                {
                    for (int row = 0; row < board.Length; row++)
                    {
                        for (int column = 0; column < 5; column++)
                        {
                            if (board[row][column] != "X" && Int32.Parse(board[row][column]) == combinations[number])
                            {
                                board[row][column] = "X";
                            }
                        }
                    }

                    boardNumber++;
                }

                if (combinations[number] > 5)
                {
                    int others = CheckIfAnyWin(boadrs);
                    if (others != -1) 
                    {
                        result = others * combinations[number];
                        break;
                    }
                }
            }

            return result;
        }

        private static object SolvePartTwo(List<string[][]> boards, int[] combinations)
        {
            int largestWinRound = 0;
            int result = 0;
            foreach (string[][] board in boards)
            {
                int boardWinRound = 0;
                List<string[][]> ad = new List<string[][]>() { board };
                for (int number = 0; number < combinations.Length; number++)
                {
                    for (int row = 0; row < board.Length; row++)
                    {
                        for (int column = 0; column < 5; column++)
                        {
                            if (board[row][column] != "X" && Int32.Parse(board[row][column]) == combinations[number])
                            {
                                board[row][column] = "X";
                            }
                        }
                    }

                    if (combinations[number] > 5)
                    {
                        int others = CheckIfAnyWin(ad);
                        if (others != -1)
                        {
                            if (boardWinRound > largestWinRound) 
                            {
                                largestWinRound = boardWinRound;

                                boardWinRound = 0;
                                result = others * combinations[number];
                            }

                            break;
                        }
                    }

                    boardWinRound++;
                }
            }

            return result;
        }


        private static List<string[][]> GetBoards(string[] input)
        {
            List<List<string[]>> allBoards = new List<List<string[]>>();
            List<string[]> currentBoard = null;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == "")
                {
                    allBoards.Add(currentBoard);
                    currentBoard = new List<string[]>();
                    continue;
                }

                var row = input[i].Split(' ').ToList();
                row.RemoveAll(x => x == "");

                string[] g = row.ToArray();
                currentBoard.Add(g);
            }

            allBoards.Add(currentBoard);
            allBoards.RemoveAt(0);

            List<string[][]> boardsStruct = new List<string[][]>();
            foreach (var current in allBoards)
            {
                boardsStruct.Add(current.ToArray());
            }

            return boardsStruct;
        }

        private static int CheckIfAnyWin(List<string[][]> final)
        {
            int found = -1;
            int result = -1;
            for (int boardNumber = 0; boardNumber < final.Count; boardNumber++)
            {
                var current = final[boardNumber];
                for (int index = 0; index < current.Length; index++)
                {
                    if (current[index].All(x => x == "X") || current.Select(x => x[index]).All(x => x == "X"))
                    {
                        found = boardNumber;
                    }
                }
            }

            if (found != -1)
            {
                var otherNumbers = final[found].SelectMany(x => x).Where(x => x != "X").ToArray();
                result = (Array.ConvertAll(otherNumbers, z => Int32.Parse(z))).Sum();
            }

            return result;
        }
    }
}
