var input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Select(l => l.Split(' ')).Select(l => (l[0][0], long.Parse(l[1]))).ToList();

SolvePartOne(input);

var input2 = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt")).Select(x=>x.Split(' ')[2]).ToArray();


SolvePartTwo(input2);

void SolvePartOne(List<(char, long)> input) 
{
    (long, long) node = (0, 0);
    List<(long, long)> nodes = new List<(long, long)>() { node };
    long sumOfSteps = 0;
    foreach (var p in input)
    {
        long steps = p.Item2;
        char direction = p.Item1;
        if (direction == 'R') 
            node = (node.Item1 + steps, node.Item2);
        else if(direction == 'L')
            node = (node.Item1 - steps, node.Item2);
        else if(direction == 'D')
            node = (node.Item1, node.Item2 + steps);
        else
            node = (node.Item1, node.Item2 - steps);

        nodes.Add(node);

        sumOfSteps += steps;
    }

    /*
     The shoelace formula does not give you the number of interior points,
     it gives you the area of the polygon. Once you have the area of the
     polygon, and you have counted the number of boundary points of the
     polygon, you can use the equation from Pick's theorem to solve
     for the number of interior points.
     
     Thanks to:
     https://www.reddit.com/r/adventofcode/comments/18l0qtr/comment/kecif1x/
     */
    long area = 0;
    for (int j = 0; j < nodes.Count - 1; j++)
        area += Shoelace(nodes[j].Item1, nodes[j].Item2, nodes[j + 1].Item1, nodes[j + 1].Item2);
    area = Math.Abs(area);

    Console.WriteLine($"{(area + sumOfSteps) / 2 + 1}");
}

void SolvePartTwo(string[] input2)
{
    List<(long, char)> instructions = new List<(long, char)>();
    foreach (var p in input2) 
    {
        var newP = String.Join("", p.Replace("(", "").Replace(")", "").Replace("#", "").Take(5));
        long answer = Convert.ToInt64(newP, 16);
        char d = p.ElementAt(7);
        if (d == '0')
            instructions.Add( (answer, 'R'));
        else if (d == '1')
            instructions.Add( (answer, 'D'));
        else if (d == '2')
            instructions.Add( (answer, 'L'));
        else if (d == '3')
            instructions.Add( (answer, 'U'));
    }


    (long, long) node = (0, 0);
    List<(long, long)> nodes = new List<(long, long)>() { node };
    long sumOfSteps = 0;
    foreach (var p in instructions)
    {
        long steps = p.Item1;
        char direction = p.Item2;
        if (direction == 'R')
            node = (node.Item1 + steps, node.Item2);
        else if (direction == 'L')
            node = (node.Item1 - steps, node.Item2);
        else if (direction == 'D')
            node = (node.Item1, node.Item2 + steps);
        else
            node = (node.Item1, node.Item2 - steps);

        nodes.Add(node);

        sumOfSteps += steps;
    }

    /*
     The shoelace formula does not give you the number of interior points,
     it gives you the area of the polygon. Once you have the area of the
     polygon, and you have counted the number of boundary points of the
     polygon, you can use the equation from Pick's theorem to solve
     for the number of interior points.
     
     Thanks to:
     https://www.reddit.com/r/adventofcode/comments/18l0qtr/comment/kecif1x/
     */
    long area = 0;
    for (int j = 0; j < nodes.Count - 1; j++)
        area += Shoelace(nodes[j].Item1, nodes[j].Item2, nodes[j + 1].Item1, nodes[j + 1].Item2);
    area = Math.Abs(area);

    Console.WriteLine($"{(area + sumOfSteps) / 2 + 1}");

    //throw new NotImplementedException();
}


static long Shoelace(long x1, long y1, long x2, long y2)
{
    return x1 * y2 - x2 * y1;
}