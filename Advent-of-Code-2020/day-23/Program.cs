using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace day_23
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            LinkedList<int> cups = new LinkedList<int>();
            LinkedList<int> cups2 = new LinkedList<int>();
            foreach (char cupLabel in input.ToCharArray()) 
            {
                cups2.AddLast(int.Parse(cupLabel.ToString()));
                cups.AddLast(int.Parse(cupLabel.ToString()));
            }

            Console.WriteLine("########## Day 23 2020 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(cups)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(cups2)}");
            Console.WriteLine("#################################");
        }

        private static string SolvePartOne(LinkedList<int> cups)
        {
            Dictionary<int, LinkedListNode<int>> t = CreateDictionary(cups);
            var current = cups.First;
            for (int i = 0; i < 100; i++)
            {
                var pickUpThree = PickUpThree(cups, current);
                LinkedListNode<int> destinationNode = GetDestinationNode(cups, pickUpThree, t, current.Value);

                foreach (var removeNode in pickUpThree)
                {
                    cups.Remove(removeNode);
                }

                var addAfterNode = destinationNode;
                foreach (var addNode in pickUpThree)
                {
                    cups.AddAfter(addAfterNode, addNode);
                    addAfterNode = addNode;
                }

                current = current.Next;
                if (current == null)
                {
                    current = cups.First;
                }
            }

            return CreateResultString(cups, t);
        }

        private static string CreateResultString(LinkedList<int> cups, Dictionary<int, LinkedListNode<int>> lookup)
        {
            string result = string.Empty;
            LinkedListNode<int> oneNode = lookup[1];
            LinkedListNode<int> current = lookup[1];
            do
            {
                current = current.Next;
                if (current == null)
                    current = cups.First;

                if (current.Value != 1)
                    result += current.Value;
            } while (current != oneNode);

            return result;
        }

        private static string SolvePartTwo(LinkedList<int> cups)
        {
            int max = cups.Max();
            for (int j = cups.Count; j < 1000000; j++)
            {
                max++;
                cups.AddLast(max);
            }

            Dictionary<int, LinkedListNode<int>> lookup = CreateDictionary(cups);
            var current = cups.First;
            for (int i = 0; i < 10000000; i++)
            {
                List<LinkedListNode<int>> pickUpThree = PickUpThree(cups, current);
                LinkedListNode<int> destinationNode = GetDestinationNode(cups, pickUpThree, lookup, current.Value);
                // Remove them
                foreach (var removeNode in pickUpThree)
                {
                    cups.Remove(removeNode);
                }

                // Add them
                var addAfterNode = destinationNode;
                foreach (var addNode in pickUpThree)
                {
                    cups.AddAfter(addAfterNode, addNode);
                    addAfterNode = addNode;
                }


                current = current.Next;
                if (current == null) 
                {
                    current = cups.First;
                }
            }

            var final = cups.ToList();
            var oneIndex = final.IndexOf(1);

            var label21 = final[oneIndex + 1];
            var label22 = final[oneIndex + 2];
            var result = (long)label22 * (long)label21;

            return result.ToString();
        }


        private static Dictionary<int, LinkedListNode<int>> CreateDictionary(LinkedList<int> cups)
        {
            Dictionary<int, LinkedListNode<int>> lookup = new Dictionary<int, LinkedListNode<int>>();
            var currentNode = cups.First;
            for (int i = 0; i < cups.Count; i++)
            {
                lookup.Add(currentNode.Value, currentNode);
                currentNode = currentNode.Next;
            }

            return lookup;
        }

        private static LinkedListNode<int> GetDestinationNode(LinkedList<int> cups, List<LinkedListNode<int>> pickUpThree, Dictionary<int, LinkedListNode<int>> lookup, int destinationNodeValue)
        {
            for (int i = 1; i < cups.Count; i++)
            {
                destinationNodeValue--;
                if (destinationNodeValue < 1)
                {
                    destinationNodeValue += cups.Count;
                }
                if (!pickUpThree.Select(n => n.Value).Contains(destinationNodeValue))
                {
                    break;
                }
            }

            return lookup[destinationNodeValue];
        }

        private static List<LinkedListNode<int>> PickUpThree(LinkedList<int> cups, LinkedListNode<int> current)
        {
            List<LinkedListNode<int>> pickUpThreeNodes = new List<LinkedListNode<int>>();
            var currentNode = current;
            for (int i = 0; i < 3; i++) 
            {
                currentNode = currentNode.Next;
                if (currentNode == null)
                {
                    currentNode = cups.First;
                }
                
                pickUpThreeNodes.Add(currentNode);
            }

            return pickUpThreeNodes;
        }
    }
}
