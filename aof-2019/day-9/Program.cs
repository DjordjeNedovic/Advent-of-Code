using System;
using System.Collections.Generic;
using System.IO;

namespace day_9
{
    class Program
    {
        static bool runProgram = true;
        //static List<long> octcodeList = new List<long>() { 1102,34463338,34463338,63,1007,63,34463338,63,1005,63,53,1101,3,0,1000,109,988,209,12,9,1000,209,6,209,3,203,0,1008,1000,1,63,1005,63,65,1008,1000,2,63,1005,63,904,1008,1000,0,63,1005,63,58,4,25,104,0,99,4,0,104,0,99,4,17,104,0,99,0,0,1102,1,37,1000,1101,856,0,1029,1101,286,0,1025,1101,39,0,1004,1101,861,0,1028,1101,845,0,1026,1102,28,1,1002,1102,1,0,1020,1101,0,892,1023,1101,0,291,1024,1101,35,0,1018,1101,0,27,1006,1102,1,26,1011,1101,33,0,1019,1102,31,1,1014,1102,1,36,1010,1102,23,1,1007,1101,0,32,1016,1101,29,0,1008,1101,20,0,1001,1102,1,25,1015,1101,38,0,1017,1101,0,24,1012,1102,1,22,1005,1101,1,0,1021,1101,0,21,1003,1102,1,838,1027,1102,1,30,1013,1101,895,0,1022,1101,0,34,1009,109,7,1208,0,22,63,1005,63,201,1001,64,1,64,1105,1,203,4,187,1002,64,2,64,109,-6,2102,1,5,63,1008,63,24,63,1005,63,223,1105,1,229,4,209,1001,64,1,64,1002,64,2,64,109,17,21102,40,1,-6,1008,1012,40,63,1005,63,255,4,235,1001,64,1,64,1106,0,255,1002,64,2,64,109,-15,21108,41,41,9,1005,1012,277,4,261,1001,64,1,64,1106,0,277,1002,64,2,64,109,11,2105,1,10,4,283,1105,1,295,1001,64,1,64,1002,64,2,64,109,-9,21101,42,0,8,1008,1013,44,63,1005,63,315,1105,1,321,4,301,1001,64,1,64,1002,64,2,64,109,13,1206,3,337,1001,64,1,64,1106,0,339,4,327,1002,64,2,64,109,-10,1208,0,29,63,1005,63,361,4,345,1001,64,1,64,1106,0,361,1002,64,2,64,109,2,2108,27,-4,63,1005,63,383,4,367,1001,64,1,64,1105,1,383,1002,64,2,64,109,-4,1207,2,30,63,1005,63,405,4,389,1001,64,1,64,1105,1,405,1002,64,2,64,109,22,1205,-8,417,1106,0,423,4,411,1001,64,1,64,1002,64,2,64,109,-27,2108,19,0,63,1005,63,443,1001,64,1,64,1106,0,445,4,429,1002,64,2,64,109,13,21108,43,45,-1,1005,1013,461,1106,0,467,4,451,1001,64,1,64,1002,64,2,64,109,1,21107,44,45,4,1005,1019,485,4,473,1105,1,489,1001,64,1,64,1002,64,2,64,109,-8,2102,1,-7,63,1008,63,37,63,1005,63,515,4,495,1001,64,1,64,1106,0,515,1002,64,2,64,109,1,2107,38,-4,63,1005,63,533,4,521,1105,1,537,1001,64,1,64,1002,64,2,64,109,4,21107,45,44,1,1005,1013,553,1106,0,559,4,543,1001,64,1,64,1002,64,2,64,109,-7,2107,21,-4,63,1005,63,575,1106,0,581,4,565,1001,64,1,64,1002,64,2,64,109,9,1205,7,599,4,587,1001,64,1,64,1105,1,599,1002,64,2,64,109,-11,2101,0,-3,63,1008,63,40,63,1005,63,619,1105,1,625,4,605,1001,64,1,64,1002,64,2,64,109,1,2101,0,-2,63,1008,63,28,63,1005,63,651,4,631,1001,64,1,64,1106,0,651,1002,64,2,64,109,1,21102,46,1,7,1008,1012,44,63,1005,63,671,1106,0,677,4,657,1001,64,1,64,1002,64,2,64,109,4,1201,-7,0,63,1008,63,28,63,1005,63,699,4,683,1105,1,703,1001,64,1,64,1002,64,2,64,109,-6,1207,-3,36,63,1005,63,719,1105,1,725,4,709,1001,64,1,64,1002,64,2,64,109,-4,1201,6,0,63,1008,63,23,63,1005,63,745,1106,0,751,4,731,1001,64,1,64,1002,64,2,64,109,8,1202,-6,1,63,1008,63,20,63,1005,63,777,4,757,1001,64,1,64,1105,1,777,1002,64,2,64,109,5,1202,-5,1,63,1008,63,25,63,1005,63,801,1001,64,1,64,1105,1,803,4,783,1002,64,2,64,109,8,21101,47,0,-6,1008,1014,47,63,1005,63,829,4,809,1001,64,1,64,1106,0,829,1002,64,2,64,109,1,2106,0,6,1001,64,1,64,1106,0,847,4,835,1002,64,2,64,109,11,2106,0,-4,4,853,1105,1,865,1001,64,1,64,1002,64,2,64,109,-15,1206,3,883,4,871,1001,64,1,64,1106,0,883,1002,64,2,64,109,14,2105,1,-8,1105,1,901,4,889,1001,64,1,64,4,64,99,21102,1,27,1,21102,1,915,0,1106,0,922,21201,1,57564,1,204,1,99,109,3,1207,-2,3,63,1005,63,964,21201,-2,-1,1,21102,1,942,0,1105,1,922,22101,0,1,-1,21201,-2,-3,1,21101,957,0,0,1105,1,922,22201,1,-1,-2,1106,0,968,21202,-2,1,-2,109,-3,2106,0,0};
        static List<long> octcodeList = new List<long>() { 109, 1, 203, 2, 204, 2, 99 };
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
            long input = 1;
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
                    case (Operation.INSTRACTION_9_RELATIVE_BASE):
                        RelativeBaseOperations(currentOctcode);
                        octcodeListIndex += 2;
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
                int command = (int)octcodeList[octcodeListIndex];
                string fullCommand = AddMissingZeros(command);
                Mode firstParamerMode = (Mode)Int32.Parse(fullCommand[2].ToString());
                switch (firstParamerMode)
                {
                    case (Mode.MODE_0_POSSITION_MODE):
                        result = octcodeList[octcodeListIndex + 1];
                        octcodeList[(int)result] = input;
                        break;
                    case (Mode.MODE_2_RELATIVE_MODE):
                        result = octcodeListIndex + relativeBaseResult;
                        octcodeList[(int)result] = input;
                        break;
                    case (Mode.UNKNOWN):
                    default:
                        Console.WriteLine($"Error {currentOctcode}, line 165");
                        break;
                }
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
                switch (firstParamMode)
                {
                    case (Mode.MODE_0_POSSITION_MODE):
                        operationResultParametar = (int)octcodeList[(int)operationFirstParametar];
                        Console.WriteLine($"Output: {(int)octcodeList[(int)operationResultParametar]}");
                        break;
                    case (Mode.MODE_1_PARAMETER_MODE):
                        Console.WriteLine($"Output: {operationFirstParametar}");
                        break;
                    case (Mode.MODE_2_RELATIVE_MODE):
                        int memoryAddress = (int)(relativeBaseResult + operationFirstParametar);
                        Console.WriteLine($"Output: {octcodeList[memoryAddress]}");
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
                case (Mode.MODE_2_RELATIVE_MODE):
                    resultSec = (octcodeList[((int)relativeBaseResult + octcodeListIndex)]);
                    break;
                case (Mode.UNKNOWN):
                default:
                    Console.WriteLine($"Error {currentOctcode}, method Calculate");
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