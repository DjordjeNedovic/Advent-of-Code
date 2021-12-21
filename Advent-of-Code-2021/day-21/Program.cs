using System;
using System.Collections.Generic;
using System.IO;

namespace day_21
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 21 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            Dictionary<string, int> combination = new Dictionary<string, int>();
            int i = 0;
            bool isPlayerOne = true;

            int playerOnePosition = Int32.Parse(input[0].Split(":")[1].Trim());
            int playerTwoPosition = Int32.Parse(input[1].Split(":")[1].Trim());

            int playerOneResult = 0;
            int playerTwoResult = 0;

            int rounds = 0;
            while (true) 
            {
                if (i == 100) 
                {
                    i = 0;
                }
                int first = ++i;

                if (i == 100)
                {
                    i = 0;
                }
                int secound = ++i;

                if (i == 100)
                {
                    i = 0;
                }
                int thierd = ++i;

                int result;
                if (combination.ContainsKey($"{first},{secound},{thierd}"))
                {
                    result = combination[$"{first},{secound},{thierd}"];
                }
                else 
                {
                    result = first + secound + thierd;
                    combination.Add($"{first},{secound},{thierd}", result);
                }

                if (isPlayerOne)
                {
                    
                    playerOnePosition = (playerOnePosition + result % 10 ) <= 10? (playerOnePosition + result % 10 ) : (playerOnePosition + result % 10) % 10;
                    playerOneResult += playerOnePosition;
                    isPlayerOne = false;
                    rounds++;
                }
                else 
                {
                    playerTwoPosition = (playerTwoPosition + result %10) <= 10 ? (playerTwoPosition + result % 10) : (playerTwoPosition + result % 10) % 10;
                    playerTwoResult += playerTwoPosition;
                    isPlayerOne = true;
                    rounds++;
                }

                if (playerOneResult >= 1000 || playerTwoResult >= 1000) 
                {
                    break;
                }
            }

            return (playerOneResult > playerTwoResult) ? playerTwoResult * rounds * 3 : playerOneResult * rounds * 3;
        }
    }
}
