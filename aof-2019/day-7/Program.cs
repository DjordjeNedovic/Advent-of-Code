using System;
using System.Collections.Generic;
using System.IO;

namespace day_7
{
    class Program
    {
        public static List<string> combinations { get; private set; }
        private static bool isAmpEHalted = false;

        static void Main(string[] args)
        {
            Console.WriteLine("################### PART ONE ###################");
            SolvePartOne();
            Console.WriteLine("################### PART TWO ###################");
            SolvePartTwo();
        }

        private static void SolvePartOne()
        {
            string permuteCompination = "01234";
            List<string> combinations = Permute(permuteCompination, 0, permuteCompination.Length - 1, new List<string>());
            int result = 0;
            foreach (string combination in combinations)
            {
                var ampA = RunAmplifierPartOne(Int32.Parse(combination[0].ToString()), 0);
                var ampB = RunAmplifierPartOne(Int32.Parse(combination[1].ToString()), ampA);
                var ampC = RunAmplifierPartOne(Int32.Parse(combination[2].ToString()), ampB);
                var ampD = RunAmplifierPartOne(Int32.Parse(combination[3].ToString()), ampC);
                var ampE = RunAmplifierPartOne(Int32.Parse(combination[4].ToString()), ampD);

                if (ampE > result)
                {
                    result = ampE;
                }
            }

            Console.WriteLine($"The highest signal that can be sent to the thrusters is: {result}");
        }

        private static void SolvePartTwo()
        {
            string str = "56789";
            List<string> combinations = Permute(str, 0, str.Length - 1, new List<string>());
            int result = 0;
            foreach (string combination in combinations)
            {
                List<int> inputsApmA = new List<int> { Int32.Parse(combination[0].ToString()), 0 };
                List<int> inputsApmB = new List<int> { Int32.Parse(combination[1].ToString()) };
                List<int> inputsApmC = new List<int> { Int32.Parse(combination[2].ToString()) };
                List<int> inputsApmD = new List<int> { Int32.Parse(combination[3].ToString()) };
                List<int> inputsApmE = new List<int> { Int32.Parse(combination[4].ToString()) };
                isAmpEHalted = false;
                while (true)
                {
                    var ampA = RunAmplifierPartTwo(inputsApmA, "ampA");
                    inputsApmB.Add(ampA);
                    var ampB = RunAmplifierPartTwo(inputsApmB, "ampB");
                    inputsApmC.Add(ampB);
                    var ampC = RunAmplifierPartTwo(inputsApmC, "ampC");
                    inputsApmD.Add(ampC);
                    var ampD = RunAmplifierPartTwo(inputsApmD, "ampD");
                    inputsApmE.Add(ampD);
                    var ampE = RunAmplifierPartTwo(inputsApmE, "ampE");
                    inputsApmA.Add(ampE);
                    if (isAmpEHalted)
                    {
                        if (ampE > result)
                        {
                            result = ampE;
                        }

                        break;
                    }
                }
            }

            Console.WriteLine($"Diagnostic code for system ID 5 is: {result}");
        }

