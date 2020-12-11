using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace day_11
{
    class Program
    {
        static int counter = 1;
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            List<Seat> seatsPartOne = new List<Seat>();
            List<Seat> seatsPartTwo = new List<Seat>();
            for (int row = 0; row < input.Length; row++)
            {
                for (int column = 0; column < input[0].Length; column++)
                {
                    if (input[row][column].ToString() != ".") 
                    {
                        seatsPartOne.Add(new Seat() { X = column, Y = row, Value = "#", ShouldBeChanged=false, LastTimeChanged = counter});
                        seatsPartTwo.Add(new Seat() { X = column, Y = row, Value = "#", ShouldBeChanged=false, LastTimeChanged = counter});
                    }
                }
            }

            Console.WriteLine($"Part one solution:  { SolvePartOne(seatsPartOne, 4)}");
            counter = 0;
            Console.WriteLine($"Part two solution:  {SolvePartOne(seatsPartTwo, 5)}");
        }

        private static int SolvePartOne(List<Seat> seats, int tolerance)
        {
            counter++;
            foreach (Seat seat in seats) 
            {
                if (counter - seat.LastTimeChanged < 5) 
                {
                    bool ChageState = ChecknNighbors(seat, seats, tolerance);
                    seat.ShouldBeChanged = ChageState;
                }
            }
            
            if (seats.Any(x => x.ShouldBeChanged == true))
            {
                seats.Where(x => x.ShouldBeChanged == true).ToList().ForEach(x => { x.Value = (x.Value == "#" ? "L" : "#"); x.ShouldBeChanged = false; x.LastTimeChanged = counter; } );
                return SolvePartOne(seats, tolerance);
            }
            else 
            {
                return seats.Where(x => x.Value == "#").Count();
            }
        }

        private static bool ChecknNighbors(Seat seat, List<Seat> seats, int tolerance)
        {
            int result = 0;
            //left
            result += FoundOccupied(seat.X, seat.Y, -1, 0, seats, (tolerance == 4));
            //right
            result += FoundOccupied(seat.X, seat.Y, 1, 0, seats, (tolerance == 4));
            //down
            result += FoundOccupied(seat.X, seat.Y, 0, 1, seats, (tolerance == 4));
            //up
            result += FoundOccupied(seat.X, seat.Y, 0, -1, seats, (tolerance == 4));
            // upleft
            result += FoundOccupied(seat.X, seat.Y, -1, -1, seats, (tolerance == 4));

            if (result <= tolerance) 
            {
                // up-right
                result += FoundOccupied(seat.X, seat.Y, 1, -1, seats, (tolerance == 4));
                
                // down-letf
                if (result <= tolerance) { 
                    result += FoundOccupied(seat.X, seat.Y, -1, 1, seats, (tolerance == 4));
                }

                // down-right
                if (result <= tolerance) { 
                    result += FoundOccupied(seat.X, seat.Y, 1, 1, seats, (tolerance == 4));

                }
            }

            if (seat.Value == "L" && result == 0)
            {
                return true;
            }
            else if (seat.Value == "#" && result >= tolerance)
            {
                return true;
            }

            return false;
        }

        private static int FoundOccupied(int x, int y, int deltaX, int deltaY, List<Seat> seats, bool isFirstPart)
        {
            var LimitC = seats.Select(x => x.X).Max();
            var LimitR = seats.Select(x => x.Y).Max();
            var tempC = x + deltaX;
            var tempR = y + deltaY;

            if (tempC < 0 || tempR < 0 || tempC > LimitC || tempR > LimitR)
            {
                return 0;
            }

            if (isFirstPart)
            {
                if (seats.Where(seat => seat.X == tempC && seat.Y == tempR && seat.Value == "#").FirstOrDefault() == null) 
                {
                    return 0;
                }

                return 1;
            }
            else
            {
                while (true) 
                {
                    var tempSeat = seats.Where(seat => seat.X == tempC && seat.Y == tempR).FirstOrDefault();
                    if (tempSeat != null)
                    {
                        if (tempSeat.Value == "#")
                        {
                            return 1;
                        }

                        break;
                    }

                    tempC += deltaX;
                    tempR += deltaY;

                    if (tempC < 0 || tempR < 0 || tempC > LimitC || tempR > LimitR) 
                    {
                        return 0;
                    }
                }

                return 0;
            }
        }
    }

    class Seat 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool ShouldBeChanged { get; set; }
        public string Value { get; set; }
        public int LastTimeChanged { get; set; }

        public override string ToString()
        {
            return $"x: { X }, y: { Y }, type: { Value }, toBeChanged: { ShouldBeChanged }, LastTimeChanged { LastTimeChanged }";
        }
    }
}