using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_11
{
    class Program
    {
        private static int flashes = 0;
        private static HashSet<string> done;
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            List<int[]> inputs = new List<int[]>();
            foreach (string line in input)
            {
                char[] splited = line.ToCharArray();
                inputs.Add(Array.ConvertAll(splited, s => Int32.Parse(s.ToString())));
            }

            Console.WriteLine("########## Day 11 2021 ##########");
            SolvePuzzle(inputs.ToArray());
            Console.WriteLine("################################");
        }

        private static void SolvePuzzle(int[][] inputs)
        {
            int iteration = 0;
            while (true) 
            {
                iteration++;
                done = new HashSet<string>();

                //increace all
                for (int row = 0; row < inputs.Length; row++)
                {
                    for (int collumn = 0; collumn < inputs[0].Length; collumn++)
                    {
                        inputs[row][collumn] += 1;
                    }
                }

                //flash 
                for (int row = 0; row < inputs.Length; row++)
                {
                    for (int collumn = 0; collumn < inputs[0].Length; collumn++)
                    {
                        if (inputs[row][collumn] >= 10)
                        {
                            inputs[row][collumn] = 0;
                            inputs = Flash(row, collumn, inputs);
                        }
                    }
                }

                if (iteration == 100) 
                {
                    Console.WriteLine($"Part one solution: {flashes}");
                }

                if (inputs.All(x => x.All(z => z == 0))) 
                {
                    Console.WriteLine($"Part two solution: {iteration}");
                    break;
                }
            }
        }

        private static int[][] Flash(int row, int collumn, int[][] inputs)
        {
            done.Add($"{row},{collumn}");
            flashes++;
            if ((row - 1) >= 0)
            {
                if (collumn - 1 >= 0 && inputs[row - 1][collumn - 1] != 0) 
                {
                    //up left
                    inputs = IncreaceEnergy(row - 1, collumn - 1, inputs);
                }

                if (inputs[row - 1][collumn] != 0) 
                {
                    //up
                    inputs = IncreaceEnergy(row - 1, collumn, inputs);
                }

                if (collumn + 1 < inputs[0].Length && inputs[row - 1][collumn + 1] != 0) 
                {
                    //up right
                    inputs = IncreaceEnergy(row - 1, collumn + 1, inputs);
                }
            }

            if ((row + 1) < inputs.Length)
            {
                if (collumn - 1 >= 0 && inputs[row + 1][collumn - 1] != 0) 
                {
                    //down left
                    inputs = IncreaceEnergy(row + 1, collumn - 1, inputs);
                }

                if (inputs[row + 1][collumn] != 0) 
                {
                    //down
                    inputs = IncreaceEnergy(row + 1, collumn, inputs);
                }

                if (collumn + 1 < inputs[0].Length && inputs[row + 1][collumn + 1] != 0) 
                {
                    //down right
                    inputs = IncreaceEnergy(row + 1, collumn + 1, inputs);
                }
            }

            if (collumn - 1 >= 0 && (inputs[row][collumn - 1] != 0))
            {
                //left
                inputs = IncreaceEnergy(row, collumn - 1, inputs);
            }
            if (collumn + 1 < inputs[0].Length && (inputs[row][collumn + 1] != 0))
            {
                //right
                inputs = IncreaceEnergy(row, collumn + 1, inputs);
            }

            return inputs;
        }

        private static int[][] IncreaceEnergy(int row, int collumn, int[][] inputs)
        {
            if (!done.Contains($"{row},{collumn}")) 
            {
                int newValue = inputs[row][collumn] + 1;
                if (newValue == 10)
                {
                    //done.Add($"{row},{collumn}");
                    inputs[row][collumn] = 0;
                    inputs = Flash(row, collumn, inputs);
                }
                else
                {
                    inputs[row][collumn] = newValue;
                }
            }

            return inputs;
        }
    }
}
