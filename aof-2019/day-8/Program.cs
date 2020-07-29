using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Non-test case requires 25 pixels wide and 6 pixels tall image. \nPlease insert with for user case.");
            string imageData = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("Insert pixel wide: ");
            string wideString = Console.ReadLine();
            Console.WriteLine("Insert pixel tall: ");
            string tallString = Console.ReadLine();

            int wideNum = Int32.Parse(wideString);
            int tallNum = Int32.Parse(tallString);
            int pixelNumber = wideNum * tallNum;
            List<string> allChuncks = new List<string>();
            for (int i = 0; i < imageData.Length; i += pixelNumber)
            {
                if (i + pixelNumber > imageData.Length)
                {
                    pixelNumber = imageData.Length - i;
                }

                string data = imageData.Substring(i, pixelNumber);
                allChuncks.Add(data);
            }
            Console.WriteLine("################### PART ONE ###################");
            SolvePartOne(allChuncks);
            Console.WriteLine();
            Console.WriteLine("################### PART TWO ###################");
            SolvePartTwo(wideNum, tallNum, allChuncks);
        }
        private static void SolvePartOne(List<string> allChuncks)
        {
            string testChunk = String.Empty;
            int minOcc = 0;
            foreach (string chunk in allChuncks)
            {
                int number = chunk.Count(x => x == '0');
                if (testChunk.Equals(String.Empty))
                {
                    testChunk = chunk;
                    minOcc = number;
                }
                if (number < minOcc)
                {
                    testChunk = chunk;
                    minOcc = number;
                }
            }

            int ones = testChunk.Count(x => x == '1');
            int twos = testChunk.Count(x => x == '2');

            Console.WriteLine($"The result of multiplying digits 1 with 2 digits is {ones * twos}");
        }

        private static void SolvePartTwo(int wideNum, int tallNum, List<string> allChuncks)
        {
            string pixelChunk = String.Empty;
            for (int i = 0; i < allChuncks[0].Length; i++) 
            {
                foreach (string chunk in allChuncks) 
                {
                    if (chunk[i].ToString().Equals("0") || chunk[i].ToString().Equals("1")) 
                    {
                        if (chunk[i].ToString().Equals("0"))
                        {
                            pixelChunk += " ";
                        }
                        else if (chunk[i].ToString().Equals("1"))
                        {
                            pixelChunk += "#";
                        }

                        break;
                    }
                }
            }
            
            Console.WriteLine("Decompresing image...");
            Console.WriteLine();
            for (int i = 0; i < tallNum; i++) 
            {
                for (int y = 0; y < wideNum; y++) 
                {
                    string pixel = pixelChunk.ElementAt(wideNum * i + y).ToString();
                    Console.Write(pixel);
                }

                Console.WriteLine();
            }
        }
    }
}
