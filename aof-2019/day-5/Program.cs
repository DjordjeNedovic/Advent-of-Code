using System;

namespace day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();
        }

        private static void PartOne()
        {
            int result = 0;
            Console.WriteLine("Enter input: ");
            string keyInput = Console.ReadLine();
            int input = Int32.Parse(keyInput);
            int[] ints = new int[] { 3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 1101, 90, 64, 225, 1101, 15, 56, 225, 1, 14, 153, 224, 101, -147, 224, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 3, 224, 1, 224, 223, 223, 2, 162, 188, 224, 101, -2014, 224, 224, 4, 224, 1002, 223, 8, 223, 101, 6, 224, 224, 1, 223, 224, 223, 1001, 18, 81, 224, 1001, 224, -137, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 3, 224, 1, 223, 224, 223, 1102, 16, 16, 224, 101, -256, 224, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 6, 224, 1, 223, 224, 223, 101, 48, 217, 224, 1001, 224, -125, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 3, 224, 1, 224, 223, 223, 1002, 158, 22, 224, 1001, 224, -1540, 224, 4, 224, 1002, 223, 8, 223, 101, 2, 224, 224, 1, 223, 224, 223, 1101, 83, 31, 225, 1101, 56, 70, 225, 1101, 13, 38, 225, 102, 36, 192, 224, 1001, 224, -3312, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 4, 224, 1, 224, 223, 223, 1102, 75, 53, 225, 1101, 14, 92, 225, 1101, 7, 66, 224, 101, -73, 224, 224, 4, 224, 102, 8, 223, 223, 101, 3, 224, 224, 1, 224, 223, 223, 1101, 77, 60, 225, 4, 223, 99, 0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1, 99999, 1005, 227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999, 1006, 0, 99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280, 1105, 1, 99999, 1, 225, 225, 225, 1101, 294, 0, 0, 105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999, 1, 225, 225, 225, 1101, 314, 0, 0, 106, 0, 0, 1105, 1, 99999, 7, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 329, 1001, 223, 1, 223, 1007, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 344, 101, 1, 223, 223, 108, 226, 226, 224, 1002, 223, 2, 223, 1006, 224, 359, 101, 1, 223, 223, 7, 226, 226, 224, 102, 2, 223, 223, 1005, 224, 374, 101, 1, 223, 223, 8, 677, 677, 224, 1002, 223, 2, 223, 1005, 224, 389, 1001, 223, 1, 223, 107, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 404, 101, 1, 223, 223, 1107, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 419, 1001, 223, 1, 223, 1008, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 434, 1001, 223, 1, 223, 7, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 449, 1001, 223, 1, 223, 1107, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 464, 101, 1, 223, 223, 1108, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 479, 101, 1, 223, 223, 1007, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 494, 1001, 223, 1, 223, 1107, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 509, 101, 1, 223, 223, 1007, 226, 226, 224, 1002, 223, 2, 223, 1006, 224, 524, 101, 1, 223, 223, 107, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 539, 1001, 223, 1, 223, 1108, 677, 677, 224, 1002, 223, 2, 223, 1005, 224, 554, 101, 1, 223, 223, 1008, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 569, 1001, 223, 1, 223, 8, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 584, 1001, 223, 1, 223, 1008, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 599, 1001, 223, 1, 223, 108, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 614, 1001, 223, 1, 223, 108, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 629, 101, 1, 223, 223, 8, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 644, 101, 1, 223, 223, 107, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 659, 101, 1, 223, 223, 1108, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 674, 1001, 223, 1, 223, 4, 223, 99, 226 };
            int oct = 0;
            while (true)
            {
                if (oct > ints.Length)
                {
                    Console.WriteLine($"out of memory");
                    break;
                }
                if (ints[oct] == 99)
                {
                    Console.WriteLine($"index {oct} returs 99");
                    break;
                }

                int first = ints[oct + 1];
                int operant = ints[oct] % 10;
                if (operant == 1 || operant == 2)
                {

                    int secound = ints[oct + 2];
                    result = ints[oct + 3];
                    if (secound >= ints.Length || first >= ints.Length || result >= ints.Length)
                    {
                        Console.WriteLine(String.Join(',', ints));
                        break;
                    }

                    if (ints[oct] > 10)
                    {
                        int command = ints[oct];
                        string fullCommand = AddMissingZeros(command);

                        bool IsFistParamInPositionMode = (Int32.Parse(fullCommand[2].ToString()) == 0) ? true : false;
                        bool IsSecoundParamInPositionMode = (Int32.Parse(fullCommand[1].ToString()) == 0) ? true : false;
                        bool IsAddressParamInPositionMode = (Int32.Parse(fullCommand[0].ToString()) == 0) ? true : false;

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
                    ints[result] = input;
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
                            Console.WriteLine($"Oct 4 output: {ints[result]}, oct {oct}");

                        }
                        else
                        {
                            Console.WriteLine($"Oct 4 output: {first}, oct {oct}");
                        }
                    }
                    else
                    {
                        result = ints[oct + 1];
                        Console.WriteLine($"Oct 4 output: {ints[result]}, oct {oct}");

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
