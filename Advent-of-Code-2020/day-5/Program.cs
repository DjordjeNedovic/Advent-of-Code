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
            foreach (string i in input) 
            {
                int MIN_COLUMN = 0;
                int MAX_COLUMN = 127;
                int MIN_ROW = 0;
                int MAX_ROW = 7;
                var arrayOfCommands = i.ToCharArray();
                foreach (char command in arrayOfCommands) 
                {
                    switch(command){
                        case 'F':
                            MAX_COLUMN = (MAX_COLUMN + MIN_COLUMN) / 2;
                            break;
                        case 'B':
                            MIN_COLUMN =(int) Math.Ceiling((decimal)(MAX_COLUMN + MIN_COLUMN) / 2);
                            break;
                        case 'R':
                            MIN_ROW = (int)Math.Ceiling((decimal)(MAX_ROW + MIN_ROW) / 2);
                            break;
                        case 'L':
                            MAX_ROW = (MAX_ROW + MIN_ROW) / 2;
                            break;
                        default:
                            break;
                    }
                }

                Seat seat = new Seat() { Column = MAX_COLUMN, Row = MAX_ROW, Index = MAX_COLUMN * 8 + MAX_ROW};
                seats.Add(seat);
            }

            Console.WriteLine($"Part one solution: {seats.Max(x => x.Index)}");
            Seat seatBeforeMy = seats.OrderBy(x => x.Index).ToList().Where(x=> !seats.Exists(z=>z.Index == x.Index+1)).First();
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