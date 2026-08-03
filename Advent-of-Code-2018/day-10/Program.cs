using System.Text;
using System.Text.RegularExpressions;

string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
List<Lights> lights = new List<Lights>();
foreach(var light in input)
    lights.Add(new Lights(light));

Console.WriteLine("########## Day 9 2018 ##########");
Console.WriteLine($"Part TWO solution: {SolvePartOne(lights)}");
Console.WriteLine("################################");


int SolvePartOne(List<Lights> lights)
{
    var minX = lights.Min(x => x.X);
    var minY = lights.Min(x => x.Y);
    var maxX = lights.Max(x => x.X);
    var maxY = lights.Max(x => x.Y);

    long area = (maxX - minX) * (maxY - minY);

    int seconds = 0;
    while (true) 
    {

        lights.ForEach(x => x.PrevMove());

        minX = lights.Min(x => x.PrevX);
        minY = lights.Min(x => x.PrevY);
        maxX = lights.Max(x => x.PrevX);
        maxY = lights.Max(x => x.PrevY);

        long area2 = (maxX - minX) * (maxY - minY);

        if(area2>area)
        {
            Console.WriteLine($"Part ONE solution:" + Environment.NewLine);
            PrintGrid(lights);

            break; 
        }

        lights.ForEach(x => x.Move());
        area = area2;
        seconds++;
    }

    return seconds;
}


void PrintGrid(List<Lights> points)
{
    long minX = points.Min(p => p.X);
    long maxX = points.Max(p => p.X);
    long minY = points.Min(p => p.Y);
    long maxY = points.Max(p => p.Y);

    var occupied = new HashSet<(long, long)>(points.Select(p => (p.X, p.Y)));

    for (long y = minY; y <= maxY; y++)
    {
        var sb = new StringBuilder();
        for (long x = minX; x <= maxX; x++)
        {
            sb.Append(occupied.Contains((x, y)) ? '#' : '.');
        }
        Console.WriteLine(sb.ToString());
    }
}

class Lights
{
    public long X { get; set; }
    public long Y { get; set; }
    public long DeltaX { get; set; }
    public long DeltaY { get; set; }

    public long PrevX { get; set; }
    public long PrevY { get; set; }

    private static readonly Regex Pattern = new Regex(
    @"position=<\s*(-?\d+),\s*(-?\d+)> velocity=<\s*(-?\d+),\s*(-?\d+)>");

    public Lights(string line)
    {
        var match = Pattern.Match(line);

        X = long.Parse(match.Groups[1].Value);
        Y = long.Parse(match.Groups[2].Value);
        PrevX = long.Parse(match.Groups[1].Value);
        PrevY = long.Parse(match.Groups[2].Value);
        DeltaX = long.Parse(match.Groups[3].Value);
        DeltaY = long.Parse(match.Groups[4].Value);
    }

    public void Move() 
    {
        X += DeltaX;
        Y += DeltaY;
    }

    public void PrevMove()
    {
        PrevX += DeltaX;
        PrevY += DeltaY;
    }
}