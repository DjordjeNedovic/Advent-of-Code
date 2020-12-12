using System;
using System.IO;
using System.Text.RegularExpressions;

namespace day_12
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));

            Console.WriteLine($"Part one solution:  {SolvePartOne(input)}");
            Console.WriteLine($"Part two solution:  {SolvePartTwo(input)}");
        }

        private static int SolvePartOne(string[] input)
        {
            Direction shipFacingDirection = Direction.EAST;
            int shipXAsixValue = 0;
            int shipYAsixValue = 0;
            string regex = @"(?<command>\w)(?<value>\d+)";
            foreach (string command in input)
            {
                Match match = Regex.Match(command, regex);
                string direction = match.Groups["command"].Value;
                int value = Int32.Parse(match.Groups["value"].Value);
                switch (direction) 
                {
                    case "N":
                        shipYAsixValue += value;
                        break;
                    case "S":
                        shipYAsixValue -= value;
                        break;
                    case "E":
                        shipXAsixValue += value;
                        break;
                    case "W":
                        shipXAsixValue -= value;
                        break;
                    case "L":
                        shipFacingDirection = GetNewDirection(shipFacingDirection, -value);
                        break;
                    case "R":
                        shipFacingDirection = GetNewDirection(shipFacingDirection, value);
                        break;
                    case "F":
                        switch (shipFacingDirection) 
                        {
                            case (Direction.NORTH):
                                shipYAsixValue += value;
                                break;
                            case (Direction.EAST):
                                shipXAsixValue += value;
                                break;
                            case (Direction.SOUTH):
                               shipYAsixValue -= value;
                                break;
                            case (Direction.WEST):
                                shipXAsixValue -= value;
                                break;
                            default:
                                throw new NotImplementedException("Something is really wrong.");
                        }
                        break;
                    default:
                        throw new NotImplementedException("Something is really wrong.");
                }
            }

            return Math.Abs(shipXAsixValue) + Math.Abs(shipYAsixValue);
        }

        private static object SolvePartTwo(string[] input)
        {
            int waypointX = 10;
            int waypointY = 1;
            int shipXAsixValue = 0;
            int shipYAsixValue = 0;
            string regex = @"(?<command>\w)(?<value>\d+)";
            foreach (string command in input) 
            {
                Match match = Regex.Match(command, regex);
                string direction = match.Groups["command"].Value;
                int value = Int32.Parse(match.Groups["value"].Value);
                Console.WriteLine($"{shipXAsixValue} : {shipYAsixValue} ({waypointX}, {waypointY})");
                switch (direction)
                {
                    case "N":
                        waypointY += value;
                        break;
                    case "S":
                        waypointY -= value;
                        break;
                    case "E":
                        waypointX += value;
                        break;
                    case "W":
                        waypointX -= value;
                        break;
                    case "L":
                        for (int i = 0; i < value / 90; i++)
                        {
                            //each step counter-clockwise chage X to -Y and Y to X
                            //e.g X = 4 Y = 10 ===> X= - 10 Y = 4 
                            int oldWaypointX = waypointX;
                            waypointX = -waypointY;
                            waypointY = oldWaypointX;
                        }

                        break;
                    case "R":
                        for (int i = 0; i < value / 90; i++)
                        {
                            //each step counter-clockwise chage X to Y and Y to -X
                            //e.g X = 4 Y = 10 ===> X = 10 Y = -4 
                            int oldWaypointX = waypointX;
                            waypointX = waypointY;
                            waypointY = -oldWaypointX;
                        }

                        break;
                    case "F":
                        shipXAsixValue += waypointX * value;
                        shipYAsixValue += waypointY * value;
                        break;
                    default:
                        throw new NotImplementedException("Something is really wrong.");
                }
            }

            return Math.Abs(shipXAsixValue) + Math.Abs(shipYAsixValue);
        }


        private static Direction GetNewDirection(Direction shipMovingDirection, int value)
        {
            int enumValue = (int)shipMovingDirection;
            int steps = value / 90;

            //here I put on my robe and wizard hat...
            if (steps > 0)
            {
                return (Direction)((enumValue + steps) % 4);
            }
            else
            {
                return (Direction)((4 + (enumValue + steps)) % 4);
            }
        }
    }

    enum Direction 
    {
        NORTH = 0,
        EAST = 1,
        SOUTH = 2,
        WEST = 3
    }
}