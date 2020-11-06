using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_16
{
    class Program
    {
        static int[] basePatern = new int[] { 0, 1, 0, -1 };
        static int[] currentPatern = new int[] { 1, 0, -1, 0 };
        
        static void Main(string[] args)
        {
            string result = SolvePartOne();
            Console.WriteLine($"first 8 nubers are: {result}");
        }

        private static string SolvePartOne()
        {
            string t = String.Empty;

            String input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            List<string> items = (List<string>)Split(input, 1);
            List<int> baseList = new List<int>();
            foreach (var intem in items)
            {
                baseList.Add(Int32.Parse(intem));
            }

            for (int phase = 1; phase <= 100; phase++)
            {
                List<int> newList = new List<int>();
                for (int stateSth = 1; stateSth <= baseList.Count; stateSth++)
                {
                    List<int> iterationResults = new List<int>();
                    for (int index = 0; index < baseList.Count; index++)
                    {
                        int currentInt = baseList.ElementAt(index);
                        int multiplayer = GetMultiplayer(stateSth, index);

                        int result = currentInt * multiplayer;


                        if (result > 10 || result < -10)
                        {
                            result = result % (10);
                        }

                        iterationResults.Add(result);
                    }

                    int mainResult = 0;
                    foreach (var one in iterationResults)
                    {
                        mainResult += one;
                    }

                    //Console.WriteLine($"[{(String.Join(',', iterationResults))}]");

                    int value = Math.Abs(mainResult) % 10;
                    newList.Add(value);

                    UpdateCurrentPattern(stateSth + 1, baseList.Count);
                }

                //Console.WriteLine($"phase {phase}: {String.Join(String.Empty, newList)}");

                t = String.Join(String.Empty, newList);

                baseList = new List<int>(newList);
                currentPatern = new int[] { 1, 0, -1, 0 };
            }

            return t.Substring(0, 8);
        }

        private static void UpdateCurrentPattern(int v, int count)
        {
            List<int> tempList = new List<int>();

            int g = 0;
            while (g < (count +1)) 
            {
                for (int i = 0; i < basePatern.Length; i++)
                {
                    for (int y = 0; y < v; y++)
                    {
                        g++;
                        tempList.Add(basePatern[i]);
                    }
                }
            }

            currentPatern = tempList.Skip(1).ToArray();
        }

        private static int GetMultiplayer(int iteration, int index)
        {
            int g = currentPatern.Length;

            if (index > currentPatern.Length)
            {
                return currentPatern[index % g];
            }
            else if (index == currentPatern.Length) 
            {
                return currentPatern[0];
            }
            else
            {
                return currentPatern[index];
            }
        }

        static List<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize)).ToList();
        }
    }
}
