using System; 

namespace day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
            for (int i = 0; i < 100; i++) 
            {
                for (int j = 0; j < 100; j++) 
                {
                    int[] programs = new int[ints.Length];
                    Array.Copy(ints, programs, ints.Length);
                    programs[1]= i;
                    programs[2] = j;
                    int oct = 0;
                    while (true)
                    {
                        if (oct > programs.Length)
                        {
                            Console.WriteLine($"out of memory");
                            break;
                        }
                        if (programs[oct] == 99)
                        {
                            break;
                        }

                        int first = programs[oct + 1];
                        int secound = programs[oct + 2];
                        if (first >= ints.Length)
                        {
                            break;
                        }
                        int resultFirs = programs[first];

                        if (secound >= ints.Length)
                        {
                            break;
                        }
                        int resultSec = programs[secound];
                        int resultAdress = programs[oct + 3];

                        if (resultAdress >= ints.Length)
                        {
                            break;
                        }

                        int g = programs[resultAdress];
                        int operant = programs[oct];

                        if (operant == 1)
                        {
                            programs[resultAdress] = resultFirs + resultSec;
                        }
                        else if (operant == 2)
                        {
                            programs[resultAdress] = resultFirs * resultSec;
                        }

                        oct += 4;
                    }

                    if (programs[0]==19690720) 
                    {
                        Console.WriteLine($"ints[1] = {programs[1]}, ints[2]={programs[2]} = {programs[0]}");
                        Console.WriteLine(String.Join(',', ints));

                    }
                }
            }
        }
    }
}