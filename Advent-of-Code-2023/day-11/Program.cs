string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

Console.WriteLine("########## Day 11 2023 ##########");
Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
Console.WriteLine("################################");

long SolvePartOne(string[] input)
{
    List<Point> points = CreateFullMap(input, 1);
    Dictionary<string, long> map = Calculate(points);

    return map.Values.Sum();
}

long SolvePartTwo(string[] input) 
{
    List<Point> points = CreateFullMap(input, 999999);
    Dictionary<string, long> map = Calculate(points);

    return map.Values.Sum();
}

List<Point> CreateFullMap(string[] input, int multi)
{
    List<Point> points = new List<Point>();
    var rowsToAdd = Enumerable.Range(0, input.Length).Where(row => input[row].All(c => c == '.')).ToArray();
    var colsToAdd = Enumerable.Range(0, input[0].Length).Where(col => input.All(l => l[col] == '.')).ToArray();

    for (var row = 0; row < input.Length; row++)
    {
        for (var col = 0; col < input[0].Length; col++)
        {
            if (input[row][col] != '#')
            {
                continue;
            }

            var colOffset = colsToAdd.Count(c => c <= col) * multi;
            var rowOffset = rowsToAdd.Count(r => r <= row) * multi;

            points.Add(new Point() { X = col + colOffset, Y = row + rowOffset });
        }
    }

    return points;
}

Dictionary<string, long> Calculate(List<Point> points)
{
    Dictionary<string, long> map = new Dictionary<string, long>();
    int i = 0;
    foreach (var p1 in points)
    {
        i++;
        foreach (var p2 in points.Skip(i))
        {
            if (p1.X == p2.X && p1.Y == p2.Y)
                continue;

            long mda = Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
            map.Add($"{p1.X},{p1.Y}|{p2.X},{p2.Y}", mda);
        }
    }

    return map;
}

class Point 
{
    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}