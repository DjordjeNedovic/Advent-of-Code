namespace day_08
{
    class Program
    {
        private static int partOneCount = 0;
        
        static void Main(string[] args)
        {
            string[] input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Split(" ");
            int[] startArray = Array.ConvertAll(input, x => Int32.Parse(x));
            Console.WriteLine("########## Day 8 2018 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(startArray)}");
            Console.WriteLine($"Part one solution: {SolvePartTwo(startArray)}");
            Console.WriteLine("################################");
        }

        private static object SolvePartOne(int[] input)
        {
            RecursivePartOne(input, 0);

            return partOneCount;
        }

        private static object SolvePartTwo(int[] input)
        {
            var res = RecursivePartTwo(input, 0);

            return res.Item2;
        }

        private static int RecursivePartOne(int[] body, int currentIndex)
        {
            var childCount = body[currentIndex];
            var metaCount = body[currentIndex + 1];

            currentIndex += 2;
            while (childCount > 0)
            {
                currentIndex = RecursivePartOne(body, currentIndex);
                childCount--;
            }

            partOneCount += body[currentIndex..(currentIndex + metaCount)].Sum();
            currentIndex += metaCount;
            return currentIndex;
        }

        static Tuple<int, int> RecursivePartTwo(int[] headers, int currIndex)
        {
            var childCount = headers[currIndex];
            var metaCount = headers[currIndex + 1];

            var children = new int[childCount];
            var val = 0;
            currIndex += 2;

            if (childCount == 0)
            {
                while (metaCount > 0)
                {
                    val += Convert.ToInt32(headers[currIndex]);
                    metaCount--;
                    currIndex += 1;
                }
            }

            int childIndex = 0;
            while (childCount > 0)
            {
                (currIndex, children[childIndex]) = RecursivePartTwo(headers, currIndex);
                childIndex += 1;
                childCount--;
            }

            while (metaCount > 0)
            {
                var metaVal = headers[currIndex] - 1;

                if (metaVal >= 0 && metaVal < children.Length)
                    val += children[metaVal];

                currIndex += 1;
                metaCount--;
            }


            return new Tuple<int, int>(currIndex, val);
        }
    }
}