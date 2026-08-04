string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
char[][] map = new char[input.Length][];
List<Cart> carts = new List<Cart>();
List<Cart> cartsPartTwo = new List<Cart>();

for (int y = 0; y < input.Length; y++)
{
    map[y] = new char[input[y].Length];
    for (int x = 0; x < input[0].Length; x++) 
    {
        if (input[y][x].ToString() == "")
            continue;
        string value = input[y][x].ToString();
        if (value == ">" || value == "<" || value == "^" || value == "v")
        {
            Cart c = new Cart();
            c.X = x;
            c.Y = y;
            c.FutureStep = FutureStep.Left;
            if (value == ">")
            {
                c.Direction= Direction.Right;
                map[y][x] = '-';
            }
            else if (value == "<")
            {
                c.Direction = Direction.Left;
                map[y][x] = '-';
            }
            else if (value == "^")
            {
                c.Direction = Direction.Up;
                map[y][x] = '|';
            }
            else 
            {
                c.Direction = Direction.Down;
                map[y][x] = '|';
            }

            carts.Add(c);
            cartsPartTwo.Add(c);
        }
        else 
        {
            map[y][x] = input[y][x];
        }
    }
}

Console.WriteLine("########## Day 9 2018 ##########");
Console.WriteLine($"Part one solution: {SolvePartOne(map, carts)}");
Console.WriteLine($"Part one solution: {SolvePartTwo(map, cartsPartTwo)}");
Console.WriteLine("################################");

string SolvePartOne(char[][] map, List<Cart> carts)
{
    while (true) 
    {
        carts = carts.OrderBy(x => x.Y).ThenBy(x => x.X).ToList();

        foreach (Cart c in carts) 
        {
            c.Move(map);
            var collision = carts
                .GroupBy(x => (x.X, x.Y))
                .FirstOrDefault(g => g.Count() > 1);

            if (collision != null)
            {
                return $"{collision.Key}";
            }
        }
    }
}

string SolvePartTwo(char[][] map, List<Cart> carts) 
{
    while (true)
    {
        carts = carts.OrderBy(x => x.Y).ThenBy(x => x.X).ToList();

        foreach (Cart c in carts.ToList())
        {
            if (!carts.Contains(c))
                continue;

            c.Move(map);
            var collision = carts
                .GroupBy(x => (x.X, x.Y))
                .FirstOrDefault(g => g.Count() > 1);

            if (collision != null)
            {
                carts.RemoveAll(x => x.X == collision.Key.X && x.Y == collision.Key.Y);
            }
        }

        if (carts.Count == 1)
            break;
    }

    return $"{carts.First().X},{carts.First().Y}";
}

class Cart 
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }
    public FutureStep FutureStep { get; set; }

    public void Move(char[][] map) 
    {
        string tile = map[Y][X].ToString();
        if (tile == "|" && Direction == Direction.Up)
        {
            Y -= 1;
        }
        else if (tile == "|" && Direction == Direction.Down)
        {
            Y += 1;
        }
        else if (tile == "-" && Direction == Direction.Left)
        {
            X -= 1;
        }
        else if (tile == "-" && Direction == Direction.Right)
        {
            X += 1;
        }
        else if (tile == "/" && Direction == Direction.Up)
        {
            Direction = Direction.Right;
            X += 1;
        }
        else if (tile == "/" && Direction == Direction.Left)
        {
            Direction = Direction.Down;
            Y += 1;
        }
        else if (tile == "/" && Direction == Direction.Right)
        {
            Direction = Direction.Up;
            Y -= 1;
        }
        else if (tile == "/" && Direction == Direction.Down)
        {
            Direction = Direction.Left;
            X -= 1;
        }
        else if (tile == "\\" && Direction == Direction.Up)
        {
            Direction = Direction.Left;
            X -= 1;
        }
        else if (tile == "\\" && Direction == Direction.Right)
        {
            Direction = Direction.Down;
            Y += 1;
        }
        else if (tile == "\\" && Direction == Direction.Down)
        {
            Direction = Direction.Right;
            X += 1;
        }
        else if (tile == "\\" && Direction == Direction.Left)
        {
            Direction = Direction.Up;
            Y -= 1;
        }
        else 
        {
            if (Direction == Direction.Up && FutureStep == FutureStep.Left) 
            {
                Direction = Direction.Left;
                FutureStep = FutureStep.Streight;
                X -= 1;
            }

            else if (Direction == Direction.Up && FutureStep == FutureStep.Right)
            {
                Direction = Direction.Right;
                FutureStep = FutureStep.Left;
                X += 1;
            }
            ///////////
            else if (Direction == Direction.Right && FutureStep == FutureStep.Left)
            {
                Direction = Direction.Up;
                FutureStep = FutureStep.Streight;
                Y -= 1;
            }

            else if (Direction == Direction.Right && FutureStep == FutureStep.Right)
            {
                Direction = Direction.Down;
                FutureStep = FutureStep.Left;
                Y += 1;
            }
            //////
            else if (Direction == Direction.Down && FutureStep == FutureStep.Left)
            {
                Direction = Direction.Right;
                FutureStep = FutureStep.Streight;
                X += 1;
            }

            else if (Direction == Direction.Down && FutureStep == FutureStep.Right)
            {
                Direction = Direction.Left;
                FutureStep = FutureStep.Left;
                X -= 1;
            }
            /////////
            else if (Direction == Direction.Left && FutureStep == FutureStep.Left)
            {
                Direction = Direction.Down;
                FutureStep = FutureStep.Streight;
                Y+=1;
            }

            else if (Direction == Direction.Left && FutureStep == FutureStep.Right)
            {
                Direction = Direction.Up;
                FutureStep = FutureStep.Left;
                Y-= 1;
            }
            /////////
            else if (FutureStep == FutureStep.Streight)
            {
                FutureStep = FutureStep.Right;
                if (Direction == Direction.Up)
                {
                    Y -= 1;
                }
                else if (Direction == Direction.Down)
                {
                    Y += 1;
                }
                else if (Direction == Direction.Left)
                {
                    X -= 1;
                }
                else if (Direction == Direction.Right)
                {
                    X += 1;
                }
            }

        }
    }
}

enum FutureStep 
{
    Left,
    Streight,
    Right
}

enum Direction
{
    Left,
    Up,
    Right,
    Down
}