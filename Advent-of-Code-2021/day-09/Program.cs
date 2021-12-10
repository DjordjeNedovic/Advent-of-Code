using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_9
{
    class Program
    {
        private static int BasinSize = 0;
        private static HashSet<string> visited = new HashSet<string>();
        private static int rowLength = 0;
        private static int collumnLength = 0;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 9 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(string[] input)
        {
            int result = 0;
            int[][] map= GetArray(input);
            for (int row = 0; row < map.Length; row++)
            {
                for (int collumn = 0; collumn < map[0].Length; collumn++)
                {
                    int curretnt = map[row][collumn];
                    if (IsLowPoint(map, row, collumn, curretnt)) 
                    {
                        result += (curretnt + 1);
                    }
                }
            }

            return result;
        }

        private static bool IsLowPoint(int[][] gg, int row, int collumn, int curretnt)
        {
            if (row != 0 && curretnt >= gg[row - 1][collumn])
            {
                return false;
            }
            if (row + 1 != gg.Length && curretnt >= gg[row + 1][collumn])
            {
                return false;
            }
            if (collumn != 0 && curretnt >= gg[row][collumn - 1])
            {
                return false;
            }
            if (collumn + 1 != gg[0].Length && curretnt >= gg[row][collumn + 1])
            {
                return false;
            }

            return true;
        }

        private static int[][] GetArray(string[] input)
        {
            List<int[]> listOfRowTiles = new List<int[]>();
            foreach (string line in input)
            {
                int[] inputs = (Array.ConvertAll(line.ToCharArray(), s => Int32.Parse(s.ToString())));
                listOfRowTiles.Add(inputs);
            }

            return listOfRowTiles.ToArray();
        }

        private static object SolvePartTwo(string[] input)
        {
            List<int> basins = new List<int>();
            int[][] map = GetArray(input);
            rowLength = map.Length;
            collumnLength = map[0].Length;
            for (int row = 0; row < rowLength; row++)
            {
                for (int collumn = 0; collumn < map[0].Length; collumn++)
                {
                    int curretnt = map[row][collumn];
                    if (IsLowPoint(map, row, collumn, curretnt))
                    {
                        BasinSize = 0;
                        visited = new HashSet<String>();
                        Recursive(map, row, collumn);
                        basins.Add(BasinSize);
                    }
                }
            }

            return basins.OrderByDescending(x=>x).Take(3).Aggregate(1, (x,y)=>x*y);
        }

        private static void Recursive(int[][] map, int row, int collumn)
        {
            if (!IsVisited(map, row, collumn) && row != -1 && collumn != -1 && row < rowLength && collumn<collumnLength)
            {
                visited.Add($"{row}:{collumn}");
                int curretnt = map[row][collumn];
                if (curretnt != 9)
                {
                    BasinSize++;
                    Recursive(map, row - 1, collumn);
                    Recursive(map, row + 1, collumn);
                    Recursive(map, row, collumn - 1);
                    Recursive(map, row, collumn + 1);
                }
            }
        }

        private static bool IsVisited(int[][] gg, int row, int collumn)
        {
            return visited.Contains($"{row}:{collumn}");
        }
    }
}
