using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Seat> seats = new List<Seat>();
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            foreach (string row in input) 
            {
                int COLUMN_LOWER_LIMIT = 0;
                int COLUMN_UPPER_LIMIT = 127;
                int ROW_LOWER_LIMIT = 0;
                int ROW_UPPER_LIMIT = 7;
                var arrayOfCommands = row.ToCharArray();
                foreach (char command in arrayOfCommands) 
                {
                    switch(command){
                        case 'F':
                            COLUMN_UPPER_LIMIT = (COLUMN_UPPER_LIMIT + COLUMN_LOWER_LIMIT) / 2;
                            break;
                        case 'B':
                            COLUMN_LOWER_LIMIT =(int) Math.Ceiling((decimal)(COLUMN_UPPER_LIMIT + COLUMN_LOWER_LIMIT) / 2);
                            break;
                        case 'R':
                            ROW_LOWER_LIMIT = (int)Math.Ceiling((decimal)(ROW_UPPER_LIMIT + ROW_LOWER_LIMIT) / 2);
                            break;
                        case 'L':
                            ROW_UPPER_LIMIT = (ROW_UPPER_LIMIT + ROW_LOWER_LIMIT) / 2;
                            break;
                        default:
                            break;
                    }
                }

                Seat seat = new Seat() { Column = COLUMN_UPPER_LIMIT, Row = ROW_UPPER_LIMIT, Index = COLUMN_UPPER_LIMIT * 8 + ROW_UPPER_LIMIT};
                seats.Add(seat);
            }

            Console.WriteLine($"Part one solution: {seats.Max(seat => seat.Index)}");
            Seat seatBeforeMy = seats.OrderBy(seat => seat.Index).ToList().Where(seat=> !seats.Exists(tempSeat=>tempSeat.Index == seat.Index + 1)).First();
            Console.WriteLine($"Part two solution: {seatBeforeMy.Index + 1}");
        }
    }

    class Seat 
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public int Index { get; set; }
    }
}