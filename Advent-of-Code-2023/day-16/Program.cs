char[][] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Select(x=>x.ToCharArray()).ToArray();

Dictionary<(int, int), char> keyValuePairs = new Dictionary<(int, int), char>();
HashSet<Beam> paths = new HashSet<Beam>();

for (int y = 0; y < input.Length; y++) 
{
    for(int x = 0; x < input[y].Length; x++)
    {
        var currentChar = input[y][x];
        keyValuePairs.Add((x, y), currentChar);
    }
}

var startBeam = new Beam() { X = 0, Y = 0, Direction = Direction.East };
SolvePartOne(startBeam, keyValuePairs);
SolvePartTwo(keyValuePairs, input[0].Length, input.Length);

void SolvePartOne(Beam startBeam, Dictionary<(int, int), char> keyValuePairs)
{
    FollowBeam(startBeam, keyValuePairs, paths);
    var num =paths.Select(x=> (x.X, x.Y)).Distinct().Count();
    Console.WriteLine(num);
}

void SolvePartTwo(Dictionary<(int, int), char> keyValuePairs, int xAsix, int yAsix)
{
    paths.Clear();
    List<int> results= new List<int>();
    for (int i = 0; i < xAsix; i++) 
    {
        startBeam = new Beam() { X=i, Y=0, Direction = Direction.South };
        FollowBeam(startBeam, keyValuePairs, paths);
        var num = paths.Select(x => (x.X, x.Y)).Distinct().Count();
        paths.Clear();
        results.Add(num);

        startBeam = new Beam() { X = i, Y = yAsix -1, Direction = Direction.North };
        FollowBeam(startBeam, keyValuePairs, paths);
        num = paths.Select(x => (x.X, x.Y)).Distinct().Count();
        paths.Clear();
        results.Add(num);
    }

    for(int j = 0; j < yAsix; j++) 
    {
        startBeam = new Beam() { X = 0, Y = j, Direction = Direction.East };
        FollowBeam(startBeam, keyValuePairs, paths);

        var num = paths.Select(x => (x.X, x.Y)).Distinct().Count();
        paths.Clear();
        results.Add(num);

        startBeam = new Beam() { X = xAsix, Y = j, Direction = Direction.West };
        FollowBeam(startBeam, keyValuePairs, paths);
        num = paths.Select(x => (x.X, x.Y)).Distinct().Count();
        paths.Clear();
        results.Add(num);
    }

    Console.WriteLine(results.Max());
}

static Beam FollowBeam(Beam startBeam, Dictionary<(int, int), char> keyValuePairs, HashSet<Beam> paths)
{
    while (true)
    {
        if (paths.Any(x=>x.X == startBeam.X && x.Y == startBeam.Y && x.Direction == startBeam.Direction) || !keyValuePairs.ContainsKey((startBeam.X, startBeam.Y)))
            break;

        char ch = keyValuePairs[(startBeam.X, startBeam.Y)];
        paths.Add(startBeam);
        if (ch == '.')
        {
            int newX = startBeam.X;
            int newY = startBeam.Y;
            if (startBeam.Direction == Direction.North)
            {
                newY -= 1;
            }
            else if (startBeam.Direction == Direction.South)
            {
                newY += 1;
            }
            else if (startBeam.Direction == Direction.East)
            {
                newX += 1;
            }
            else
            {
                newX -= 1;
            }

            startBeam = new Beam() { X = newX, Y = newY, Direction = startBeam.Direction };
        }
        else if (ch == '|')
        {
            var newBeam = new Beam() { X = startBeam.X, Y = startBeam.Y - 1, Direction = Direction.North };
            FollowBeam(newBeam, keyValuePairs, paths);
            newBeam = new Beam() { X = startBeam.X, Y = startBeam.Y + 1, Direction = Direction.South };
            FollowBeam(newBeam, keyValuePairs, paths);
        }
        else if (ch == '-')
        {
            var newBeam = new Beam() { X = startBeam.X - 1, Y = startBeam.Y, Direction = Direction.West };
            FollowBeam(newBeam, keyValuePairs, paths);
            newBeam = new Beam() { X = startBeam.X + 1, Y = startBeam.Y, Direction = Direction.East };
            FollowBeam(newBeam, keyValuePairs, paths);
        }
        else if (ch == '/') 
        {
            int newX = startBeam.X;
            int newY = startBeam.Y;
            Direction newDirection = startBeam.Direction;
            if (startBeam.Direction == Direction.North)
            {
                newX += 1;
                newDirection = Direction.East;
            }
            else if (startBeam.Direction == Direction.South)
            {
                newX -= 1;
                newDirection = Direction.West;
            }
            else if (startBeam.Direction == Direction.East)
            {
                newY -= 1;
                newDirection = Direction.North;
            }
            else
            {
                newY += 1;
                newDirection = Direction.South;
            }

            startBeam = new Beam() { X = newX, Y = newY, Direction = newDirection };
        }
        else 
        {
            int newX = startBeam.X;
            int newY = startBeam.Y;
            Direction newDirection = startBeam.Direction;
            if (startBeam.Direction == Direction.North)
            {
                newX -= 1;
                newDirection = Direction.West;
            }
            else if (startBeam.Direction == Direction.South)
            {
                newX += 1;
                newDirection = Direction.East;
            }
            else if (startBeam.Direction == Direction.East)
            {
                newY += 1;
                newDirection = Direction.South;
            }
            else
            {
                newY -= 1;
                newDirection = Direction.North;
            }

            startBeam = new Beam() { X = newX, Y = newY, Direction = newDirection };
        }
    }

    return startBeam;
}

class Beam 
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }

    public override string ToString()
    {
        return $"X {X}, Y {Y}";
    }
}

enum Direction 
{
    North,
    West,
    East,
    South
}