        //maintability index for this method is 23, refactoring will be done... one day
        private static int RunAmplifierPartOne(int firstInput, int secondInput)
        {
            int solution = 0;
            int i = 0;
            int result = 0;
            int[] inputs = new int[] { firstInput, secondInput };
            string inputFromTxt = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            int[] puzzleInput = Array.ConvertAll(inputFromTxt.Split(','), s => Int32.Parse(s));
            int opcodeIndex = 0;
            while (true)
            {
                if (opcodeIndex > puzzleInput.Length)
                {
                    break;
                }
                if (puzzleInput[opcodeIndex] == 99)
                {
                    break;
                }

                int opcode = puzzleInput[opcodeIndex] % 10;
                int firstParam = puzzleInput[opcodeIndex + 1];
                int secondParam = puzzleInput[opcodeIndex + 2];
                if (opcode == 1 || opcode == 2)
                {
                    result = puzzleInput[opcodeIndex + 3];
                    if (puzzleInput[opcodeIndex] > 10)
                    {
                        int command = puzzleInput[opcodeIndex];
                        string fullCommand = AddMissingZeros(command);
                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        int firstValue = IsFistParamInPositionMode ? puzzleInput[firstParam] : firstParam;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secondParam] : secondParam;
                        if (opcode == 1)
                        {
                            puzzleInput[result] = firstValue + secoundValue;
                        }
                        else if (opcode == 2)
                        {
                            puzzleInput[result] = firstValue * secoundValue;
                        }
                    }
                    else
                    {
                        int readValueOfFirstParam = puzzleInput[firstParam];
                        int readValueOfSecondParam = puzzleInput[secondParam];
                        if (opcode == 1)
                        {
                            puzzleInput[result] = readValueOfFirstParam + readValueOfSecondParam;
                        }
                        else if (opcode == 2)
                        {
                            puzzleInput[result] = readValueOfFirstParam * readValueOfSecondParam;
                        }
                    }

                    opcodeIndex += 4;
                }
                else if (opcode == 3)
                {
                    result = puzzleInput[opcodeIndex + 1];
                    opcodeIndex += 2;
                    puzzleInput[result] = inputs[i];

                    i++;
                }
                else if (opcode == 4)
                {
                    if (puzzleInput[opcodeIndex] > 10)
                    {
                        int command = puzzleInput[opcodeIndex];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        if (IsFistParamInPositionMode)
                        {
                            result = puzzleInput[firstParam];
                            solution = puzzleInput[result];
                        }
                        else
                        {
                            solution = firstParam;
                        }
                    }
                    else
                    {
                        result = puzzleInput[opcodeIndex + 1];
                        solution = puzzleInput[result];
                    }

                    opcodeIndex += 2;
                }
                else if (opcode == 5 || opcode == 6)
                {
                    if (puzzleInput[opcodeIndex] > 10)
                    {
                        int command = puzzleInput[opcodeIndex];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;

                        int firstValue = IsFistParamInPositionMode ? puzzleInput[firstParam] : firstParam;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secondParam] : secondParam;
                        if (opcode == 5)
                        {
                            if (firstValue != 0)
                            {
                                opcodeIndex = secoundValue;
                                continue;
                            }

                            opcodeIndex += 3;
                        }
                        if (opcode == 6)
                        {
                            if (firstValue == 0)
                            {
                                opcodeIndex = secoundValue;

                                continue;
                            }

                            opcodeIndex += 3;
                        }
                    }
                    else
                    {
                        if (opcode == 5)
                        {
                            if (puzzleInput[firstParam] != 0)
                            {
                                opcodeIndex = puzzleInput[secondParam];
                                continue;
                            }

                            opcodeIndex += 3;
                        }
                        if (opcode == 6)
                        {
                            if (puzzleInput[firstParam] == 0)
                            {
                                opcodeIndex = puzzleInput[secondParam];
                                continue;
                            }

                            opcodeIndex += 3;
                        }
                    }
                }
                else if (opcode == 7 || opcode == 8)
                {
                    result = puzzleInput[opcodeIndex + 3];
                    if (puzzleInput[opcodeIndex] > 10)
                    {
                        int command = puzzleInput[opcodeIndex];
                        string fullCommand = AddMissingZeros(command);
                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        int firstValue = IsFistParamInPositionMode ? puzzleInput[firstParam] : firstParam;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secondParam] : secondParam;
                        if (opcode == 7)
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
                        if (opcode == 8)
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
                        if (opcode == 7)
                        {
                            if (puzzleInput[firstParam] < puzzleInput[secondParam])
                            {
                                puzzleInput[result] = 1;
                            }
                            else
                            {
                                puzzleInput[result] = 0;
                            }
                        }
                        if (opcode == 8)
                        {
                            if (puzzleInput[firstParam] == puzzleInput[secondParam])
                            {
                                puzzleInput[result] = 1;
                            }
                            else
                            {
                                puzzleInput[result] = 0;
                            }
                        }
                    }

