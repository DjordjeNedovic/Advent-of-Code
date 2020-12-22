using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_22
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            string[] decks = input.Split("\r\n\r\n");
            string[] player1Data = decks[0].Split("\r\n");
            List<int> player1deck = ParseDeck(player1Data);
            string[] player2Data = decks[1].Split("\r\n");
            List<int> player2deck = ParseDeck(player2Data);

            Console.WriteLine("########## Day 22 2020 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(player1deck, player2deck)}");
            player1deck = ParseDeck(player1Data);
            player2deck = ParseDeck(player2Data);
            Console.WriteLine($"Part two solution: {SolvePartTwo(player1deck, player2deck)}");
            Console.WriteLine("#################################");
        }

        private static List<int> ParseDeck(string[] decks)
        {
            List<int> player1deck = new List<int>();
            for (int i = 0; i < decks.Length; i++)
            {
                if (decks[i].Contains("Player"))
                {
                    continue;
                }

                player1deck.Add(int.Parse(decks[i]));
            }

            return player1deck;
        }

        private static int SolvePartOne(List<int> player1deck, List<int> player2deck)
        {
            while (true)
            {
                if (player1deck.Count == 0 || player2deck.Count == 0)
                {
                    break;
                }

                int p1Card = player1deck[0];
                int p2Card = player2deck[0];
                if (p1Card > p2Card)
                {
                    player1deck.Add(p1Card);
                    player1deck.Add(p2Card);

                    player1deck.RemoveAt(0);
                    player2deck.RemoveAt(0);
                }
                else 
                {
                    player2deck.Add(p2Card);
                    player2deck.Add(p1Card);

                    player1deck.RemoveAt(0);
                    player2deck.RemoveAt(0);
                }
            }

            return CalculateWiningScore(player1deck.Count != 0 ? player1deck : player2deck);
        }

        private static int CalculateWiningScore(List<int> list)
        {
            int result = 0;
            list.Reverse();
            for (int i = 1; i <= list.Count; i++) 
            {
                result += list[i - 1] * i;
            }

            return result;
        }

        private static object SolvePartTwo(List<int> player1deck, List<int> player2deck)
        {
            while (true)
            {
                if (player1deck.Count == 0 || player2deck.Count == 0)
                {
                    break;
                }

                int p1Card = player1deck[0];
                int p2Card = player2deck[0];
                if (p1Card >= player1deck.Count || p2Card >= player2deck.Count)
                {
                    if (p1Card > p2Card)
                    {
                        player1deck.Add(p1Card);
                        player1deck.Add(p2Card);

                        player1deck.RemoveAt(0);
                        player2deck.RemoveAt(0);
                    }
                    else
                    {
                        player2deck.Add(p2Card);
                        player2deck.Add(p1Card);

                        player1deck.RemoveAt(0);
                        player2deck.RemoveAt(0);
                    }
                }
                else
                {
                    if (IsWinnerOfRecurviseGamePlayerOne(new List<int>(player1deck.GetRange(1, p1Card)), new List<int>(player2deck.GetRange(1, p2Card))))
                    {
                        player1deck.Add(p1Card);
                        player1deck.Add(p2Card);

                        player1deck.RemoveAt(0);
                        player2deck.RemoveAt(0);
                    }
                    else
                    {
                        player2deck.Add(p2Card);
                        player2deck.Add(p1Card);

                        player1deck.RemoveAt(0);
                        player2deck.RemoveAt(0);
                    }
                }
            }

            return CalculateWiningScore(player1deck.Count != 0 ? player1deck : player2deck);
        }

        private static bool IsWinnerOfRecurviseGamePlayerOne(List<int> player1deck, List<int> player2deck)
        {
            List<List<int>> player1deckCombinations = new List<List<int>>();
            List<List<int>> player2deckCombinations = new List<List<int>>();
            while (true)
            {
                int p1Card = player1deck[0];
                int p2Card = player2deck[0];
                if (p1Card >= player1deck.Count || p2Card >= player2deck.Count)
                {
                    if (p1Card > p2Card)
                    {
                        player1deck.Add(p1Card);
                        player1deck.Add(p2Card);

                        player1deck.RemoveAt(0);
                        player2deck.RemoveAt(0);
                    }
                    else
                    {
                        player2deck.Add(p2Card);
                        player2deck.Add(p1Card);

                        player1deck.RemoveAt(0);
                        player2deck.RemoveAt(0);
                    }
                }
                else
                {
                    if (IsWinnerOfRecurviseGamePlayerOne(new List<int>(player1deck.GetRange(1, p1Card)), new List<int>(player2deck.GetRange(1, p2Card))))
                    {
                        player1deck.Add(p1Card);
                        player1deck.Add(p2Card);

                        player1deck.RemoveAt(0);
                        player2deck.RemoveAt(0);
                    }
                    else
                    {
                        player2deck.Add(p2Card);
                        player2deck.Add(p1Card);

                        player1deck.RemoveAt(0);
                        player2deck.RemoveAt(0);
                    }
                }

                if (player1deckCombinations.Any(x => x.SequenceEqual(player1deck)) || player2deckCombinations.Any(x => x.SequenceEqual(player2deck))) 
                {
                    return true;
                }

                player1deckCombinations.Add(new List<int>(player1deck));
                player2deckCombinations.Add(new List<int>(player2deck));

                if (player1deck.Count == 0 || player2deck.Count == 0)
                {
                    break;
                }
            }

            return (player1deck.Count > player2deck.Count);
        }
    }
}