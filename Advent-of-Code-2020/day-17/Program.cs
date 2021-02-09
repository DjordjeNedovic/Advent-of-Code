using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_17
{
    class Program
    {
        private static List<(int, int, int)> neighbour3D;
        private static List<(int, int, int, int)> neighbours4D;

        static void Main(string[] args)
        {
            SetNeighboursInAllDimensions();

            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 17 2020 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("#################################");
        }

        private static void SetNeighboursInAllDimensions()
        {
            neighbour3D = new List<(int, int, int)>();
            neighbours4D = new List<(int, int, int, int)>();
            for (int i = -1; i < 2; i++)
            {
                for (int y = -1; y < 2; y++)
                {
                    for (int z = -1; z < 2; z++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if (i == 0 && y == 0 && z == 0 && j == 0)
                                continue;

                            neighbours4D.Add((i, y, z, j));
                        }

                        if (i == 0 && y == 0 && z == 0)
                            continue;

                        neighbour3D.Add((i, y, z));
                    }
                }
            }
        }

        private static int SolvePartOne(string[] input)
        {
            HashSet<(int, int, int)> cubes = new HashSet<(int, int, int)>();

            int r = 0;
            foreach (var line in input)
            {
                var row = line.Trim();
                for (int c = 0; c < row.Length; c++) 
                {
                    if (row[c] == '#') 
                    {
                        cubes.Add((r, c, 0));
                    }
                }

                ++r;
            }

            for (int j = 0; j < 6; j++) 
            {
                cubes = Cycle(cubes);
            }

            return cubes.Count;
        }

        private static HashSet<(int, int, int)> Cycle(HashSet<(int, int, int)> cubes)
        {
            HashSet<(int, int, int)> nextCycle = new HashSet<(int, int, int)>();

            foreach (var cube in cubes)
            {
                List<(int, int, int)> allNeighbours = GetNeighboursIn3D(cube);
                allNeighbours.Add(cube);

                foreach (var otherCube in allNeighbours)
                {
                    int active = NumberOfActiveNeighbours3D(cubes, otherCube);
                    if (cubes.Contains(otherCube) && (active == 2 || active == 3))
                    {
                        nextCycle.Add(otherCube);
                    }
                    else if (!cubes.Contains(otherCube) && active == 3) 
                    {
                        nextCycle.Add(otherCube);
                    }
                }
            }

            return nextCycle;
        }

        private static List<(int, int, int)> GetNeighboursIn3D((int, int, int) loc)
        {
            return neighbour3D.Select(a => (a.Item1 + loc.Item1, a.Item2 + loc.Item2, a.Item3 + loc.Item3)).ToList();
        }

        private static int NumberOfActiveNeighbours3D(HashSet<(int, int, int)> grid, (int, int, int) loc)
        {
            return neighbour3D.Select(cube => (cube.Item1 + loc.Item1, cube.Item2 + loc.Item2, cube.Item3 + loc.Item3))
                             .Where(newCube => grid.Contains(newCube)).Count();
        }

        private static int SolvePartTwo(string[] input)
        {
            HashSet<(int, int, int, int)> cubes = new HashSet<(int, int, int, int)>();

            int r = 0;
            foreach (var line in input)
            {
                var row = line.Trim();
                for (int c = 0; c < row.Length; c++)
                    if (row[c] == '#')
                        cubes.Add((r, c, 0, 0));
                ++r;
            }

            for (int j = 0; j < 6; j++) 
            {
                cubes = CyclePartTwo(cubes);
            }

            return cubes.Count;
        }

        private static HashSet<(int, int, int, int)> CyclePartTwo(HashSet<(int, int, int, int)> cubes)
        {
            HashSet<(int, int, int, int)> nextCycle = new HashSet<(int, int, int, int)>();
            foreach (var cube in cubes)
            {
                List<(int, int, int, int)> allNeighbours = GetNeighboursIn4D(cube);
                allNeighbours.Add(cube);
                foreach (var otherCube in allNeighbours)
                {
                    int active = NumberOfActiveNeighbours4D(cubes, otherCube);
                    if (cubes.Contains(otherCube) && (active == 2 || active == 3))
                    {
                        nextCycle.Add(otherCube);
                    }
                    else if (!cubes.Contains(otherCube) && active == 3) 
                    {
                        nextCycle.Add(otherCube);
                    }
                }
            }

            return nextCycle;
        }

        private static List<(int, int, int, int)> GetNeighboursIn4D((int, int, int, int) pivotCube)
        {
            return neighbours4D.Select(cube => (cube.Item1 + pivotCube.Item1, cube.Item2 + pivotCube.Item2, cube.Item3 + pivotCube.Item3, cube.Item4 + pivotCube.Item4)).ToList();
        }

        private static int NumberOfActiveNeighbours4D(HashSet<(int, int, int, int)> grid, (int, int, int, int) pivotCube)
        {
            return neighbours4D.Select(cybe => (cybe.Item1 + pivotCube.Item1, cybe.Item2 + pivotCube.Item2, cybe.Item3 + pivotCube.Item3, cybe.Item4 + pivotCube.Item4))
                               .Where(newCube => grid.Contains(newCube)).Count();
        }
    }
}
