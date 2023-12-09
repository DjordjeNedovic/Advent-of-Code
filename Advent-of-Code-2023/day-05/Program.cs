List<Tuple<long, long, long>> seed_to_soil_map = new();
List<Tuple<long, long, long>> soil_to_fert_map = new();
List<Tuple<long, long, long>> fert_to_water_map = new();
List<Tuple<long, long, long>> water_to_light_map = new();
List<Tuple<long, long, long>> light_to_temp_map = new();
List<Tuple<long, long, long>> temp_to_hum_map = new();
List<Tuple<long, long, long>> hum_to_loc_map = new();

string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

CreateMap(input);

//In release mode, on i7 (11 gen) and 16gb of ram
//it will take 5 sec to do this

Console.WriteLine("########## Day 5 2025 ##########");
Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
Console.WriteLine("################################");

long SolvePartOne(string[] input)
{
    var seds = Array.ConvertAll(input[0].Split(':')[1].Split(' ').Where(x => x != "").ToArray(), s => Int64.Parse(s));
    List<Plant> seedsPartOne = new();

    foreach (var kvp in seds)
    {
        seedsPartOne.Add(new Plant() { Seed = kvp });
    }

    return Solve(seedsPartOne);
}

long SolvePartTwo(string[] input)
{
    //THANKS TO REDDIT
    //data is somehow grouped in a sequence
    //idea is to read every 1000th seed number in range
    //and find a range with a smallest Location for every 1000th seed number
    var seedsRanges = Array.ConvertAll(input[0].Split(':')[1].Split(' ').Where(x => x != "").ToArray(), s => Int64.Parse(s));

    long minLocationGeneral = 0;
    long minLocationRange = 0;

    List<Plant> plants = new List<Plant>();
    for (long i = 0; i < seedsRanges.Length / 2; i++)
    {
        long start = seedsRanges[i * 2];
        long end = seedsRanges[i * 2 + 1];

        for (long current = start; current < start + end - 1; current += 1000)
        {
            plants.Add(new Plant() { Seed = current });
        }

        var minLocationThatIsFoundInThisRange = Solve(plants);

        if (minLocationGeneral == 0 || minLocationThatIsFoundInThisRange < minLocationGeneral)
        {
            minLocationGeneral = minLocationThatIsFoundInThisRange;
            minLocationRange = i;
        }
    }

    //when you find range that contains seed with smallest number
    //do previous step againg (read every 1000the seed)
    // find min location and find that seed number
    plants = new List<Plant>();
    long minStart = seedsRanges[minLocationRange * 2];
    long minEnd = seedsRanges[minLocationRange * 2 + 1];
    for (long current = minStart; current < minStart + minEnd - 1; current += 1000)
    {
        plants.Add(new Plant() { Seed = current });
    }

    var resultOfComputation = Solve(plants);

    //when you find seed number
    //take every number between +1000 and -1000 of the seed number
    var platObjectWithSmallestLocation = plants.Where(x => x.Location == minLocationGeneral).First();

    minStart = platObjectWithSmallestLocation.Seed - 1000;
    minEnd = platObjectWithSmallestLocation.Seed + 1000;
    plants = new List<Plant>();

    for (long current = minStart; current < minEnd; current += 1)
    {
        plants.Add(new Plant() { Seed = current });
    }

    //iterate throught all of them
    //get smallest location
    resultOfComputation = Solve(plants);

    //i belive that this can be done in cleanr manner
    //but i am to tired right now
        
    return resultOfComputation;
}

