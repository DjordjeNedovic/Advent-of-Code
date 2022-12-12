namespace day_10
{
    internal class Program
    {
        private static int cycle = 0;
        private static int register = 1;
        private static int result = 0;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 10 2022 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: ");
            SolvePartTwo(input);
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(string[] input)
        {
            foreach (var line in input)
            {
                if (line.Equals("noop"))
                {
                    Tick();
                }
                else
                {
                    int t = Int32.Parse(line.Split(" ")[1]);
                    Tick();
                    Tick();

                    register += t;
                }
            }

            return result;
        }

        private static object SolvePartTwo(string[] input)
        {
            cycle = 0;
            register = 1;
            result = 0;

            foreach (var line in input)
            {
                if (line.Equals("noop"))
                {
                    TickAndDraw();
                }
                else
                {
                    int t = Int32.Parse(line.Split(" ")[1]);
                    TickAndDraw();
                    TickAndDraw();

                    register += t;
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            return " ";
        }

        private static void Tick()
        {
            cycle++;
            if (IsRightCyrcle(cycle))
            {
                result += register * cycle;
            }
        }

        private static bool IsRightCyrcle(int cycle)
        {
            return (cycle == 20 || cycle == 60 || cycle == 100 || cycle == 140 || cycle == 180 || cycle == 220);
        }

        private static void TickAndDraw()
        {
            if (cycle % 40 == 0)
            {
                Console.WriteLine();
            }
            if (cycle % 40 == register - 1 || cycle % 40 == register + 1 || cycle % 40 == register)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(" ");
            }

            cycle++;
        }
    }
}
