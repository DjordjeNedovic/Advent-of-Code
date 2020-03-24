using System;
using System.Collections.Generic;

namespace day_7
{
    class Program
    {
        public static List<string> combinations { get; private set; }
        private static bool isAmpEHalted = false;

        static void Main(string[] args)
        {
            SolvePartOne();
            SolvePartTwo();
        }

        private static void SolvePartOne()
        {
            combinations = new List<string>();
            String str = "01234";
            int n = str.Length;
            permute(str, 0, n - 1);
            int result = 0;
            string t = "";
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
                    t = combination;
                }
            }

            Console.WriteLine($"Output is: {result}, {t}");
        }

        private static void SolvePartTwo() 
        {
            combinations = new List<string>();
            String str = "56789";
            int n = str.Length;
            permute(str, 0, n - 1);
            int result = 0;
            string t = "";
            foreach (string combination in combinations)
            {
                Console.WriteLine($"combination {combination}");
                List<int> inputsApmA = new List<int>{ Int32.Parse(combination[0].ToString()), 0 };
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
                    if (isAmpEHalted )
                    {
                        if (ampE > result)
                        {
                            result = ampE;
                            t = combination; 
                        }

                        break;
                    } 
                }
            }

            Console.WriteLine($"Output is: {result}, {t}");
        }

        private static int RunAmplifierPartTwo(List<int> inputs, string ampName)
        {
            int solution = 0;
            int i = 0;
            int result = 0;
            int[] ints = new int[] { 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5};
            int oct = 0;
            while (true)
            {
                if (oct > ints.Length)
                {
                    Console.WriteLine("shit happend");
                    break;
                }
                if (ints[oct] == 99)
                {
                    if (ampName.Equals("ampE")) 
                    {
                        isAmpEHalted = true;
                    }

                    break;
                }

                int operant = ints[oct] % 10;
                int first = ints[oct + 1];
                int secound = ints[oct + 2];
                if (operant == 1 || operant == 2)
                {
                    result = ints[oct + 3];
                    if (ints[oct] > 10)
                    {
                        int command = ints[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        int firstValue = IsFistParamInPositionMode ? ints[first] : first;
                        int secoundValue = IsSecoundParamInPositionMode ? ints[secound] : secound;

                        if (operant == 1)
                        {
                            ints[result] = firstValue + secoundValue;
                        }
                        else if (operant == 2)
                        {
                            ints[result] = firstValue * secoundValue;
                        }
                    }
                    else
                    {
                        int resultFirs = ints[first];
                        int resultSec = ints[secound];
                        if (operant == 1)
                        {
                            ints[result] = resultFirs + resultSec;
                        }
                        else if (operant == 2)
                        {
                            ints[result] = resultFirs * resultSec;
                        }
                    }

                    oct += 4;
                }
                else if (operant == 3)
                {
                    if (inputs.Count>i)
                    {
                        result = ints[oct + 1];
                        oct += 2;
                        ints[result] = inputs[i];

                        i++;
                    }
                    else 
                    {
                        //Console.WriteLine($"apm {ampName} needs next input, solution {solution} is pushed");
                        return solution;
                    }

                }
                else if (operant == 4)
                {
                    if (ints[oct] > 10)
                    {
                        int command = ints[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        if (IsFistParamInPositionMode)
                        {
                            result = ints[first];
                            solution = ints[result];
                        }
                        else
                        {
                            solution = first;
                        }
                    }
                    else
                    {
                        result = ints[oct + 1];
                        solution = ints[result];
                    }

                    oct += 2;
                }
                else if (operant == 5 || operant == 6)
                {
                    if (ints[oct] > 10)
                    {
                        int command = ints[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;

                        int firstValue = IsFistParamInPositionMode ? ints[first] : first;
                        int secoundValue = IsSecoundParamInPositionMode ? ints[secound] : secound;

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
                            if (ints[first] != 0)
                            {
                                oct = ints[secound];
                                continue;
                            }
                            oct += 3;
                        }
                        if (operant == 6)
                        {
                            if (ints[first] == 0)
                            {
                                oct = ints[secound];
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
                        int command = ints[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;

                        int firstValue = IsFistParamInPositionMode ? ints[first] : first;
                        int secoundValue = IsSecoundParamInPositionMode ? ints[secound] : secound;

                        if (operant == 7)
                        {
                            if (firstValue < secoundValue)
                            {
                                ints[result] = 1;
                            }
                            else
                            {
                                ints[result] = 0;
                            }
                        }
                        if (operant == 8)
                        {
                            if (firstValue == secoundValue)
                            {
                                ints[result] = 1;
                            }
                            else
                            {
                                ints[result] = 0;
                            }
                        }
                    }
                    else
                    {
                        if (operant == 7)
                        {
                            if (ints[first] < ints[secound])
                            {
                                ints[result] = 1;
                            }
                            else
                            {
                                ints[result] = 0;
                            }
                        }
                        if (operant == 8)
                        {
                            if (ints[first] == ints[secound])
                            {
                                ints[result] = 1;
                            }
                            else
                            {
                                ints[result] = 0;
                            }
                        }
                    }

                    oct += 4;
                }
                else
                {
                    //Console.WriteLine("BUM");
                    break;
                }
            }

            return solution;
        }

        private static int RunAmplifierPartOne(int firstInput, int secondInput)
        {
            int solution = 0;
            int i = 0;
            int result = 0;
            int[] inputs = new int[] { firstInput, secondInput };
            int[] ints = new int[] { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 42, 67, 84, 109, 126, 207, 288, 369, 450, 99999, 3, 9, 102, 4, 9, 9, 1001, 9, 4, 9, 102, 2, 9, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 5, 9, 1002, 9, 5, 9, 1001, 9, 5, 9, 1002, 9, 5, 9, 101, 5, 9, 9, 4, 9, 99, 3, 9, 101, 5, 9, 9, 1002, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 102, 4, 9, 9, 101, 2, 9, 9, 102, 4, 9, 9, 1001, 9, 2, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 101, 5, 9, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 99 };
            int oct = 0;
                while (true)
                {
                    if (oct > ints.Length)
                    {
                        break;
                    }
                    if (ints[oct] == 99)
                    {
                        break;
                    }

                    int operant = ints[oct] % 10;
                    int first = ints[oct + 1];
                    int secound = ints[oct + 2];

                    if (operant == 1 || operant == 2)
                    {
                        result = ints[oct + 3];
                        if (ints[oct] > 10)
                        {
                            int command = ints[oct];
                            string fullCommand = AddMissingZeros(command);

                            bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                            bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                            int firstValue = IsFistParamInPositionMode ? ints[first] : first;
                            int secoundValue = IsSecoundParamInPositionMode ? ints[secound] : secound;

                            if (operant == 1)
                            {
                                ints[result] = firstValue + secoundValue;
                            }
                            else if (operant == 2)
                            {
                                ints[result] = firstValue * secoundValue;
                            }
                        }
                        else
                        {
                            int resultFirs = ints[first];
                            int resultSec = ints[secound];
                            if (operant == 1)
                            {
                                ints[result] = resultFirs + resultSec;
                            }
                            else if (operant == 2)
                            {
                                ints[result] = resultFirs * resultSec;
                            }
                        }

                        oct += 4;
                    }
                    else if (operant == 3)
                    {
                        result = ints[oct + 1];
                        oct += 2;
                        ints[result] = inputs[i];

                        i++;
                    }
                    else if (operant == 4)
                    {
                        if (ints[oct] > 10)
                        {
                            int command = ints[oct];
                            string fullCommand = AddMissingZeros(command);

                            bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                            if (IsFistParamInPositionMode)
                            {
                                result = ints[first];
                                //Console.WriteLine($"Output: {ints[result]}");
                                solution = ints[result];
                            }
                            else
                            {
                                //Console.WriteLine($"Output: {first}");
                                solution = first;
                            }
                        }
                        else
                        {
                            result = ints[oct + 1];
                            //Console.WriteLine($"Output: {ints[result]}");
                            solution = ints[result];
                        }

                        oct += 2;
                    }
                    else if (operant == 5 || operant == 6)
                    {
                        if (ints[oct] > 10)
                        {
                            int command = ints[oct];
                            string fullCommand = AddMissingZeros(command);

                            bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                            bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                            bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;

                            int firstValue = IsFistParamInPositionMode ? ints[first] : first;
                            int secoundValue = IsSecoundParamInPositionMode ? ints[secound] : secound;

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
                                if (ints[first] != 0)
                                {
                                    oct = ints[secound];
                                    continue;
                                }
                                oct += 3;
                            }
                            if (operant == 6)
                            {
                                if (ints[first] == 0)
                                {
                                    oct = ints[secound];
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
                            int command = ints[oct];
                            string fullCommand = AddMissingZeros(command);

                            bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                            bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;

                            int firstValue = IsFistParamInPositionMode ? ints[first] : first;
                            int secoundValue = IsSecoundParamInPositionMode ? ints[secound] : secound;

                            if (operant == 7)
                            {
                                if (firstValue < secoundValue)
                                {
                                    ints[result] = 1;
                                }
                                else
                                {
                                    ints[result] = 0;
                                }
                            }
                            if (operant == 8)
                            {
                                if (firstValue == secoundValue)
                                {
                                    ints[result] = 1;
                                }
                                else
                                {
                                    ints[result] = 0;
                                }
                            }
                        }
                        else
                        {
                            if (operant == 7)
                            {
                                if (ints[first] < ints[secound])
                                {
                                    ints[result] = 1;
                                }
                                else
                                {
                                    ints[result] = 0;
                                }
                            }
                            if (operant == 8)
                            {
                                if (ints[first] == ints[secound])
                                {
                                    ints[result] = 1;
                                }
                                else
                                {
                                    ints[result] = 0;
                                }
                            }
                        }

                        oct += 4;
                    }
                    else
                    {
                        //Console.WriteLine("BUM");
                        break;
                    }
                }

            return solution;
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

        private static void permute(String str,  int l, int r)
        {
            if (l == r) {
                combinations.Add(str);
            }
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = swap(str, l, i);
                    permute(str, l + 1, r);
                    str = swap(str, l, i);
                }
            }
        }

        public static String swap(String a, int i, int j)
        {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }
    }
}