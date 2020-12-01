using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_9
{
    class Program
    {
        static bool runProgram = true;
        static List<long> octcodeList;
        static long relativeBaseResult = 0;
        static int octcodeListIndex = 0;
        static List<Panel> panels;
        static int outputIndex = 0;
        static RobotPointigDirections RobotPointigDirections;
        static int panelCounter = 1;

        //your answer is too high, You guessed 3752.
        static void Main(string[] args)
        {
            RestartMachine();
            Console.WriteLine("Position (0,0) is start position");

            Intercode(0);
            Console.WriteLine($"There are {panelCounter} panels");

            RestartMachine();
            Intercode(1);

            var maxX = panels.Select(x => x.CordinateX).Max();
            var maxY = panels.Select(x => x.CordinateY).Max();
            var minY = panels.Select(x => x.CordinateY).Min();


            //image is upside down, will be fix in some cleanup
            for (int y = maxY; maxY > minY; y--) 
            {
                for (int x = 0; x < maxX; x++) 
                {
                    var tempPanel = panels.Where(tempPanel => tempPanel.CordinateX == x && tempPanel.CordinateY == y).FirstOrDefault();
                    if (tempPanel != null)
                    {
                        Console.SetCursorPosition(tempPanel.CordinateX, Math.Abs(tempPanel.CordinateY +10));
                        Console.Write(tempPanel.Color == 1 ? '#' : ' ');
                    }
                }
            }
        }

        private static void RestartMachine()
        {
            string inputFromTxt = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            octcodeList = (Array.ConvertAll(inputFromTxt.Split(','), s => Int64.Parse(s))).ToList();
            panels = new List<Panel>();
            outputIndex = 0;
            relativeBaseResult = 0;
            runProgram = true;
            octcodeListIndex = 0;
            panelCounter = 1;
            Panel startPanel = new Panel() { Color = 0, CordinateX = 0, CordinateY = 0 };
            panels.Add(startPanel);
            RobotPointigDirections = RobotPointigDirections.UP;
        }

        public static void Intercode(long input)
        {
            Operation operation = 0;
            outputIndex = 0;
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
                        outputIndex++;
                        input = OutputValue(currentOctcode);
                        break;
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
                    AddZerosAsElementsInList(operationResultParametar, relativeBaseResult, true);
                }

                long resultFirst = Calculate(currentOctcode, operationFirstParametar, fullCommand[2].ToString());
                long resultSec = Calculate(currentOctcode, operationSecondParametar, fullCommand[1].ToString());
                StoreValue(currentOctcode, operationResultParametar, fullCommand[0].ToString(), resultFirst * resultSec);
            }
            else
            {
                AddZerosAsElementsInList(operationFirstParametar, operationResultParametar, true);
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

        private static long OutputValue(long currentOctcode)
        {
            long newInput = 0;
            long output;
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
                        output = (int)octcodeList[(int)operationResultParametar];
                        //Console.WriteLine($"Output: {output}");
                        newInput = OutputStep(output);
                        break;
                    case (Mode.MODE_1_PARAMETER_MODE):
                        output = operationFirstParametar;
                        //Console.WriteLine($"Output: {output}");
                        newInput = OutputStep(output);
                        break;
                    case (Mode.MODE_2_RELATIVE_MODE):
                        long memoryAddress = operationFirstParametar + relativeBaseResult;
                        output = octcodeList[(int)memoryAddress];
                        //Console.WriteLine($"Output: {output}");
                        newInput = OutputStep(output);
                        break;
                    case (Mode.UNKNOWN):
                    default:
                        Console.WriteLine($"Error {currentOctcode}, method Output()");
                        break;
                }
            }
            else
            {
                output = octcodeList[(int)octcodeList[octcodeListIndex + 1]];
                //Console.WriteLine($"Output: {output}");
                newInput = OutputStep(output);
            }

            octcodeListIndex += 2;
            return newInput;
        }

        private static long OutputStep(long result)
        {
            if (outputIndex % 2 == 1)
            {
                Panel panel = panels.Last();
                panel.Color = result;

                return result;
            }
            else
            {
                return MoveAcordingToOutput(result == 0);
            }
        }

        private static long MoveAcordingToOutput(bool isLeftDirection)
        {
            var tt = panels.Last();
            Panel panel = new Panel();
            if (isLeftDirection)
            {
                switch (RobotPointigDirections)
                {
                    case (RobotPointigDirections.UP):
                        panel.CordinateX = tt.CordinateX - 1;
                        panel.CordinateY = tt.CordinateY;
                        
                        RobotPointigDirections = RobotPointigDirections.LEFT;
                        //Console.WriteLine($"direction LEFT");
                        break;
                    case (RobotPointigDirections.LEFT):
                        panel.CordinateX = tt.CordinateX;
                        panel.CordinateY = tt.CordinateY - 1;

                        RobotPointigDirections = RobotPointigDirections.DOWN;
                        //Console.WriteLine($"direction DOWN");
                        break;
                    case (RobotPointigDirections.DOWN):
                        panel.CordinateX = tt.CordinateX + 1;
                        panel.CordinateY = tt.CordinateY;
                        
                        RobotPointigDirections = RobotPointigDirections.RIGHT;
                        //Console.WriteLine($"direction RIGHT");
                        break;
                    case (RobotPointigDirections.RIGHT):
                        panel.CordinateX = tt.CordinateX;
                        panel.CordinateY = tt.CordinateY + 1;

                        RobotPointigDirections = RobotPointigDirections.UP;
                        //Console.WriteLine($"direction UP");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (RobotPointigDirections)
                {
                    case (RobotPointigDirections.UP):
                        panel.CordinateX = tt.CordinateX + 1;
                        panel.CordinateY = tt.CordinateY;

                        RobotPointigDirections = RobotPointigDirections.RIGHT;
                        //Console.WriteLine($"direction RIGHT");
                        break;
                    case (RobotPointigDirections.LEFT):
                        panel.CordinateX = tt.CordinateX;
                        panel.CordinateY = tt.CordinateY + 1;

                        RobotPointigDirections = RobotPointigDirections.UP;
                        //Console.WriteLine($"direction UP");
                        break;

                    case (RobotPointigDirections.DOWN):
                        panel.CordinateX = tt.CordinateX - 1;
                        panel.CordinateY = tt.CordinateY;

                        RobotPointigDirections = RobotPointigDirections.LEFT;
                        //Console.WriteLine($"direction LEFT");
                        break;

                    case (RobotPointigDirections.RIGHT):
                        panel.CordinateX = tt.CordinateX;
                        panel.CordinateY = tt.CordinateY - 1;

                        RobotPointigDirections = RobotPointigDirections.DOWN;
                        //Console.WriteLine($"direction DOWN");
                        break;
                    default:
                        break;
                }
            }

            if (IsPanelColored(panel))
            {
                var tttt = panels.Where(x => x.CordinateX == panel.CordinateX && x.CordinateY == panel.CordinateY).LastOrDefault().Color;
                panels.Add(panel);
                //Console.WriteLine($"Read color {(tttt == 0 ? "Black" : "White")}");
                return tttt;
            }
            else 
            {
                panelCounter++;
                panels.Add(panel);
                return 0;
            }
        }

        private static bool IsPanelColored(Panel panel)
        {
            return panels.Any(x => x.CordinateX == panel.CordinateX && x.CordinateY == panel.CordinateY);
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
                    resultSec = (octcodeList[(int)relativeBaseResult +(int)operationSecondParametar]);
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
                for (int i = 0; i < missingZeroIndex + 10000; i++)
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

    enum RobotPointigDirections 
    {
        UP = 0,
        LEFT = 1,
        DOWN = 2,
        RIGHT = 3
    }

    class Panel
    {
        public int CordinateX { get; set; }
        public int CordinateY { get; set; }
        public long  Color { get; set; }

        public override string ToString()
        {
            return $"({CordinateX},{CordinateY}), Color {(Color== 0 ? "black" : "white")}";
        }
    }
}