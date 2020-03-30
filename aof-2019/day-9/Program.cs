using System;
using System.Collections.Generic;
using System.Linq;

namespace day_9
{
    class Program
    {
        static bool runProgram = true;
        static List<long> octcodeList = new List<long>() { };
        static long relativeBaseResult = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter input: ");
            string keyInput = Console.ReadLine();
            long input = Int64.Parse(keyInput);
            Intercode(input);

            Console.ReadLine();
        }

        public static void Intercode(long input) 
        {
            Operation operation = 0;
            int octcodeListIndex = 0;
            while (runProgram) 
            {
                if (octcodeListIndex > octcodeList.Count) 
                {
                    Console.WriteLine($"Out of memory");
                    break;
                }

                long currentOctcode = octcodeList[octcodeListIndex];
                if (currentOctcode > 99)
                {
                    operation = (Operation)(currentOctcode % 10);

                }
                else 
                {
                    operation = (Operation)(currentOctcode);
                }

                switch (operation) 
                {
                    case (Operation.INSTRACTION_1_ADDING):
                        Adding(currentOctcode, octcodeListIndex);
                        octcodeListIndex += 4;
                        break;
                    case (Operation.INSTRACTION_2_MULTIPLING):
                        Multiplaing(currentOctcode, octcodeListIndex);
                        octcodeListIndex += 4;
                        break;
                    case (Operation.INSTRACTION_3_WRITE_INPUT):
                        WriteInput(currentOctcode, octcodeListIndex, input);
                        octcodeListIndex += 2;
                        break;
                    case (Operation.INSTRACTION_4_OUTPUT):
                        OutputValue(currentOctcode, octcodeListIndex);
                        octcodeListIndex += 2;
                        break;
                    case (Operation.INSTRACTION_5_JUMP_IF_TRUE):
                        octcodeListIndex += JumpIfTrue(currentOctcode, octcodeListIndex);
                        break;
                    case (Operation.INSTRACTION_6_JUMP_IF_FALSE):
                        octcodeListIndex += JumpIfFalse(currentOctcode, octcodeListIndex);
                        break;
                    case (Operation.INSTRACTION_7_LESS_THAN):
                        LessThan(currentOctcode, octcodeListIndex);
                        octcodeListIndex += 4;
                        break;
                    case (Operation.INSTRACTION_8_EQUALS):
                        EqualsOpetation(currentOctcode, octcodeListIndex);
                        octcodeListIndex += 4;
                        break;
                    case (Operation.INSTRACTION_9_RELATIVE_BASE):
                        RelativeBaseOperations(currentOctcode, octcodeListIndex);
                        octcodeListIndex += 2;
                        break;
                    case (Operation.INSTRACTION_99_HALT):
                        Exit();
                        break;
                    case (Operation.UNKNOWN):
                    default:
                        Console.WriteLine($"Error: Unknown OppCode {currentOctcode}");
                        runProgram = false;
                        break;
                }
            }
        }

        private static void Exit()
        {
            Console.WriteLine("Code halted");
            runProgram = false;
        }

        private static void RelativeBaseOperations(long currentOctcode, int octcodeListIndex)
        {
            string fullCommand = AddMissingZeros(currentOctcode);
            Mode firstParamerMode = (Mode)Int32.Parse(fullCommand[2].ToString());
            Mode secpundParamerMode = (Mode)Int32.Parse(fullCommand[1].ToString());
            Mode thirdParameterMode = (Mode)Int32.Parse(fullCommand[0].ToString());
        }

        private static void EqualsOpetation(long currentOctcode, int octcodeListIndex)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            long operationSecondParametar = octcodeList[octcodeListIndex + 2];
            long operationResultParametar = octcodeList[octcodeListIndex + 3];

