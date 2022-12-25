namespace day_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 09 2022 ##########");
            //Console.WriteLine($"Part one solution: {SolvePartOne(input, 2)}");
            Console.WriteLine($"Part two solution: {SolvePartOne(input, 10)}");
            Console.WriteLine("#################################");
        }

        private static int SolvePartOne(string[] input, int ropeLen)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            int headX = 0;
            int headY = 0;
            int tailX = 0;
            int tailY = 0;

            foreach (string command in input) 
            {
                string direction = command.Split(' ')[0];
                int size = Int32.Parse(command.Split(' ')[1]);

                if (direction == "R")
                {
                    headX += size;
                    if (Check(headX, headY, tailX, tailY,ropeLen)) 
                    {
                        continue;
                    }

                    if (headY != tailY)
                    {
                        if (headY > tailY)
                        {
                            tailY += 1;
                            tailX += 1;
                        }
                        else 
                        {
                            tailY -= 1;
                            tailX += 1;
                        }
                    }

                    for (int i = tailX; i < headX; i++)
                    {
                        string key = $"{i},{tailY}";
                        if (result.ContainsKey(key))
                        {
                            result[key]++;
                        }
                        else
                        {
                            result.Add(key, 1);
                        }
                    }

                    tailX = headX - 1;
                }
                else if (direction == "L")
                {
                    headX -= size;
                    if (Check(headX, headY, tailX, tailY,ropeLen))
                    {
                        continue;
                    } 
                    if (headY != tailY)
                    {
                        if (headY > tailY)
                        {
                            tailY += 1;
                            tailX -= 1;
                        }
                        else
                        {
                            tailY -= 1;
                            tailX -= 1;
                        }
                    }

                    for (int i = tailX; i > headX; i--)
                    {
                        string key = $"{i},{tailY}";
                        if (result.ContainsKey(key))
                        {
                            result[key]++;
                        }
                        else
                        {
                            result.Add(key, 1);
                        }
                    }

                    tailX = headX + 1;
                }
                else if (direction == "U")
                {
                    headY += size;
                    if (Check(headX, headY, tailX, tailY,ropeLen))
                    {
                        continue;
                    }

                    if (headX != tailX) 
                    {
                        if (headX > tailX)
                        {
                            tailX += 1;
                            tailY += 1;
                        }
                        else 
                        {
                            tailY += 1;
                            tailX -= 1;
                        }
                    }

                    for (int i = tailY; i < headY; i++)
                    {
                        string key = $"{tailX},{i}";
                        if (result.ContainsKey(key))
                        {
                            result[key]++;
                        }
                        else
                        {
                            result.Add(key, 1);
                        }
                    }

                    tailY = headY - 1;
                }
                else 
                {
                    headY -= size;
                    if (Check(headX, headY, tailX, tailY,ropeLen))
                    {
                        continue;
                    }
                    
                    if (headX != tailX)
                    {
                        if (headX > tailX)
                        {
                            tailX += 1;
                            tailY -= 1;
                        }
                        else
                        {
                            tailY -= 1;
                            tailX -= 1;
                        }
                    }

                    for (int i = tailY; i > headY; i--)
                    {
                        string key = $"{tailX},{i}";
                        if (result.ContainsKey(key))
                        {
                            result[key]++;
                        }
                        else
                        {
                            result.Add(key, 1);
                        }
                    }

                    tailY=headY + 1;
                }

                //Console.WriteLine($"Direction {direction},{size} - H:{headX},{headY} - T:{tailX},{tailY}");
            }

            return result.Count;
        }

        private static bool Check(int headX, int headY, int tailX, int tailY, int size)
        {
            int half = size -1;
            List<string> co2 = new List<string>();
            for (int i = headY - half; i <= headY + half; i++) 
            {
                for (int x = headX - half; x <= headX + half; x++) 
                {
                    co2.Add($"{x},{i}");
                }
            }

            return co2.Any(x => x.Equals($"{tailX},{tailY}"));
        }

        private static int SolvePartTwo(string[] input)
        {
            return 0;
        }
    }
}
