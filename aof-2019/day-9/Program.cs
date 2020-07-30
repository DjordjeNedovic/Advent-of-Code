using System;
using System.Collections.Generic;
using System.IO;

namespace day_9
{
    class Program
    {
        static bool runProgram = true;
        //static List<long> octcodeList = new List<long>() { 1101, 100, -1, 4, 0 };
        static List<long> octcodeList = new List<long>() { 3,225,1,225,6,6,1100,1,238,225,104,0,1101,90,64,225,1101,15,56,225,1,14,153,224,101,-147,224,224,4,224,1002,223,8,223,1001,224,3,224,1,224,223,223,2,162,188,224,101,-2014,224,224,4,224,1002,223,8,223,101,6,224,224,1,223,224,223,1001,18,81,224,1001,224,-137,224,4,224,1002,223,8,223,1001,224,3,224,1,223,224,223,1102,16,16,224,101,-256,224,224,4,224,1002,223,8,223,1001,224,6,224,1,223,224,223,101,48,217,224,1001,224,-125,224,4,224,1002,223,8,223,1001,224,3,224,1,224,223,223,1002,158,22,224,1001,224,-1540,224,4,224,1002,223,8,223,101,2,224,224,1,223,224,223,1101,83,31,225,1101,56,70,225,1101,13,38,225,102,36,192,224,1001,224,-3312,224,4,224,1002,223,8,223,1001,224,4,224,1,224,223,223,1102,75,53,225,1101,14,92,225,1101,7,66,224,101,-73,224,224,4,224,102,8,223,223,101,3,224,224,1,224,223,223,1101,77,60,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,7,226,677,224,1002,223,2,223,1005,224,329,1001,223,1,223,1007,226,677,224,1002,223,2,223,1005,224,344,101,1,223,223,108,226,226,224,1002,223,2,223,1006,224,359,101,1,223,223,7,226,226,224,102,2,223,223,1005,224,374,101,1,223,223,8,677,677,224,1002,223,2,223,1005,224,389,1001,223,1,223,107,677,677,224,102,2,223,223,1006,224,404,101,1,223,223,1107,677,226,224,102,2,223,223,1006,224,419,1001,223,1,223,1008,226,226,224,1002,223,2,223,1005,224,434,1001,223,1,223,7,677,226,224,102,2,223,223,1006,224,449,1001,223,1,223,1107,226,226,224,1002,223,2,223,1005,224,464,101,1,223,223,1108,226,677,224,102,2,223,223,1005,224,479,101,1,223,223,1007,677,677,224,102,2,223,223,1006,224,494,1001,223,1,223,1107,226,677,224,1002,223,2,223,1005,224,509,101,1,223,223,1007,226,226,224,1002,223,2,223,1006,224,524,101,1,223,223,107,226,226,224,1002,223,2,223,1005,224,539,1001,223,1,223,1108,677,677,224,1002,223,2,223,1005,224,554,101,1,223,223,1008,677,226,224,102,2,223,223,1006,224,569,1001,223,1,223,8,226,677,224,102,2,223,223,1005,224,584,1001,223,1,223,1008,677,677,224,1002,223,2,223,1006,224,599,1001,223,1,223,108,677,677,224,102,2,223,223,1006,224,614,1001,223,1,223,108,226,677,224,102,2,223,223,1005,224,629,101,1,223,223,8,677,226,224,102,2,223,223,1005,224,644,101,1,223,223,107,677,226,224,1002,223,2,223,1005,224,659,101,1,223,223,1108,677,226,224,102,2,223,223,1005,224,674,1001,223,1,223,4,223,99,226};
        static long relativeBaseResult = 0;
        static int octcodeListIndex = 0;


        //1187721666102244 not!
        static void Main(string[] args)
        {
            //string inputFromTxt = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            //int[] puzzleInput = Array.ConvertAll(inputFromTxt.Split(','), s => Int32.Parse(s));
            //Console.WriteLine("Enter input: ");
            //string keyInput = Console.ReadLine();
            //long input = Int64.Parse(keyInput);
            long input = 5;
            Intercode(input);
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
                        Adding(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_2_MULTIPLING):
                        Multiplaing(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_3_WRITE_INPUT):
                        WriteInput(currentOctcode, input);
                        break;
                    case (Operation.INSTRACTION_4_OUTPUT):
                        OutputValue(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_5_JUMP_IF_TRUE):
                        JumpIfTrue(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_6_JUMP_IF_FALSE):
                        JumpIfFalse(currentOctcode);
                        break;
                    case (Operation.INSTRACTION_7_LESS_THAN):
                        LessThan(currentOctcode);
                        octcodeListIndex += 4;
                        break;
                    case (Operation.INSTRACTION_8_EQUALS):
                        EqualsOpetation(currentOctcode);
                        octcodeListIndex += 4;
                        break;
                    case (Operation.INSTRACTION_99_HALT):
                        Exit();
                        break;
                    case (Operation.UNKNOWN):
                    default:
                        Console.WriteLine($"Error: Unknown OppCode {currentOctcode}, line 111");
                        runProgram = false;
                        break;
                }
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
        
        private static void Multiplaing(long currentOctcode)
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

        private static void WriteInput(long currentOctcode, long input)
        {
            long result = 0;
            if (currentOctcode > 10)
            {
            }
            else
            {
                result = octcodeList[octcodeListIndex + 1];
                octcodeList[(int)result] = input;
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
                long resultFirst = 0;
                switch (firstParamMode)
                {
                    case (Mode.MODE_0_POSSITION_MODE):
                        operationResultParametar = (int)octcodeList[(int)operationFirstParametar];
                        Console.WriteLine($"Output: {(int)octcodeList[(int)operationResultParametar]}");
                        break;
                    case (Mode.MODE_1_PARAMETER_MODE):
                        Console.WriteLine($"Output: {operationFirstParametar}");
                        break;
                    case (Mode.UNKNOWN):
                    default:
                        Console.WriteLine($"Error {currentOctcode}, line 530");
                        break;
                }

            }
            else 
            {
                result = octcodeList[octcodeListIndex + 1];
                Console.WriteLine($"Output: {octcodeList[(int)result]}");
            }

            octcodeListIndex += 2;
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

            long firstValue = 0;
            long secoundValue = 0;
            if (currentOctcode > 10)
            {
                long command = octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
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
                octcodeList[(int)operationResultParametar] = 1;
            }
            else
            {
                octcodeList[(int)operationResultParametar] = 0;
            }
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
                octcodeList[(int)operationResultParametar] = 1;
            }
            else
            {
                octcodeList[(int)operationResultParametar] = 0;
            }
        }

        private static void Exit()
        {
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
                case (Mode.UNKNOWN):
                default:
                    Console.WriteLine($"Error {currentOctcode}, line 509");
                    break;
            }

            return resultSec;
        }

        private static void AddZerosAsElementsInList(long operationFirstParametar, long operationResultParametar, bool IsFistParamInPositionMode)
        {
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
        //INSTRACTION_9_RELATIVE_BASE = 9,
        INSTRACTION_99_HALT = 99,
        UNKNOWN = 0
    }

    enum Mode
    {
        MODE_0_POSSITION_MODE = 0,
        MODE_1_PARAMETER_MODE = 1,
        //MODE_2_RELATIVE_MODE = 2,
        UNKNOWN = 99
    }
}