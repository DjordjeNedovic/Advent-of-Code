using System;
using System.Collections.Generic;

namespace day_9
{
    class Program
    {
        static bool runProgram = true;
        static List<long> octcodeList = new List<long>() { 109, -1, 4, 1, 99 };
        static long relativeBaseResult = 0;
        static int octcodeListIndex = 0;

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
#if DEBUG
                        Console.WriteLine($"Operation.INSTRACTION_1_ADDING, optcode {currentOctcode}, optcode index {octcodeListIndex}");
#endif
                        Adding(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_2_MULTIPLING):
#if DEBUG
                        Console.WriteLine($"Operation.INSTRACTION_2_MULTIPLING, optcode {currentOctcode}, optcode index {octcodeListIndex}");
#endif
                        Multiplaing(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_3_WRITE_INPUT):
#if DEBUG
                        Console.WriteLine($"Operation.INSTRACTION_3_WRITE_INPUT, optcode {currentOctcode}, optcode index {octcodeListIndex}");
#endif
                        WriteInput(currentOctcode, input);
                        break;
                    case (Operation.INSTRACTION_4_OUTPUT):
#if DEBUG
                        Console.WriteLine($"Operation.INSTRACTION_4_OUTPUT, optcode {currentOctcode}, optcode index {octcodeListIndex}");
#endif
                        OutputValue(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_5_JUMP_IF_TRUE):
#if DEBUG
                        Console.WriteLine($"Operation.INSTRACTION_5_JUMP_IF_TRUE, optcode {currentOctcode}, optcode index {octcodeListIndex}");
#endif
                        JumpIfTrue(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_6_JUMP_IF_FALSE):
#if DEBUG
                        Console.WriteLine($"Operation.INSTRACTION_6_JUMP_IF_FALSE, optcode {currentOctcode}, optcode index {octcodeListIndex}");
#endif
                        JumpIfFalse(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_7_LESS_THAN):
#if DEBUG
                        Console.WriteLine($"Operation.INSTRACTION_7_LESS_THAN, optcode {currentOctcode}, optcode index {octcodeListIndex}");
#endif
                        LessThan(currentOctcode);
                        octcodeListIndex += 4;
                        break;
                    case (Operation.INSTRACTION_8_EQUALS):
#if DEBUG
                        Console.WriteLine($"Operation.INSTRACTION_8_EQUALS, optcode {currentOctcode}, optcode index {octcodeListIndex}");
#endif
                        EqualsOpetation(currentOctcode);
                        octcodeListIndex += 4;
                        break;
                    case (Operation.INSTRACTION_9_RELATIVE_BASE):
#if DEBUG
                        Console.WriteLine($"Operation.INSTRACTION_9_RELATIVE_BASE, optcode {currentOctcode}, optcode index {octcodeListIndex}");
#endif
                        RelativeBaseOperations(currentOctcode);
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

        private static void RelativeBaseOperations(long currentOctcode)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            string fullCommand = AddMissingZeros(currentOctcode);
            if (Int32.Parse(fullCommand[2].ToString()) == 0)
            {
                relativeBaseResult = octcodeList[(int)operationFirstParametar] + relativeBaseResult;
            }
            else if (Int32.Parse(fullCommand[2].ToString()) == 1)
            {
                // In immediate mode, a parameter is interpreted as a value - if the parameter is 50, its value is simply 50.
                relativeBaseResult = operationFirstParametar + relativeBaseResult;
            }
            else if (Int32.Parse(fullCommand[2].ToString()) == 2)
            {
                long t = 0;
                //relativeBase = ints[(int)(first + relativeBase)];
                if (relativeBaseResult < octcodeList.Count)
                {
                    t = octcodeList[(int)relativeBaseResult];
                }
                else
                {
                    t = octcodeList[0];
                }
                relativeBaseResult = octcodeList[(int)(t + operationFirstParametar)] + relativeBaseResult;
            }
            else
            {
                Console.WriteLine($"Error: Unknown OppCode {currentOctcode}");
            }
#if DEBUG
            Console.WriteLine($"new relative base: {relativeBaseResult}");
#endif
        }

        private static void EqualsOpetation(long currentOctcode)
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

        private static void LessThan(long currentOctcode)
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

        private static void JumpIfFalse(long currentOctcode)
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
                octcodeListIndex = (int)secoundValue;
            }
            else
            {
                octcodeListIndex += 3;
            }
        }

        private static void JumpIfTrue(long currentOctcode)
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
                octcodeListIndex = (int)secoundValue;
            }
            else 
            {
                octcodeListIndex += 3;
            }
        }

        private static void OutputValue(long currentOctcode)
        {
            if (currentOctcode > 10)
            {
                long operationFirstParametar = octcodeList[octcodeListIndex + 1];
                long operationResultParametar = 0;
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
                    Console.WriteLine($"Error: Unknown OppCode {currentOctcode}");
                }
            }
            else
            {
                long operationResultParametar = octcodeList[octcodeListIndex + 1];
                Console.WriteLine($"Output: {octcodeList[(int)operationResultParametar]}");
            }

            octcodeListIndex += 2;
        }

        private static void WriteInput(long currentOctcode, long input)
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

            octcodeListIndex += 2;
        }

        private static void Multiplaing(long currentOctcode)
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
            octcodeListIndex += 4;
        }

        private static void Adding(long currentOctcode)
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

            octcodeListIndex += 4;
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
        MODE_1_POSSITION_MODE = 0,
        MODE_2_PARAMETER_MODE = 1,
        MODE_3_RELATIVE_MODE = 2,
        UNKNOWN = 99
    }
}