                    opcodeIndex += 4;
                }
                else
                {
                    break;
                }
            }

            return solution;
        }

        //maintability index for this method is 23, refactoring will be done... one day
        private static int RunAmplifierPartTwo(List<int> inputs, string ampName)
        {
            int solution = 0;
            int i = 0;
            int result = 0;
            string inputFromTxt = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            int[] puzzleInput = Array.ConvertAll(inputFromTxt.Split(','), s => Int32.Parse(s));
            int opcodeIndex = 0;
            while (true)
            {
                if (opcodeIndex > puzzleInput.Length)
                {
                    break;
                }
                if (puzzleInput[opcodeIndex] == 99)
                {
                    if (ampName.Equals("ampE"))
                    {
                        isAmpEHalted = true;
                    }

                    break;
                }

                int opcode = puzzleInput[opcodeIndex] % 10;
                int firstParam = puzzleInput[opcodeIndex + 1];
                int secondParam = puzzleInput[opcodeIndex + 2];
                if (opcode == 1 || opcode == 2)
                {
                    result = puzzleInput[opcodeIndex + 3];
                    if (puzzleInput[opcodeIndex] > 10)
                    {
                        int command = puzzleInput[opcodeIndex];
                        string fullCommand = AddMissingZeros(command);
                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        int firstValue = IsFistParamInPositionMode ? puzzleInput[firstParam] : firstParam;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secondParam] : secondParam;
                        if (opcode == 1)
                        {
                            puzzleInput[result] = firstValue + secoundValue;
                        }
                        else if (opcode == 2)
                        {
                            puzzleInput[result] = firstValue * secoundValue;
                        }
                    }
                    else
                    {
                        int readValueOfFirstParam = puzzleInput[firstParam];
                        int readValeuOfSecondParam = puzzleInput[secondParam];
                        if (opcode == 1)
                        {
                            puzzleInput[result] = readValueOfFirstParam + readValeuOfSecondParam;
                        }
                        else if (opcode == 2)
                        {
                            puzzleInput[result] = readValueOfFirstParam * readValeuOfSecondParam;
                        }
                    }

                    opcodeIndex += 4;
                }
                else if (opcode == 3)
                {
                    if (inputs.Count > i)
                    {
                        result = puzzleInput[opcodeIndex + 1];
                        opcodeIndex += 2;
                        puzzleInput[result] = inputs[i];
                        i++;
                    }
                    else
                    {
                        return solution;
                    }

                }
                else if (opcode == 4)
                {
                    if (puzzleInput[opcodeIndex] > 10)
                    {
                        int command = puzzleInput[opcodeIndex];
                        string fullCommand = AddMissingZeros(command);
                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        if (IsFistParamInPositionMode)
                        {
                            result = puzzleInput[firstParam];
                            solution = puzzleInput[result];
                        }
                        else
                        {
                            solution = firstParam;
                        }
                    }
                    else
                    {
                        result = puzzleInput[opcodeIndex + 1];
                        solution = puzzleInput[result];
                    }

                    opcodeIndex += 2;
                }
                else if (opcode == 5 || opcode == 6)
                {
                    if (puzzleInput[opcodeIndex] > 10)
                    {
                        int command = puzzleInput[opcodeIndex];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;

                        int firstValue = IsFistParamInPositionMode ? puzzleInput[firstParam] : firstParam;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secondParam] : secondParam;
                        if (opcode == 5)
                        {
                            if (firstValue != 0)
                            {
                                opcodeIndex = secoundValue;
                                continue;
                            }

                            opcodeIndex += 3;
                        }
                        if (opcode == 6)
                        {
                            if (firstValue == 0)
                            {
                                opcodeIndex = secoundValue;
                                continue;
                            }

                            opcodeIndex += 3;
                        }
                    }
                    else
                    {
                        if (opcode == 5)
                        {
                            if (puzzleInput[firstParam] != 0)
                            {
                                opcodeIndex = puzzleInput[secondParam];
                                continue;
                            }

                            opcodeIndex += 3;
                        }
                        if (opcode == 6)
                        {
                            if (puzzleInput[firstParam] == 0)
                            {
                                opcodeIndex = puzzleInput[secondParam];
                                continue;
                            }

                            opcodeIndex += 3;
                        }
                    }
                }
                else if (opcode == 7 || opcode == 8)
                {
                    result = puzzleInput[opcodeIndex + 3];
                    if (puzzleInput[opcodeIndex] > 10)
                    {
                        int command = puzzleInput[opcodeIndex];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;

                        int firstValue = IsFistParamInPositionMode ? puzzleInput[firstParam] : firstParam;
                        int secoundValue = IsSecoundParamInPositionMode ? puzzleInput[secondParam] : secondParam;

                        if (opcode == 7)
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
                        if (opcode == 8)
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
                        if (opcode == 7)
                        {
                            if (puzzleInput[firstParam] < puzzleInput[secondParam])
                            {
                                puzzleInput[result] = 1;
                            }
                            else
                            {
                                puzzleInput[result] = 0;
                            }
                        }
                        if (opcode == 8)
                        {
                            if (puzzleInput[firstParam] == puzzleInput[secondParam])
                            {
                                puzzleInput[result] = 1;
                            }
                            else
                            {
                                puzzleInput[result] = 0;
                            }
                        }
                    }

                    opcodeIndex += 4;
                }
                else
                {
                    break;
                }
            }

            return solution;
        }
        
        private static string AddMissingZeros(int command)
        {
            string commandToString = command.ToString();
            while (commandToString.Length != 5)
            {
                commandToString = "0" + commandToString;
            }

            return commandToString;
        }

        private static List<string> Permute(string stringToPemute, int currentIndex, int stringSize, List<string> combinations)
        {
            if (currentIndex == stringSize)
            {
                combinations.Add(stringToPemute);
            }
            else
            {
                for (int i = currentIndex; i <= stringSize; i++)
                {
                    stringToPemute = Swap(stringToPemute, currentIndex, i);
                    Permute(stringToPemute, currentIndex + 1, stringSize, combinations);
                    stringToPemute = Swap(stringToPemute, currentIndex, i);
                }
            }

            return combinations;
        }

        public static string Swap(string stringToPermute, int indexFrom, int indexTo)
        {
            char temp;
            char[] charArray = stringToPermute.ToCharArray();
            temp = charArray[indexFrom];
            charArray[indexFrom] = charArray[indexTo];
            charArray[indexTo] = temp;
            return new string(charArray);
        }
    }
}