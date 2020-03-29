using System;
using System.Collections.Generic;
using System.Linq;

namespace day_9
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();
            Console.Read();
        }

        private static void PartOne()
        {
            //Console.WriteLine("Enter input: ");
            //string keyInput = Console.ReadLine();
            //long input = Int64.Parse(keyInput);
            long relativeBase = 0;
            long result = 0;
            List<long> ints = new List<long> { 104, 1125899906842624, 99 };
            int oct = 0;
            while (true)
            {
                if (oct > ints.Count)
                {
                    Console.WriteLine($"out of memory");
                    break;
                }
                if (ints[oct] == 99)
                {
                    Console.WriteLine($"end of program");
                    break;
                }

                long operant = ints[oct] % 10;
                long first = ints[oct + 1];
                long secound = ints[oct + 2];

                if (operant == 1 || operant == 2)
                {
                    result = ints[oct + 3];
                    if (ints[oct] > 10)
                    {
                        long command = ints[oct];
                        string fullCommand = AddMissingZeros(command);
                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;
#if DEBUG
                        Console.WriteLine($"full command: {fullCommand}");
#endif
                        int intsCountMinusIndex = ints.Count - 1;
                        if ((IsAddressParamInPositionMode && first > ints.Count) || (IsAddressParamInPositionMode && result > intsCountMinusIndex))
                        {
                            int missingZeroParams = (int)first - intsCountMinusIndex;
                            int missingZeroIndexResult = (int)result - intsCountMinusIndex;
                            int missingZeroIndex = missingZeroIndexResult > missingZeroParams ? missingZeroIndexResult : missingZeroParams;

                            for (int i = 0; i < missingZeroIndex; i++)
                            {
                                ints.Add(0);
                            }
                        }

                        long firstValue = IsFistParamInPositionMode ? ints[(int)first] : first;
                        long secoundValue = IsSecoundParamInPositionMode ? ints[(int)secound] : secound;

                        if (operant == 1)
                        {
                            ints[(int)result] = firstValue + secoundValue;
                        }
                        else if (operant == 2)
                        {
                            ints[(int)result] = firstValue * secoundValue;
                        }
                    }
                    else
                    {
                        long resultFirs = ints[(int)first];
                        long resultSec = ints[(int)secound];
                        if (operant == 1)
                        {
                            ints[(int)result] = resultFirs + resultSec;
                        }
                        else if (operant == 2)
                        {
                            ints[(int)result] = resultFirs * resultSec;
                        }
                    }

                    oct += 4;
                }
                else if (operant == 3)
                {
                    result = ints[oct + 1];
                    oct += 2;
                    //ints[(int)result] = input;
                }
                else if (operant == 4)
                {
                    if (ints[oct] > 10)
                    {
                        int command = (int)ints[oct];
                        string fullCommand = AddMissingZeros(command);
                        Console.WriteLine($"full command: {fullCommand}");
                        //for 203 error, full command is 00104
                        bool IsFirstParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsFirstParamInRelativeMode = (Int32.Parse(fullCommand[2].ToString()) == 2) ? true : false;
                        if (IsFirstParamInPositionMode)
                        {
                            result = (int)ints[(int)first];
                            Console.WriteLine($"Output: {(int)ints[(int)result]}");

                        }
                        else if (IsFirstParamInRelativeMode)
                        {
                            //Console.WriteLine($"It is relative base");
                            int memoryAddress = (int)(relativeBase + first);
                            //Console.WriteLine($"memoryAddress: {memoryAddress}");
                            Console.WriteLine($"Output: {ints[memoryAddress]}");
                        }
                        else 
                        {
                            Console.WriteLine($"Output: {first}");
                        }
                    }
                    else
                    {
                        result = ints[oct + 1];
                        Console.WriteLine($"Output: {ints[(int)result]}");

                    }

                    oct += 2;
                }
                else if (operant == 5 || operant == 6)
                {
                    if (ints[oct] > 10)
                    {
                        long command = ints[oct];
                        string fullCommand = AddMissingZeros(command);
#if DEBUG
                        Console.WriteLine($"full command: {fullCommand}");
#endif
                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        //bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;

                        long firstValue = IsFistParamInPositionMode ? ints[(int)first] : first;
                        long secoundValue = IsSecoundParamInPositionMode ? ints[(int)secound] : secound;

                        if (operant == 5)
                        {
                            if (firstValue != 0)
                            {
                                oct = (int)secoundValue;
                                continue;
                            }
                            oct += 3;
                        }
                        if (operant == 6)
                        {
                            if (firstValue == 0)
                            {
                                oct = (int)secoundValue;
                                continue;
                            }
                            oct += 3;
                        }
                    }
                    else
                    {
                        if (operant == 5)
                        {
                            if (ints[(int)first] != 0)
                            {
                                oct = (int)ints[(int)secound];
                                continue;
                            }
                            oct += 3;
                        }
                        if (operant == 6)
                        {
                            if (ints[(int)first] == 0)
                            {
                                oct = (int)ints[(int)secound];
                                continue;
                            }
                            oct += 3;
                        }
                    }
                }
                else if (operant == 7 || operant == 8)
                {
                    result = ints[oct + 3];
                    if (ints[oct] > 10)
                    {
                        int command = (int)ints[oct];
                        string fullCommand = AddMissingZeros(command);
#if DEBUG
                        Console.WriteLine($"full command: {fullCommand}");
#endif
                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        //bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;
                        int intsCountMinusIndex = ints.Count - 1;
                        if ( (IsFistParamInPositionMode && first > ints.Count) || (IsFistParamInPositionMode && result > intsCountMinusIndex))
                        {
                            int missingZeroParams = (int)first - intsCountMinusIndex;
                            int missingZeroIndexResult = (int)result - intsCountMinusIndex;
                            int missingZeroIndex = missingZeroIndexResult>missingZeroParams ?missingZeroIndexResult : missingZeroParams;

                            //Console.WriteLine($"it is bigger, adding missing {missingZeroIndex} zeros");

                            for (int i = 0; i < missingZeroIndex; i++)
                            {
                                ints.Add(0);
                            }
                        }



                        long firstValue = IsFistParamInPositionMode ? ints[(int)first] : first;
                        long secoundValue = IsSecoundParamInPositionMode ? ints[(int)secound] : secound;

                        if (operant == 7)
                        {
                            if (firstValue < secoundValue)
                            {
                                ints[(int)result] = 1;
                            }
                            else
                            {
                                ints[(int)result] = 0;
                            }
                        }
                        if (operant == 8)
                        {
                            if (firstValue == secoundValue)
                            {
                                ints[(int)result] = 1;
                            }
                            else
                            {
                                ints[(int)result] = 0;
                            }
                        }
                    }
                    else
                    {
                        if (operant == 7)
                        {
                            if (ints[(int)first] < ints[(int)secound])
                            {
                                ints[(int)result] = 1;
                            }
                            else
                            {
                                ints[(int)result] = 0;
                            }
                        }
                        if (operant == 8)
                        {
                            if (ints[(int)first] == ints[(int)secound])
                            {
                                ints[(int)result] = 1;
                            }
                            else
                            {
                                ints[(int)result] = 0;
                            }
                        }
                    }

                    oct += 4;
                }
                else if (operant == 9) 
                {
#if DEBUG
                    Console.WriteLine("It's nine!");

                    int command = (int)ints[oct];
                    string fullCommand = AddMissingZeros(command);
                    Console.WriteLine($"full command: {fullCommand}");
#endif
                    relativeBase = first + relativeBase;
#if DEBUG
                    Console.WriteLine($"new relative base: {relativeBase}");
#endif
                    oct += 2;
                }
                else
                {
                    Console.WriteLine("BUM");
                    break;
                }
            }
        }

        private static string AddMissingZeros(long command)
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