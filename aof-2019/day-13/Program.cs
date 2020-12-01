using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace day_13
{
    class Program
    {
        static bool runProgram = true;
        static List<long> octcodeList;
        static long relativeBaseResult = 0;
        static int octcodeListIndex = 0;
        static List<int> cordinates = new List<int>();
        static int previousPadleX = 0;
        static int previousPadleY = 0;
        static int previousBallX = 0;
        static int previousBallY = 0;


        static void Main(string[] args)
        {
            string inputFromTxt = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            octcodeList = (Array.ConvertAll(inputFromTxt.Split(','), s => Int64.Parse(s))).ToList();

            //change puzzleInput, first element (on memory address 0) to 2 for part two
            if (octcodeList[0] != 2)
            {
                Intercode(0, true);
                PartOne();
            }
            else 
            {
                PartTwo();
            }
        }
               
        private static void PartOne()
        {
            int blockCounter = 0;
            for (int i = 0; i < cordinates.Count; i += 3)
            {
                int x = cordinates.ElementAt(i);
                int y = cordinates.ElementAt(i + 1);
                int type = cordinates.ElementAt(i + 2);
                Console.SetCursorPosition(x, y);
                switch (type)
                {
                    case 0:
                        Console.WriteLine();
                        break;
                    case 1:
                        Console.WriteLine("#");
                        break;
                    case 2:
                        blockCounter++;
                        Console.WriteLine("%");
                        break;
                    case 3:
                        Console.WriteLine("_");
                        break;
                    case 4:
                        Console.WriteLine("*");
                        break;
                    default:

                        break;
                }
            }

            Console.WriteLine($"blockCounter {blockCounter}");
            Console.ReadLine();
        }

        private static void PartTwo()
        {
            Intercode(0, false);
        }

        private static int MovePaddle(int moveIndex, int previousPadleX, int previousPadleY)
        {
            Console.SetCursorPosition(0, 30);
            Console.WriteLine("###################################");
            Console.WriteLine($"DEBUG STATE: paddle possition before move ({previousPadleX},{previousPadleY})");

            RemoveField(previousPadleX, previousPadleY);
            int newPadleX = previousPadleX + moveIndex;
            Console.SetCursorPosition(0, 32);
            Console.WriteLine($"DEBUG STATE: paddle possition after move ({newPadleX},{previousPadleY})");

            Console.SetCursorPosition(newPadleX, previousPadleY);
            Console.WriteLine("_");

            return newPadleX;
        }

        private static void RemoveField(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        public static void Intercode(long input, bool isPartOne)
        {
            int g = 1;
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
                        Adding(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_2_MULTIPLING):
                        Multiplaing(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_3_WRITE_INPUT):
                        WriteInput(currentOctcode, input);
                        break;
                    case (Operation.INSTRACTION_4_OUTPUT):
                        if (isPartOne)
                        {
                            OutputValue(currentOctcode);
                            break;
                        }
                        else 
                        {
                            int cordinate = ReturnOutputValue(currentOctcode);
                            cordinates.Add(cordinate);
                            if (cordinates.Count == 3) 
                            {
                                WriteGrid(cordinates);
                                cordinates.Clear();
                                input = CalculateNewInput();
                            }
                            Thread.Sleep(20);
                            break;
                        }

                    case (Operation.INSTRACTION_5_JUMP_IF_TRUE):
                        JumpIfTrue(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_6_JUMP_IF_FALSE):
                        JumpIfFalse(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_7_LESS_THAN):
                        LessThan(currentOctcode);

                        break;
                    case (Operation.INSTRACTION_8_EQUALS):
                        EqualsOpetation(currentOctcode);

                        break;
                    case (Operation.INSTRACTION_9_RELATIVE_BASE):
                        RelativeBaseOperations(currentOctcode);

                        break;
                    case (Operation.INSTRACTION_99_HALT):
                        Exit();
                        break;
                    case (Operation.UNKNOWN):
                    default:
                        Console.WriteLine($"Error: Unknown OppCode {currentOctcode}, method Intercode()");
                        runProgram = false;
                        break;
                }
            }
        }

        private static long CalculateNewInput()
        {
            Console.SetCursorPosition(0, 27);
            Console.WriteLine($"DEBUG STATE: ball possition ({previousBallX},{previousBallY})");
            Console.WriteLine($"DEBUG STATE: paddle possition ({previousPadleX},{previousPadleY})");

            if (previousBallX != 0 && previousBallY != 0)
            {
                if (previousPadleX > previousBallX)
                {
                    return -1;
                }
                else if (previousPadleX < previousBallX)
                {
                    return 1;
                }
                else 
                {
                    return 0;
                }
            }

            return 0;
        }

        private static void WriteGrid(List<int> cordinates)
        {
            int x = cordinates.ElementAt(0);
            int y = cordinates.ElementAt(1);
            int type = cordinates.ElementAt(2);

            if (x == -1 && y == 0)
            {
                Console.SetCursorPosition(0, 25);
                Console.WriteLine($"current score {type}");
                return;
            }

            switch (type)
            {
                case 0:
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine();
                    break;
                case 1:
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("#");
                    break;
                case 2:
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("%");
                    break;
                case 3:
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("_");
                    previousPadleX = x;
                    previousPadleY = y;
                    break;
                case 4:
                    RemoveField(previousBallX, previousBallY);
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("*");
                    previousBallX = x;
                    previousBallY = y;

                    break;
                default:
                    break;
            }
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
                bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;
                AddZerosAsElementsInList(operationFirstParametar, operationResultParametar, IsAddressParamInPositionMode);

                long resultFirst = Calculate(currentOctcode, operationFirstParametar, fullCommand[2].ToString());
                long resultSec = Calculate(currentOctcode, operationSecondParametar, fullCommand[1].ToString());
                StoreValue(currentOctcode, operationResultParametar, fullCommand[0].ToString(), resultFirst + resultSec);
            }
            else
            {
                long resultFirst = octcodeList[(int)operationFirstParametar];
                long resultSec = octcodeList[(int)operationSecondParametar];
                octcodeList[(int)operationResultParametar] = resultFirst + resultSec;
            }

            octcodeListIndex += 4;
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

                if (Int32.Parse(fullCommand[0].ToString()) == 0)
                {
                    AddZerosAsElementsInList(operationFirstParametar, operationResultParametar, true);
                }
                else if (Int32.Parse(fullCommand[0].ToString()) == 2) 
                {
                    AddZerosAsElementsInList( operationResultParametar, relativeBaseResult, true);
                }
                
                long resultFirst = Calculate(currentOctcode, operationFirstParametar, fullCommand[2].ToString());
                long resultSec = Calculate(currentOctcode, operationSecondParametar, fullCommand[1].ToString());
                StoreValue(currentOctcode, operationResultParametar, fullCommand[0].ToString(), resultFirst * resultSec);
            }
            else
            {
                long resultFirst = octcodeList[(int)operationFirstParametar];
                long resultSec = octcodeList[(int)operationSecondParametar];

                octcodeList[(int)operationResultParametar] = resultFirst * resultSec;
            }

            octcodeListIndex += 4;
        }

        private static void WriteInput(long currentOctcode, long input)
        {
            long address = 0;
            if (currentOctcode > 10)
            {
                int command = (int)octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
                long operationFirstParametar = octcodeList[octcodeListIndex + 1];
                Mode firstParamerMode = (Mode)Int32.Parse(fullCommand[2].ToString());
                switch (firstParamerMode)
                {
                    case (Mode.MODE_0_POSSITION_MODE):
                        octcodeList[(int)operationFirstParametar] = input;
                        break;
                    case (Mode.MODE_2_RELATIVE_MODE):
                        address = operationFirstParametar + relativeBaseResult;
                        octcodeList[(int)address] = input;
                        break;
                    case (Mode.UNKNOWN):
                    default:
                        Console.WriteLine($"Error {currentOctcode}, method WriteInput()");
                        break;
                }
            }
            else
            {
                address = octcodeList[octcodeListIndex + 1];
                octcodeList[(int)address] = input;
            }

            octcodeListIndex += 2;
        }

        private static void OutputValue(long currentOctcode)
        {
            long result = 0;
            if (currentOctcode > 10)
            {
                long operationFirstParametar = octcodeList[octcodeListIndex + 1];
                long operationResultParametar = 0;
                long command = currentOctcode;
                string fullCommand = AddMissingZeros(command);
                bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;
                AddZerosAsElementsInList(operationFirstParametar, operationResultParametar, IsAddressParamInPositionMode);
                Mode firstParamMode = (Mode)Int32.Parse(fullCommand[2].ToString());
                switch (firstParamMode)
                {
                    case (Mode.MODE_0_POSSITION_MODE):
                        operationResultParametar = (int)octcodeList[(int)operationFirstParametar];
                        cordinates.Add((int)octcodeList[(int)operationResultParametar]);
                        break;
                    case (Mode.MODE_1_PARAMETER_MODE):
                        cordinates.Add((int)operationFirstParametar);
                        break;
                    case (Mode.MODE_2_RELATIVE_MODE):
                        long memoryAddress = operationFirstParametar + relativeBaseResult;
                        cordinates.Add((int)octcodeList[(int)memoryAddress]);
                        break;
                    case (Mode.UNKNOWN):
                    default:
                        Console.WriteLine($"Error {currentOctcode}, method Output()");
                        break;
                }
            }
            else
            {
                result = octcodeList[octcodeListIndex + 1];
                cordinates.Add((int)octcodeList[(int)result]);
            }

            octcodeListIndex += 2;
        }

        private static int ReturnOutputValue(long currentOctcode)
        {
            int returnValue =0;
            long result = 0;
            if (currentOctcode > 10)
            {
                long operationFirstParametar = octcodeList[octcodeListIndex + 1];
                long operationResultParametar = 0;
                long command = currentOctcode;
                string fullCommand = AddMissingZeros(command);
                bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;
                AddZerosAsElementsInList(operationFirstParametar, operationResultParametar, IsAddressParamInPositionMode);
                Mode firstParamMode = (Mode)Int32.Parse(fullCommand[2].ToString());
                switch (firstParamMode)
                {
                    case (Mode.MODE_0_POSSITION_MODE):
                        operationResultParametar = (int)octcodeList[(int)operationFirstParametar];
                        returnValue=(int)octcodeList[(int)operationResultParametar];
                        break;
                    case (Mode.MODE_1_PARAMETER_MODE):
                        returnValue = (int)operationFirstParametar;
                        break;
                    case (Mode.MODE_2_RELATIVE_MODE):
                        long memoryAddress = operationFirstParametar + relativeBaseResult;
                        returnValue = (int)octcodeList[(int)memoryAddress];
                        break;
                    case (Mode.UNKNOWN):
                    default:
                        Console.WriteLine($"Error {currentOctcode}, method Output()");
                        break;
                }
            }
            else
            {
                result = octcodeList[octcodeListIndex + 1];
                //Console.WriteLine($"Output: {octcodeList[(int)result]}");
                returnValue = ((int)octcodeList[(int)result]);
            }

            octcodeListIndex += 2;

            return returnValue;
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

                firstValue = Calculate(currentOctcode, operationFirstParametar, fullCommand[2].ToString());
                secoundValue = Calculate(currentOctcode, operationSecondParametar, fullCommand[1].ToString());
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

        private static void JumpIfFalse(long currentOctcode)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            long operationSecondParametar = octcodeList[octcodeListIndex + 2];
            long firstValue = 0;
            long secoundValue = 0;
            if (currentOctcode > 10)
            {
                long command = octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
                firstValue = Calculate(currentOctcode, operationFirstParametar, fullCommand[2].ToString());
                secoundValue = Calculate(currentOctcode, operationSecondParametar, fullCommand[1].ToString());
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

        private static void LessThan(long currentOctcode)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            long operationSecondParametar = octcodeList[octcodeListIndex + 2];
            long operationResultParametar = octcodeList[octcodeListIndex + 3];
            string fullCommand = String.Empty;

            long firstValue = 0;
            long secoundValue = 0;
            if (currentOctcode > 10)
            {
                long command = octcodeList[octcodeListIndex];
                fullCommand = AddMissingZeros(command);
                bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                AddZerosAsElementsInList(operationFirstParametar, operationResultParametar, IsFistParamInPositionMode);

                firstValue = Calculate(currentOctcode, operationFirstParametar, fullCommand[2].ToString());
                secoundValue = Calculate(currentOctcode, operationSecondParametar, fullCommand[1].ToString());
            }
            else
            {
                firstValue = octcodeList[(int)operationFirstParametar];
                secoundValue = octcodeList[(int)operationSecondParametar];
            }

            if (firstValue < secoundValue)
            {
                StoreValue(currentOctcode, operationResultParametar, fullCommand == String.Empty ? "0" : fullCommand[0].ToString(), 1);
            }
            else
            {
                StoreValue(currentOctcode, operationResultParametar, fullCommand == String.Empty ? "0" : fullCommand[0].ToString(), 0);
            }

            octcodeListIndex += 4;
        }

        private static void EqualsOpetation(long currentOctcode)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            long operationSecondParametar = octcodeList[octcodeListIndex + 2];
            long operationResultParametar = octcodeList[octcodeListIndex + 3];
            string fullCommand = String.Empty;

            long firstValue = 0;
            long secoundValue = 0;
            if (currentOctcode > 10)
            {
                long command = octcodeList[octcodeListIndex];
                fullCommand = AddMissingZeros(command);
                bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                AddZerosAsElementsInList(operationFirstParametar, operationResultParametar, IsFistParamInPositionMode);
                firstValue = Calculate(currentOctcode, operationFirstParametar, fullCommand[2].ToString());
                secoundValue = Calculate(currentOctcode, operationSecondParametar, fullCommand[1].ToString());
            }
            else
            {
                firstValue = octcodeList[(int)operationFirstParametar];
                secoundValue = octcodeList[(int)operationSecondParametar];
            }

            if (firstValue == secoundValue)
            {
                StoreValue(currentOctcode, operationResultParametar, fullCommand == String.Empty ? "0" : fullCommand[0].ToString(), 1);
            }
            else
            {
                StoreValue(currentOctcode, operationResultParametar, fullCommand == String.Empty ? "0" : fullCommand[0].ToString(), 0);
            }

            octcodeListIndex += 4;
        }

        private static void RelativeBaseOperations(long currentOctcode)
        {
            long operationFirstParametar = octcodeList[octcodeListIndex + 1];
            string fullCommand = AddMissingZeros(currentOctcode);
            Mode mode = (Mode)Int32.Parse(fullCommand[2].ToString());
            switch (mode)
            {
                case (Mode.MODE_0_POSSITION_MODE):
                    relativeBaseResult += octcodeList[(int)operationFirstParametar];
                    break;
                case (Mode.MODE_1_PARAMETER_MODE):
                    relativeBaseResult += operationFirstParametar;
                    break;
                case (Mode.MODE_2_RELATIVE_MODE):
                    relativeBaseResult += octcodeList[(int)(operationFirstParametar + relativeBaseResult)];
                    break;
                case (Mode.UNKNOWN):
                default:
                    Console.WriteLine($"Error, {currentOctcode}, method RelativeBaseOperations()");
                    break;
            }

            octcodeListIndex += 2;
        }

        private static void Exit()
        {
            Console.SetCursorPosition(0, 32);
            Console.WriteLine("Code halted");
            runProgram = false;
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

        private static long Calculate(long currentOctcode, long operationSecondParametar, string fullCommand)
        {
            Mode secondParamMode = (Mode)Int32.Parse(fullCommand);
            long resultSec = 0;
            switch (secondParamMode)
            {
                case (Mode.MODE_0_POSSITION_MODE):
                    resultSec = octcodeList[(int)operationSecondParametar];
                    break;
                case (Mode.MODE_1_PARAMETER_MODE):
                    resultSec = operationSecondParametar;
                    break;
                case (Mode.MODE_2_RELATIVE_MODE):
                    resultSec = (octcodeList[(int)relativeBaseResult + (int)operationSecondParametar]);
                    break;
                case (Mode.UNKNOWN):
                default:
                    Console.WriteLine($"Error {currentOctcode}, method Calculate()");
                    break;
            }

            return resultSec;
        }

        private static void StoreValue(long currentOctcode, long operationResultParametar, string stringMode, long valueToStore)
        {
            Mode mode = (Mode)Int32.Parse(stringMode);
            switch (mode)
            {
                case (Mode.MODE_0_POSSITION_MODE):
                    octcodeList[(int)operationResultParametar] = valueToStore;
                    break;
                case (Mode.MODE_2_RELATIVE_MODE):
                    octcodeList[(int)(operationResultParametar + relativeBaseResult)] = valueToStore;
                    var ter = (int)(operationResultParametar + relativeBaseResult);
                    break;
                case (Mode.UNKNOWN):
                default:
                    Console.WriteLine($"Error, {currentOctcode}, method StoreValue()");
                    break;
            }
        }

        private static void AddZerosAsElementsInList(long operationFirstParametar, long operationResultParametar, bool IsFistParamInPositionMode)
        {
            int intsCountMinusIndex = octcodeList.Count - 1;
            if (IsFistParamInPositionMode && (operationFirstParametar > octcodeList.Count || operationResultParametar > intsCountMinusIndex))
            {
                int missingZeroParams = (int)operationFirstParametar - intsCountMinusIndex;
                int missingZeroIndexResult = (int)operationResultParametar - intsCountMinusIndex;
                int missingZeroIndex = missingZeroIndexResult > missingZeroParams ? missingZeroIndexResult : missingZeroParams;
                for (int i = 0; i < missingZeroIndex + 10; i++)
                {
                    octcodeList.Add(0);
                }
            }
        }
    }

    enum Operation
    {
        INSTRACTION_1_ADDING = 1,
        INSTRACTION_2_MULTIPLING = 2,
        INSTRACTION_3_WRITE_INPUT = 3,
        INSTRACTION_4_OUTPUT = 4,
        INSTRACTION_5_JUMP_IF_TRUE = 5,
        INSTRACTION_6_JUMP_IF_FALSE = 6,
        INSTRACTION_7_LESS_THAN = 7,
        INSTRACTION_8_EQUALS = 8,
        INSTRACTION_9_RELATIVE_BASE = 9,
        INSTRACTION_99_HALT = 99,
        UNKNOWN = 0
    }

    enum Mode
    {
        MODE_0_POSSITION_MODE = 0,
        MODE_1_PARAMETER_MODE = 1,
        MODE_2_RELATIVE_MODE = 2,
        UNKNOWN = 99
    }
}