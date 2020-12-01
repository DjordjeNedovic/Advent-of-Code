using System;
using System.IO;

namespace day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            int[] testValues = Array.ConvertAll(input.Split(','), s => Int32.Parse(s));
            for (int firstIndex = 0; firstIndex < 100; firstIndex++) 
            {
                for (int secondIndex = 0; secondIndex < 100; secondIndex++) 
                {
                    int[] variations = new int[testValues.Length];
                    Array.Copy(testValues, variations, testValues.Length);
                    variations[1] = firstIndex;
                    variations[2] = secondIndex;
                    int oct = 0;
                    while (true)
                    {
                        if (oct > variations.Length)
                        {
                            Console.WriteLine($"out of memory");
                            break;
                        }
                        if (variations[oct] == 99)
                        {
                            break;
                        }

                        int firstParam = variations[oct + 1];
                        if (firstParam >= testValues.Length)
                        {
                            break;
                        }

                        int resultFirs = variations[firstParam];
                        
                        int secoundParam = variations[oct + 2];
                        if (secoundParam >= testValues.Length)
                        {
                            break;
                        }

                        int resultSec = variations[secoundParam];
                        
                        int resultAdress = variations[oct + 3];
                        if (resultAdress >= testValues.Length)
                        {
                            break;
                        }

                        int operant = variations[oct];
                        if (operant == 1)
                        {
                            variations[resultAdress] = resultFirs + resultSec;
                        }
                        else if (operant == 2)
                        {
                            variations[resultAdress] = resultFirs * resultSec;
                        }

                        oct += 4;
                    }

                    if (variations[0]==19690720) 
                    {
                        Console.WriteLine($"ints[1] = {variations[1]}, ints[2]={variations[2]} = {variations[0]}");
                        Console.WriteLine($"100 * noun + verb = {100 * variations[1] + variations[2]}");
                    }
                }
            }
        }
    }
}