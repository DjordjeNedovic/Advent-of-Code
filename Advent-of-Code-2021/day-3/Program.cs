using System;
using System.Collections.Generic;
using System.IO;

namespace day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 3 2021 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(string[] input)
        {
            string gamaBytes = "";
            string epselonBytes = "";

            for (int i = 0; i < input[0].Length; i++) 
            {
                int ones = 0;
                int zeros = 0;
                foreach (string line in input) 
                {
                    char temp = line.ToCharArray()[i];
                    if (temp.Equals('0'))
                    {
                        zeros++;
                    }
                    else 
                    {
                        ones++;
                    }
                }

                gamaBytes += (ones > zeros) ? "1" : "0";
                epselonBytes += (ones > zeros) ? "0" : "1";
            }

            int gamaInt = Convert.ToInt32(gamaBytes,2);
            int epsilonInt = Convert.ToInt32(epselonBytes,2);

            return gamaInt * epsilonInt;
        }

        private static int SolvePartTwo(string[] input)
        {
            List<string> zerosList = new List<string>();
            List<string> onesList = new List<string>();
            foreach (string line in input)
            {
                char temp = line.ToCharArray()[0];
                if (temp.Equals('0'))
                {
                    zerosList.Add(line);
                }
                else
                {
                    onesList.Add(line);
                }
            }

            string bytes1 = ReturnBynaryNumber(input, zerosList, zerosList.Count > onesList.Count);
            string bytes2 = ReturnBynaryNumber(input, onesList, zerosList.Count < onesList.Count);

            return Convert.ToInt32(bytes1, 2)* Convert.ToInt32(bytes2, 2);
        }

        private static string ReturnBynaryNumber(string[] input, List<string> mainList, bool isOxygenGeneratorRating)
        {
            for (int i = 1; i < input[0].Length; i++)
            {
                if (mainList.Count == 1)
                {
                    break;
                }

                List<string> zerosListCopy = new List<string>();
                List<string> onesListCopy = new List<string>();
                for (int j = 0; j < mainList.Count; j++)
                {
                    if (mainList.Count == 2)
                    {
                        char tempA = mainList[j].ToCharArray()[i];
                        char tempB = mainList[j+1].ToCharArray()[i];
                        if (tempA.Equals(tempB))
                        {
                            break;
                        }

                        if (isOxygenGeneratorRating)
                        {
                            if (tempA == '1')
                            {
                                return mainList[j];
                            }
                            else
                            {
                                return mainList[j + 1];
                            }
                        }
                        else 
                        {
                            if (tempA == '0')
                            {
                                return mainList[j];
                            }
                            else
                            {
                                return mainList[j + 1];
                            }
                        }
                    
                    }
                    else 
                    {
                        string line = mainList[j];
                        char temp = line.ToCharArray()[i];
                        if (temp.Equals('0'))
                        {
                            zerosListCopy.Add(line);
                        }
                        else
                        {
                            onesListCopy.Add(line);
                        }
                    }
                }

                if (isOxygenGeneratorRating)
                {
                    if (zerosListCopy.Count > onesListCopy.Count)
                    {
                        mainList = zerosListCopy;
                    }
                    else if (zerosListCopy.Count <= onesListCopy.Count)
                    {
                        mainList = onesListCopy;
                    }
                }
                else 
                {
                    if (zerosListCopy.Count <= onesListCopy.Count)
                    {
                        mainList = zerosListCopy;
                    }
                    else
                    {
                        if(onesListCopy.Count!=0)
                            mainList = onesListCopy;
                    }
                }
            }

            return mainList[0];
        }
    }
}