void CreateMap(string[] input)
{
    string state = "";
    for (int i = 1; i < input.Length; i++) 
    {
        if (input[i] == "")
            continue;

        string line = input[i];

        if (line.Contains("-to-")) 
        {
            state = line;
            continue;
        }

        var values = Array.ConvertAll(line.Split(' ').Where(x=> x!="").ToArray(), s => Int64.Parse(s));
        if (state.Contains("seed-to-soil")) 
        {
            seed_to_soil_map.Add(new Tuple<long, long, long>(values[0], values[1], values[2]));
        }
        else if (state.Contains("soil-to-fertilizer"))
        {
            soil_to_fert_map.Add(new Tuple<long, long, long>(values[0], values[1], values[2]));
        }
        else if (state.Contains("fertilizer-to-water"))
        {
            fert_to_water_map.Add(new Tuple<long, long, long>(values[0], values[1], values[2]));
        }
        else if (state.Contains("water-to-light"))
        {
            water_to_light_map.Add(new Tuple<long, long, long>(values[0], values[1], values[2]));
        }
        else if (state.Contains("light-to-temperature"))
        {
            light_to_temp_map.Add(new Tuple<long, long, long>(values[0], values[1], values[2]));
        }
        else if (state.Contains("temperature-to-humidity"))
        {
            temp_to_hum_map.Add(new Tuple<long, long, long>(values[0], values[1], values[2]));
        }
        else if (state.Contains("humidity-to-location"))
        {
            hum_to_loc_map.Add(new Tuple<long, long, long>(values[0], values[1], values[2]));
        }
    }
    }

long Solve(List<Plant> seedsList)
{
    foreach (var currentSeed in seedsList) 
    {
        foreach (var map in seed_to_soil_map) 
        {
            if (map.Item2 <= currentSeed.Seed && currentSeed.Seed <= (map.Item2 + map.Item3 - 1))
            {
                currentSeed.Soil = currentSeed.Seed + (map.Item1 - map.Item2);
                break;
            }
        }

        if (currentSeed.Soil == 0) 
        {
            currentSeed.Soil = currentSeed.Seed;
        }
            
        foreach (var map in soil_to_fert_map)
        {
            if (map.Item2 <= currentSeed.Soil && currentSeed.Soil <= (map.Item2 + map.Item3 - 1))
            {
                currentSeed.Fert = currentSeed.Soil + (map.Item1 - map.Item2);
                break;
            }
        }

        if (currentSeed.Fert == 0)
        {
            currentSeed.Fert = currentSeed.Soil;
        }

        foreach (var map in fert_to_water_map)
        {
            if (map.Item2 <= currentSeed.Fert && currentSeed.Fert <= (map.Item2 + map.Item3 - 1))
            {
                currentSeed.Water = currentSeed.Fert + (map.Item1 - map.Item2);
                break;
            }
        }

        if (currentSeed.Water == 0)
        {
            currentSeed.Water = currentSeed.Fert;
        }

        foreach (var map in water_to_light_map)
        {
            if (map.Item2 <= currentSeed.Water && currentSeed.Water <= (map.Item2 + map.Item3 - 1))
            {
                currentSeed.Light = currentSeed.Water + (map.Item1 - map.Item2);
                break;
            }
        }

        if (currentSeed.Light == 0)
        {
            currentSeed.Light = currentSeed.Water;
        }

           
        foreach (var map in light_to_temp_map)
        {
            if (map.Item2 <= currentSeed.Light && currentSeed.Light <= (map.Item2 + map.Item3 - 1))
            {
                currentSeed.Temp = currentSeed.Light + (map.Item1 - map.Item2);
                break;
            }
        }

        if (currentSeed.Temp == 0)
        {
            currentSeed.Temp = currentSeed.Light;
        }

        foreach (var map in temp_to_hum_map)
        {
            if (map.Item2 <= currentSeed.Temp && currentSeed.Temp <= (map.Item2 + map.Item3 - 1))
            {
                currentSeed.Humidity = currentSeed.Temp + (map.Item1 - map.Item2);
                break;
            }
        }

        if (currentSeed.Humidity == 0)
        {
            currentSeed.Humidity = currentSeed.Temp;
        }

        foreach (var map in hum_to_loc_map)
        {
            if (map.Item2 <= currentSeed.Humidity && currentSeed.Humidity <= (map.Item2 + map.Item3 - 1))
            {
                currentSeed.Location = currentSeed.Humidity + (map.Item1 - map.Item2);
                break;
            }
        }

        if (currentSeed.Location == 0)
        {
            currentSeed.Location = currentSeed.Humidity;
        }
    }

    return seedsList.Select(x => x.Location).Min();
}

class Plant 
{
    public long Seed { get; set; }
    public long Soil { get; set; }
    public long Fert { get; set; }
    public long Water { get; set; }
    public long Light { get; set; }
    public long Temp { get; set; }
    public long Humidity { get; set; }
    public long Location { get; set; }
}
