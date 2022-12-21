using System.Text.RegularExpressions;

namespace day_21
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine("########## Day 21 2022 ##########");
            Console.WriteLine($"Part one solution: {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution: {SolvePartTwo(input)}");
            Console.WriteLine("#################################");
        }

        private static object SolvePartOne(string[] input)
        {
            Dictionary<string, string> keyValuePairs = ProccessEveryting(input, true);

            return keyValuePairs["root"];
        }

        private static Dictionary<string, string> ProccessEveryting(string[] input, bool isPartOne)
        {
            Dictionary<string, long> keyValuePairs = new Dictionary<string, long>();
            Dictionary<string, string> keyValueUnreloved = new Dictionary<string, string>();

            foreach (string line in input)
            {
                if (!isPartOne) 
                {
                    if (line.StartsWith("humn"))
                    {
                        keyValueUnreloved.Add("humn", "");
                        continue;
                    }
                }

                bool containsInt = line.Any(char.IsDigit);
                if (containsInt)
                {
                    string name = line.Split(':')[0];
                    int value = Int32.Parse(line.Split(':')[1].Trim());
                    keyValuePairs.Add(name, value);
                }
                else
                {
                    string name = line.Split(':')[0];
                    string command = line.Split(':')[1].Trim();
                    keyValueUnreloved.Add(name, command);
                }
            }

            while (true)
            {
                Dictionary<string, long> keyValuePairsCopy = new Dictionary<string, long>();
                bool isChanged = false;
                foreach (string key in keyValuePairs.Keys)
                {
                    while (keyValueUnreloved.Values.Any(x => x.Contains(key)))
                    {
                        string keyCopy = keyValueUnreloved.FirstOrDefault(x => x.Value.Contains(key)).Key;
                        string value = keyValueUnreloved[keyCopy];
                        value = value.Replace(key, keyValuePairs[key].ToString());
                        isChanged = true;
                        if (ValueCanBeOperated(value))
                        {
                            long newValue = SetNewValue(value);
                            keyValuePairsCopy.Add(keyCopy, newValue);
                            keyValueUnreloved.Remove(keyCopy);

                            break;
                        }
                        else
                        {
                            keyValueUnreloved[keyCopy] = value;
                        }
                    }
                }

                foreach (var pait in keyValuePairsCopy)
                {
                    keyValuePairs.Add(pait.Key, pait.Value);
                }

                if (keyValueUnreloved.Count == 0 || (!isPartOne && !isChanged)) 
                {
                    if (isPartOne) 
                    {
                        keyValueUnreloved.Add("root", keyValuePairs["root"].ToString());
                    }

                    break;
                }
            }

            return keyValueUnreloved;
        }

        private static long SetNewValue(string value)
        {
            string[] values = value.Split(' ');

            long r1;
            Int64.TryParse(values[0], out r1);

            long r2;
            Int64.TryParse(values[2], out r2);

            if (values[1] == "+")
            {
                return r1 + r2;
            }
            else if (values[1] == "-")
            {
                return r1 - r2;
            }
            else if (values[1] == "*")
            {
                return r1 * r2;
            }
            else
            {
                return r1 / r2;
            }
        }

        private static bool ValueCanBeOperated(string value)
        {
            string[] values = value.Split(' ');

            long r1;
            if (!Int64.TryParse(values[0], out r1))
                return false;

            long r2;
            if (!Int64.TryParse(values[2], out r2))
                return false;

            return true;
        }

        private static object SolvePartTwo(string[] input)
        {
            Dictionary<string, string> keyValueUnreloved = ProccessEveryting(input, false);

            string regex = @"\w[a-z]+";
            string toProcess = keyValueUnreloved["root"].Split(" ")[0];

            long final = 0;
            foreach (var elements in keyValueUnreloved["root"].Split(" "))
            {
                if (elements.All(char.IsDigit))
                {
                    final = Int64.Parse(elements);
                }

                if (elements.All(char.IsLetter)) 
                {
                    toProcess = elements;
                }
            }

            while (true)
            {
                Match match = Regex.Match(toProcess, regex);
                foreach (var t in match.Groups)
                {
                    string key = t.ToString();
                    if (key.All(char.IsDigit))
                    {
                        continue;
                    }

                    string valueToBeProcessed = keyValueUnreloved[key];
                    keyValueUnreloved.Remove(key);
                    toProcess = toProcess.Replace(key, "( " + valueToBeProcessed + " )");
                }

                if (keyValueUnreloved.Count == 2) 
                {
                    break;
                }
            }

            bool isItAtLeft = true;
            bool itIsEnd = false;
            while (!itIsEnd)
            {
                toProcess = toProcess.Substring(1, toProcess.Length - 2).Trim();

                string number = "";
                string operation = "";
                var splitProccess = toProcess.Split(" ");
                if (toProcess.StartsWith("("))
                {
                    
                    number = splitProccess[splitProccess.Length - 1];
                    operation = splitProccess[splitProccess.Length - 2];

                    splitProccess[splitProccess.Length - 1] = "";
                    splitProccess[splitProccess.Length - 2] = "";
                }
                else if (toProcess.StartsWith("h"))
                {
                    operation = toProcess.Split(' ')[1];
                    number = toProcess.Split(' ')[2];

                    itIsEnd = true;
                }
                else
                {
                    number = splitProccess[0];
                    operation = splitProccess[1];

                    splitProccess[0] = "";
                    splitProccess[1] = "";
                    isItAtLeft = false;
                }

                toProcess = String.Join(" ", splitProccess).Trim();

                final = Calculate(final, number, operation, isItAtLeft);
                isItAtLeft = true;
            }

            return final;
        }

        private static long Calculate(long final, string number, string operation, bool isItAtLeft)
        {
            if (operation == "*")
            {
                return final / Int64.Parse(number);
            }
            else if (operation == "/")
            {
                if (isItAtLeft)
                {
                    return final * Int64.Parse(number);
                }
                else 
                {
                    return Int64.Parse(number) / final;
                }
            }
            else if (operation == "+")
            {
                return final - Int64.Parse(number);
            }
            else
            {
                if (isItAtLeft) 
                {
                    return final + Int64.Parse(number);
                }
                else
                {
                    return Int64.Parse(number) - final;
                }
            }
        }
    }
}