            long firstValue = 0;
            long secoundValue = 0;
            if (currentOctcode > 10)
            {
                long command = octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
                bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;

                int intsCountMinusIndex = octcodeList.Count - 1;
                if (IsFistParamInPositionMode && (operationFirstParametar > octcodeList.Count || operationResultParametar > intsCountMinusIndex))
                {
                    int missingZeroParams = (int)operationFirstParametar - intsCountMinusIndex;
                    int missingZeroIndexResult = (int)operationResultParametar - intsCountMinusIndex;
                    int missingZeroIndex = missingZeroIndexResult > missingZeroParams ? missingZeroIndexResult : missingZeroParams;
                    for (int i = 0; i < missingZeroIndex; i++)
                    {
                        octcodeList.Add(0);
                    }
                }

                firstValue = IsFistParamInPositionMode ? octcodeList[(int)operationFirstParametar] : operationFirstParametar;
                secoundValue = IsSecoundParamInPositionMode ? octcodeList[(int)operationSecondParametar] : operationSecondParametar;
            }
            else
            {
                firstValue = octcodeList[(int)operationFirstParametar];
                secoundValue = octcodeList[(int)operationSecondParametar];
            }

            if (firstValue == secoundValue)
            {
                octcodeList[(int)operationResultParametar] = 1;
            }
            else
            {
                octcodeList[(int)operationResultParametar] = 0;
            }
        }

        private static void LessThan(long currentOctcode, int octcodeListIndex)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            long operationSecondParametar = octcodeList[octcodeListIndex + 2];
            long operationResultParametar = octcodeList[octcodeListIndex + 3];

            long firstValue = 0;
            long secoundValue = 0;
            if (currentOctcode > 10)
            {
                long command = octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
                bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;

                int intsCountMinusIndex = octcodeList.Count - 1;
                if (IsFistParamInPositionMode && (operationFirstParametar > octcodeList.Count || operationResultParametar > intsCountMinusIndex))
                {
                    int missingZeroParams = (int)operationFirstParametar - intsCountMinusIndex;
                    int missingZeroIndexResult = (int)operationResultParametar - intsCountMinusIndex;
                    int missingZeroIndex = missingZeroIndexResult > missingZeroParams ? missingZeroIndexResult : missingZeroParams;
                    for (int i = 0; i < missingZeroIndex; i++)
                    {
                        octcodeList.Add(0);
                    }
                }

                firstValue = IsFistParamInPositionMode ? octcodeList[(int)operationFirstParametar] : operationFirstParametar;
                secoundValue = IsSecoundParamInPositionMode ? octcodeList[(int)operationSecondParametar] : operationSecondParametar;
            }
            else
            {
                firstValue = octcodeList[(int)operationFirstParametar];
                secoundValue = octcodeList[(int)operationSecondParametar];
            }

            if (firstValue < secoundValue)
            {
                octcodeList[(int)operationResultParametar] = 1;
            }
            else
            {
                octcodeList[(int)operationResultParametar] = 0;
            }
        }

        private static int JumpIfFalse(long currentOctcode, int octcodeListIndex)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            long operationSecondParametar = octcodeList[octcodeListIndex + 2];
            long operationResultParametar = octcodeList[octcodeListIndex + 3];
            long firstValue = 0;
            long secoundValue = 0;
            if (currentOctcode > 10)
            {
                long command = octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
                bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;

                int intsCountMinusIndex = octcodeList.Count - 1;
                if (IsFistParamInPositionMode && (operationFirstParametar > octcodeList.Count || operationResultParametar > intsCountMinusIndex))
                {
                    int missingZeroParams = (int)operationFirstParametar - intsCountMinusIndex;
                    int missingZeroIndexResult = (int)operationResultParametar - intsCountMinusIndex;
                    int missingZeroIndex = missingZeroIndexResult > missingZeroParams ? missingZeroIndexResult : missingZeroParams;
                    for (int i = 0; i < missingZeroIndex; i++)
                    {
                        octcodeList.Add(0);
                    }
                }

                firstValue = IsFistParamInPositionMode ? octcodeList[(int)operationFirstParametar] : operationFirstParametar;
                secoundValue = IsSecoundParamInPositionMode ? octcodeList[(int)operationSecondParametar] : operationSecondParametar;
            }
            else
            {
                firstValue = octcodeList[(int)operationFirstParametar];
                secoundValue = octcodeList[(int)operationSecondParametar];
            }

            if (firstValue == 0)
            {
                return (int)secoundValue;
            }

            return 3;
        }

        private static int JumpIfTrue(long currentOctcode, int octcodeListIndex)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            long operationSecondParametar = octcodeList[octcodeListIndex + 2];
            long firstValue = 0;
            long secoundValue = 0;
            if (currentOctcode > 10)
            {
                long command = octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
                bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;

                firstValue = IsFistParamInPositionMode ? octcodeList[(int)operationFirstParametar] : operationFirstParametar;
                secoundValue = IsSecoundParamInPositionMode ? octcodeList[(int)operationSecondParametar] : operationSecondParametar;
            }
            else 
            {
                firstValue = octcodeList[(int)operationFirstParametar];
                secoundValue = octcodeList[(int)operationSecondParametar];
            }

            if (firstValue != 0)
            {
                return (int)secoundValue;
            }

            return 3;
        }

        private static void OutputValue(long currentOctcode, int octcodeListIndex)
        {
            if (currentOctcode > 10)
            {
                long operationFirstParametar = octcodeList[octcodeListIndex + 1];
                long operationResultParametar = octcodeList[octcodeListIndex + 3];
                int command = (int)octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
                bool IsFirstParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                bool IsFirstParamInImmedMode = (Int32.Parse(fullCommand[2].ToString()) == 1) ? true : false;
                bool IsFirstParamInRelativeMode = (Int32.Parse(fullCommand[2].ToString()) == 2) ? true : false;
                if (IsFirstParamInPositionMode)
                {
                    operationResultParametar = (int)octcodeList[(int)operationFirstParametar];
                    Console.WriteLine($"Output: {(int)octcodeList[(int)operationResultParametar]}");

                }
                else if (IsFirstParamInImmedMode)
                {
                    Console.WriteLine($"Output: {operationFirstParametar}");
                }
                else if (IsFirstParamInRelativeMode)
                {
                    int memoryAddress = (int)(relativeBaseResult + operationFirstParametar);
                    Console.WriteLine($"Output: {octcodeList[memoryAddress]}");
                }
                else
                {
                    Console.WriteLine("Shit just become real");
                }
            }
            else
            {
                long operationResultParametar = octcodeList[octcodeListIndex + 1];
                Console.WriteLine($"Output: {octcodeList[(int)operationResultParametar]}");
            }
        }

        private static void WriteInput(long currentOctcode, int octcodeListIndex, long input)
        {
            if (currentOctcode > 10)
            {
                long operationFirstParametar = octcodeList[octcodeListIndex + 1];
                int command = (int)octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
                bool IsFirstParamInRelativeMode = (Int32.Parse(fullCommand[2].ToString()) == 2) ? true : false;
                if (IsFirstParamInRelativeMode)
                {
                    int memoryAddress = (int)(relativeBaseResult + operationFirstParametar);
                    octcodeList[(int)memoryAddress] = input;
                }
                else
                {
                    Console.WriteLine("Shit just become real");
                }
            }
            else
            {
                long operationResultParametar = octcodeList[octcodeListIndex + 1];
                octcodeList[(int)operationResultParametar] = input;
            }
        }

        private static void Multiplaing(long currentOctcode, int octcodeListIndex)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            long operationSecondParametar = octcodeList[octcodeListIndex + 2];
            long operationResultParametar = octcodeList[octcodeListIndex + 3];
            if (currentOctcode > 10)
            {
                long command = currentOctcode;
                string fullCommand = AddMissingZeros(command);
                bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;

                int intsCountMinusIndex = octcodeList.Count - 1;
                if (IsAddressParamInPositionMode && (operationFirstParametar > octcodeList.Count || operationResultParametar > intsCountMinusIndex))
                {
                    int missingZeroParams = (int)operationFirstParametar - intsCountMinusIndex;
                    int missingZeroIndexResult = (int)operationResultParametar - intsCountMinusIndex;
                    int missingZeroIndex = missingZeroIndexResult > missingZeroParams ? missingZeroIndexResult : missingZeroParams;
                    for (int i = 0; i < missingZeroIndex; i++)
                    {
                        octcodeList.Add(0);
                    }
                }

                long resultFirst = IsFistParamInPositionMode ? octcodeList[(int)operationFirstParametar] : operationFirstParametar;
                long resultSec = IsSecoundParamInPositionMode ? octcodeList[(int)operationSecondParametar] : operationSecondParametar;
                octcodeList[(int)operationResultParametar] = resultFirst * resultSec;
            }
            else
            {
                long resultFirst = octcodeList[(int)operationFirstParametar];
                long resultSec = octcodeList[(int)operationSecondParametar];
                octcodeList[(int)operationResultParametar] = resultFirst * resultSec;
            }
        }

        private static void Adding(long currentOctcode, int octcodeListIndex)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            long operationSecondParametar = octcodeList[octcodeListIndex + 2];
            long operationResultParametar = octcodeList[octcodeListIndex + 3];
            if (currentOctcode > 10)
            {
                long command = currentOctcode;
                string fullCommand = AddMissingZeros(command);
                bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;

                int intsCountMinusIndex = octcodeList.Count - 1;
                if (IsAddressParamInPositionMode && (operationFirstParametar > octcodeList.Count || operationResultParametar > intsCountMinusIndex))
                {
                    int missingZeroParams = (int)operationFirstParametar - intsCountMinusIndex;
                    int missingZeroIndexResult = (int)operationResultParametar - intsCountMinusIndex;
                    int missingZeroIndex = missingZeroIndexResult > missingZeroParams ? missingZeroIndexResult : missingZeroParams;
                    for (int i = 0; i < missingZeroIndex; i++)
                    {
                        octcodeList.Add(0);
                    }
                }

                long resultFirst = IsFistParamInPositionMode ? octcodeList[(int)operationFirstParametar] : operationFirstParametar;
                long resultSec = IsSecoundParamInPositionMode ? octcodeList[(int)operationSecondParametar] : operationSecondParametar;
                octcodeList[(int)operationResultParametar] = resultFirst + resultSec;
            }
            else
            {
                long resultFirst = octcodeList[(int)operationFirstParametar];
                long resultSec = octcodeList[(int)operationSecondParametar];
                octcodeList[(int)operationResultParametar] = resultFirst + resultSec;
            }
        }

        private static void PartOne()
        {
            Console.WriteLine("Enter input: ");
            string keyInput = Console.ReadLine();
            long input = Int64.Parse(keyInput);
            long relativeBase = 0;
            long result = 0;
            List<long> ints = new List<long> { 109, 1, 203, 2, 204, 2, 99 };
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
                        //Console.WriteLine($"full command: {fullCommand}");
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
                    if (ints[oct] > 10)
                    {
                        int command = (int)ints[oct];
                        string fullCommand = AddMissingZeros(command);
#if DEGUB
                        Console.WriteLine($"full command: {fullCommand}");
#endif
                        bool IsFirstParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsFirstParamInImmedMode = (Int32.Parse(fullCommand[2].ToString()) == 1) ? true : false;
                        bool IsFirstParamInRelativeMode = (Int32.Parse(fullCommand[2].ToString()) == 2) ? true : false;
                        
                        if (IsFirstParamInRelativeMode)
                        {
                            int memoryAddress = (int)(relativeBase + first);
                            ints[(int)memoryAddress] = input;
                        }
                        else
                        {
                            Console.WriteLine("Shit just become real");
                        }
                    }
                    else
                    {
                        result = ints[oct + 1];
                        
                        ints[(int)result] = input;
                    }

                    oct += 2;
                }
                else if (operant == 4)
                {
                    if (ints[oct] > 10)
                    {
                        int command = (int)ints[oct];
                        string fullCommand = AddMissingZeros(command);
#if DEGUB
                        Console.WriteLine($"full command: {fullCommand}");
#endif
                        bool IsFirstParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsFirstParamInImmedMode = (Int32.Parse(fullCommand[2].ToString()) == 1) ? true : false;
                        bool IsFirstParamInRelativeMode = (Int32.Parse(fullCommand[2].ToString()) == 2) ? true : false;
                        if (IsFirstParamInPositionMode)
                        {
                            result = (int)ints[(int)first];
                            Console.WriteLine($"Output: {(int)ints[(int)result]}");

                        }
                        else if(IsFirstParamInImmedMode)
                        {
                            Console.WriteLine($"Output: {first}");
                        }
                        else if (IsFirstParamInRelativeMode)
                        {
                            int memoryAddress = (int)(relativeBase + first);
                            Console.WriteLine($"Output: {ints[memoryAddress]}");
                        }
                        else 
                        {
                            Console.WriteLine("Shit just become real");
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
                    int command = (int)ints[oct];
                    string fullCommand = AddMissingZeros(command);
                    bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                    if (Int32.Parse(fullCommand[2].ToString()) == 0)
                    {
#if DEBUG
                        Console.WriteLine("mode 0");
                        var valueFromFirstAddress = ints[(int)first];
                        Console.WriteLine($"valueFromFirstAddress: {valueFromFirstAddress}");
                        var valueFromRelativeBaseAddress = ints[(int)relativeBase];
                        Console.WriteLine($"valueFromRelativeBaseAddress: {valueFromRelativeBaseAddress}");
                        Console.WriteLine("Position mode for 9");
#endif

                        // In position mode, its value is the value stored at address
                        relativeBase = ints[(int)first] + relativeBase;
                    }
                    else if (Int32.Parse(fullCommand[2].ToString()) == 1)
                    {
#if DEBUG
                        Console.WriteLine("mode 1");
#endif
                        // In immediate mode, a parameter is interpreted as a value - if the parameter is 50, its value is simply 50.
                        relativeBase = first + relativeBase;
                    }
                    else if (Int32.Parse(fullCommand[2].ToString()) == 2)
                    {
#if DEBUG
                    Console.WriteLine("mode 2");
#endif
                        long t = 0; 
                        //relativeBase = ints[(int)(first + relativeBase)];
                        if (relativeBase < ints.Count)
                        {
                            t = ints[(int)relativeBase];
                        }
                        else 
                        {
                            t = ints[0];
                        }
                        relativeBase = ints[ (int)(t + first)] + relativeBase;
                    }
                    else 
                    {
                        Console.WriteLine("Shit just become real.");
                    }
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

    enum Operation 
    {
        INSTRACTION_1_ADDING = 1,
        INSTRACTION_2_MULTIPLING = 2,
        INSTRACTION_3_WRITE_INPUT = 3,
        INSTRACTION_4_OUTPUT = 4,
        INSTRACTION_5_JUMP_IF_TRUE = 5,
        INSTRACTION_6_JUMP_IF_FALSE =6,
        INSTRACTION_7_LESS_THAN = 7,
        INSTRACTION_8_EQUALS = 8,
        INSTRACTION_9_RELATIVE_BASE =9,
        INSTRACTION_99_HALT = 99,
        UNKNOWN = 0
    }

    enum Mode 
    {
        MODE_1_POSSITION_MODE = 1,
        MODE_2_PARAMETER_MODE = 2,
        MODE_3_RELATIVE_MODE = 3,
        UNKNOWN = 0
    }
}