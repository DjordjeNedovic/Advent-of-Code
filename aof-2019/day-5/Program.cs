using System;
using System.IO;

namespace day_5
{
    //it will be refactored
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("################### PART ONE ###################");
            PartOne();
            Console.WriteLine("################### PART Two ###################");
            PartTwo();
        }

        private static void PartTwo()
        {
            int result = 0;
            Console.WriteLine("Enter input: ");
            string keyInput = Console.ReadLine();
            int input = Int32.Parse(keyInput);
            string inputFromTxt = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            int[] puzzleInput = Array.ConvertAll(inputFromTxt.Split(','), s => Int32.Parse(s));
            int oct = 0;
            while (true)
            {
                if (oct > puzzleInput.Length)
                {
                    Console.WriteLine($"out of memory");
                    break;
                }
                if (puzzleInput[oct] == 99)
                {
                    Console.WriteLine($"end of program");
                    break;
                }
                
                int operant = puzzleInput[oct] % 10;
                int first = puzzleInput[oct + 1];
                int secound = puzzleInput[oct + 2];
                
                if (operant == 1 || operant == 2)
                {
                    result = puzzleInput[oct + 3];
                    if (puzzleInput[oct] > 10)
                    {
                        int command = puzzleInput[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;

                        int firstValue = IsFistParamInPositionMode ? puzzleInput[first] : first;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secound] : secound;

                        if (operant == 1)
                        {
                            puzzleInput[result] = firstValue + secoundValue;
                        }
                        else if (operant == 2)
                        {
                            puzzleInput[result] = firstValue * secoundValue;
                        }
                    }
                    else
                    {
                        int resultFirs = puzzleInput[first];
                        int resultSec = puzzleInput[secound];
                        if (operant == 1)
                        {
                            puzzleInput[result] = resultFirs + resultSec;
                        }
                        else if (operant == 2)
                        {
                            puzzleInput[result] = resultFirs * resultSec;
                        }
                    }

                    oct += 4;
                }
                else if (operant == 3)
                {
                    result = puzzleInput[oct + 1];
                    oct += 2;
                    puzzleInput[result] = input;
                }
                else if (operant == 4)
                {
                    if (puzzleInput[oct] > 10)
                    {
                        int command = puzzleInput[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        if (IsFistParamInPositionMode)
                        {
                            result = puzzleInput[first];
                            Console.WriteLine($"Output: {puzzleInput[result]}");

                        }
                        else
                        {
                            Console.WriteLine($"Output: {first}");
                        }
                    }
                    else
                    {
                        result = puzzleInput[oct + 1];
                        Console.WriteLine($"Output: {puzzleInput[result]}");

                    }

                    oct += 2;
                }
                else if (operant == 5 || operant == 6)
                {
                    if (puzzleInput[oct] > 10)
                    {
                        int command = puzzleInput[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        int firstValue = IsFistParamInPositionMode ? puzzleInput[first] : first;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secound] : secound;
                        if (operant == 5)
                        {
                            if (firstValue != 0)
                            {
                                oct = secoundValue;
                                continue;
                            }
                            oct += 3;
                        }
                        if (operant == 6)
                        {
                            if (firstValue == 0)
                            {
                                oct = secoundValue;
                                continue;
                            }
                            oct += 3;
                        }
                    }
                    else
                    {
                        if (operant == 5)
                        {
                            if (puzzleInput[first] !=0 )
                            {
                                oct = puzzleInput[secound]; 
                                continue;
                            }
                            oct += 3;
                        }
                        if (operant == 6)
                        {
                            if (puzzleInput[first] == 0)
                            {
                                oct = puzzleInput[secound];
                                continue;
                            }
                            oct += 3;
                        }
                    }
                }
                else if (operant == 7 || operant == 8)
                {
                    result = puzzleInput[oct + 3];
                    if (puzzleInput[oct] > 10)
                    {
                        int command = puzzleInput[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        int firstValue = IsFistParamInPositionMode ? puzzleInput[first] : first;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secound] : secound;

                        if (operant == 7)
                        {
                            if (firstValue < secoundValue)
                            {
                                puzzleInput[result] = 1;
                            }
                            else
                            {
                                puzzleInput[result] = 0;
                            }
                        }
                        if (operant == 8)
                        {
                            if (firstValue == secoundValue)
                            {
                                puzzleInput[result] = 1;
                            }
                            else
                            {
                                puzzleInput[result] = 0;
                            }
                        }
                    }
                    else
                    {
                        if (operant == 7) 
                        {
                            if (puzzleInput[first] < puzzleInput[secound])
                            {
                                puzzleInput[result] = 1;
                            }
                            else
                            {
                                puzzleInput[result] = 0;
                            }
                        }
                        if (operant == 8)
                        {
                            if (puzzleInput[first] == puzzleInput[secound])
                            {
                                puzzleInput[result] = 1;
                            }
                            else
                            {
                                puzzleInput[result] = 0;
                            }
                        }
                    }

                    oct += 4;
                }
                else
                {
                    Console.WriteLine("BUM");
                    break;
                }
            }
        }

        private static void PartOne()
        {
            int result = 0;
            Console.WriteLine("Enter input: ");
            string keyInput = Console.ReadLine();
            int input = Int32.Parse(keyInput);
            string inputFromTxt = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            int[] puzzleInput = Array.ConvertAll(inputFromTxt.Split(','), s => Int32.Parse(s));
            int oct = 0;
            while (true)
            {
                if (oct > puzzleInput.Length)
                {
                    Console.WriteLine($"out of memory");
                    break;
                }
                if (puzzleInput[oct] == 99)
                {
                    Console.WriteLine($"index {oct} returs 99");
                    break;
                }

                int first = puzzleInput[oct + 1];
                int operant = puzzleInput[oct] % 10;
                if (operant == 1 || operant == 2)
                {

                    int secound = puzzleInput[oct + 2];
                    result = puzzleInput[oct + 3];
                    if (secound >= puzzleInput.Length || first >= puzzleInput.Length || result >= puzzleInput.Length)
                    {
                        Console.WriteLine(String.Join(',', puzzleInput));
                        break;
                    }

                    if (puzzleInput[oct] > 10)
                    {
                        int command = puzzleInput[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        int firstValue = IsFistParamInPositionMode ? puzzleInput[first] : first;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secound] : secound;

                        if (operant == 1)
                        {
                            puzzleInput[result] = firstValue + secoundValue;
                        }
                        else if (operant == 2)
                        {
                            puzzleInput[result] = firstValue * secoundValue;
                        }
                    }
                    else
                    {
                        int resultFirs = puzzleInput[first];
                        int resultSec = puzzleInput[secound];
                        if (operant == 1)
                        {
                            puzzleInput[result] = resultFirs + resultSec;
                        }
                        else if (operant == 2)
                        {
                            puzzleInput[result] = resultFirs * resultSec;
                        }
                    }

                    oct += 4;
                }
                else if (operant == 3)
                {
                    result = puzzleInput[oct + 1];
                    oct += 2;
                    puzzleInput[result] = input;
                }
                else if (operant == 4)
                {
                    if (puzzleInput[oct] > 10)
                    {
                        int command = puzzleInput[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        if (IsFistParamInPositionMode)
                        {
                            result = puzzleInput[first];
                            Console.WriteLine($"Oct 4 output: {puzzleInput[result]}, oct {oct}");

                        }
                        else
                        {
                            Console.WriteLine($"Oct 4 output: {first}, oct {oct}");
                        }
                    }
                    else
                    {
                        result = puzzleInput[oct + 1];
                        Console.WriteLine($"Oct 4 output: {puzzleInput[result]}, oct {oct}");

                    }


                    oct += 2;
                }
                else
                {
                    Console.WriteLine("BUM");
                    break;
                }
            }
        }

        private static string AddMissingZeros(int command)
        {
            string commandStr = command.ToString();
            while (commandStr.Length != 5) 
            {
                commandStr = "0" + commandStr;
            }

            return commandStr;
        }
    }
}