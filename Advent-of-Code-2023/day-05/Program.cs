namespace day_05
{
    internal class Program
    {
        private static List<Plant> seedsPartOne = new();
        private static List<Tuple<long, long, long>> seed_to_soil_map = new();
        private static List<Tuple<long, long, long>> soil_to_fert_map = new();
        private static List<Tuple<long, long, long>> fert_to_water_map = new();
        private static List<Tuple<long, long, long>> water_to_light_map = new();
        private static List<Tuple<long, long, long>> light_to_temp_map = new();
        private static List<Tuple<long, long, long>> temp_to_hum_map = new();
        private static List<Tuple<long, long, long>> hum_to_loc_map = new();
        
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            CreateMap(input);

            Console.WriteLine("########## Day 5 2025 ##########");
            Console.WriteLine($"Part one solution: {Solve(seedsPartOne)}");
            Console.WriteLine("################################");
        }

        private static void CreateMap(string[] input)
        {
            var seds = Array.ConvertAll(input[0].Split(':')[1].Split(' ').Where(x=> x!= "").ToArray(), s=>Int64.Parse(s));

            foreach(var kvp in seds) 
            {
                seedsPartOne.Add(new Plant() { Seed = kvp});
            }

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

        private static long Solve(List<Plant> seedsList)
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
                        //set soil to object
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
                        //set soil to object
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

            foreach (var t in seedsList) 
            {
                //Console.WriteLine(t.ToString());
            }

            return seedsList.Select(x => x.Location).Min();
        }
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

        public override string ToString()
        {
            return $"Seed: {Seed}, soil {Soil}, fert {Fert}, Water {Water}, Light {Light}, Temp {Temp}, Hum {Humidity}, Loc {Location}";
        }
    }
}
