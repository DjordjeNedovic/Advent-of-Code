string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

var times = Array.ConvertAll(input[0].Split(':')[1].Split(' ').Where(x => x != "").ToArray(), s => Int64.Parse(s));
var distances = Array.ConvertAll(input[1].Split(':')[1].Split(' ').Where(x => x != "").ToArray(), s => Int64.Parse(s));

var time = Int64.Parse(String.Join("", input[0].Split(':')[1].Split(' ').Where(x => x != "").ToArray()));
var distance = Int64.Parse(String.Join("", input[1].Split(':')[1].Split(' ').Where(x => x != "").ToArray()));

Console.WriteLine("########## Day 4 2023 ##########");
Console.WriteLine($"Part one solution: {Solve(times, distances)}");
Console.WriteLine($"Part two solution: {Solve(new long[1] { time }, new long[1] { distance })}");
Console.WriteLine("################################");

object Solve(long[] times, long[] distances)
{
    long result = 0;

    for (int i = 0; i < times.Length; i++) 
    {
        long raceTime = times[i];
        long raceDistance = distances[i];
        long counter = 0;
        for (int msPressed = 1; msPressed <= raceTime; msPressed++) 
        {
            long speed = msPressed;
            long distance = speed * (raceTime - msPressed);

            if (distance > raceDistance) 
            {
                counter++;
            }
        }

        if (result == 0)
        {
            result = counter;
        }
        else 
        {
            result *= counter;
        }
    }

    return result;
}