using System.Text.RegularExpressions;

namespace day_04
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            Console.WriteLine("########## Day 4 2018 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("################################");
        }

        private static int SolvePartOne(string[] input)
        {
            Dictionary<int, Dictionary<int, int>> schedule = GetSleepSchedule(input);

            int sum = 0;
            int guardId = 0;
            foreach (var keyValuePair in schedule) 
            {
                var tempSum = schedule[keyValuePair.Key].Sum(x => x.Value);
                if (tempSum > sum) 
                {
                    sum = tempSum;
                    guardId = keyValuePair.Key;
                }
            }

            var selectedMinute = schedule[guardId].Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            return guardId * selectedMinute;
        }

        private static List<Action> SortDateTime(string[] input)
        {
            List<Action> actions = new List<Action>();
            string regexDateTime = @"(?<dateTime>([0-9]{4}-[0-9]{2}-[0-9]{2}) ([0-9]{2}:[0-9]{2}))\] (?<action>.*)";
            foreach (string line in input)
            {
                GroupCollection groups = Regex.Match(line, regexDateTime).Groups;
                actions.Add(new Action()
                {
                    ActionDateTime = Convert.ToDateTime(groups["dateTime"].Value),
                    ActionType = groups["action"].Value
                });
            }

            return actions.OrderBy(x => x.ActionDateTime).ToList();
        }

        private static object SolvePartTwo(string[] input)
        {
            Dictionary<int, Dictionary<int, int>> schedule = GetSleepSchedule(input);
            var max1 = 0;
            var max2 = 0;
            int guardId = 0;
            foreach (var keyValuePair in schedule)
            {
                var tmax1 = schedule[keyValuePair.Key].Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                var tmax2 = schedule[keyValuePair.Key].Aggregate((l, r) => l.Value > r.Value ? l : r).Value;

                if (tmax2 > max2)
                {
                    guardId = keyValuePair.Key;
                    max1 = tmax1;
                    max2 = tmax2;
                }
            }

            return max1 * guardId;
        }

        private static Dictionary<int, Dictionary<int, int>> GetSleepSchedule(string[] input)
        {
            int guardId;
            Dictionary<int, Dictionary<int, int>> schedule = new Dictionary<int, Dictionary<int, int>>();
            List<Action> actions = SortDateTime(input);
            string regexGuard = @"Guard #(\d+)";
            guardId = 0;
            DateTime fallsAsleap = new DateTime();
            foreach (Action current in actions)
            {
                if (current.ActionType.Contains("Guard"))
                {
                    Match mm = Regex.Match(current.ActionType, regexGuard);
                    guardId = Int32.Parse(mm.Groups[1].Value);
                    //Console.WriteLine(current.ActionType);
                    continue;
                }

                if (current.ActionType.Contains("fall"))
                {
                    fallsAsleap = current.ActionDateTime;
                }

                if (current.ActionType.Contains("wakes"))
                {
                    DateTime wakesUp = current.ActionDateTime;
                    TimeSpan ts = wakesUp - fallsAsleap;
                    IEnumerable<int> squares = Enumerable.Range(fallsAsleap.Minute, wakesUp.Minute - fallsAsleap.Minute).ToList();
                    if (schedule.ContainsKey(guardId))
                    {
                        foreach (int sq in squares)
                        {
                            if (schedule[guardId].ContainsKey(sq))
                            {
                                schedule[guardId][sq]++;
                            }
                            else
                            {
                                schedule[guardId].Add(sq, 1);
                            }
                        }
                    }
                    else
                    {
                        schedule.Add(guardId, new Dictionary<int, int>());
                        foreach (int sq in squares)
                        {
                            schedule[guardId].Add(sq, 1);
                        }
                    }
                }
            }

            return schedule;
        }
    }

    class Action 
    {
        public DateTime ActionDateTime { get; set; }
        public string ActionType { get; set; }

        public override string ToString()
        {
            return $"{ActionDateTime} : {ActionType}";
        }
    }
}
