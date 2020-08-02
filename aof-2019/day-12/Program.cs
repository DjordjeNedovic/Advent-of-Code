using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_12
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] positionsOfPlanets = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzleInput.txt"));
            List<Moon> moons = new List<Moon>();
            foreach (string positionOfPlanet in positionsOfPlanets) 
            {
                var trimedPosition = positionOfPlanet.Replace("<","").Replace(">","");
                Moon moon = new Moon();
                string[] cordinates = trimedPosition.Split(',');
                string[] valueX = cordinates[0].Split('=');
                moon.PositionX = Int32.Parse(valueX[1].Trim());

                string[] valueY = cordinates[1].Split('=');
                moon.PositionY = Int32.Parse(valueY[1].Trim());

                string[] valueZ = cordinates[2].Split('=');
                moon.PositionZ = Int32.Parse(valueZ[1].Trim());

                moon.VelocityX = 0;
                moon.VelocityY = 0;
                moon.VelocityZ = 0;

                moon.PotentialEnergy = 0;
                moon.KineticEnergy = 0;

                moons.Add(moon);
            }

            Console.WriteLine("Map read");
            Console.WriteLine("Please insert number of steps for part one ( insert 0 to skip):");
            int numberOfSteps = Int32.Parse(Console.ReadLine());
            if (numberOfSteps != 0)
            {
                Console.WriteLine("################### PART ONE ###################");
                moons.ForEach(x => Console.WriteLine(x));
                SolvePartOne(moons, numberOfSteps);
            }
            else 
            {
                moons.ForEach(x => { x.InitialState =$"{x.PositionX}, {x.PositionY}, {x.PositionZ}"; x.FoundedLCM = false; });
                SolvePartTwo(moons);
            }
        }

        private static void SolvePartOne(List<Moon> moons, int numberOfSteps)
        {
            for (int step = 0; step < numberOfSteps; step++) 
            {
                Console.WriteLine();
                Console.WriteLine($"Applying step {step +1 }");
                foreach (Moon targetMoon in moons) 
                {
                    foreach (Moon oterMoon in moons.Where(x => x != targetMoon)) 
                    {
                        if(targetMoon.PositionX != oterMoon.PositionX)
                            _ = targetMoon.PositionX < oterMoon.PositionX ? targetMoon.VelocityX += 1 : targetMoon.VelocityX -= 1;

                        if (targetMoon.PositionY != oterMoon.PositionY)
                            _ = targetMoon.PositionY < oterMoon.PositionY ? targetMoon.VelocityY += 1 : targetMoon.VelocityY -= 1;

                        if (targetMoon.PositionZ != oterMoon.PositionZ)
                            _ = targetMoon.PositionZ < oterMoon.PositionZ ? targetMoon.VelocityZ += 1 : targetMoon.VelocityZ -= 1;
                    }
                }

                moons.ForEach(x => { x.PositionX += x.VelocityX; x.PositionY += x.VelocityY; x.PositionZ += x.VelocityZ; });
            }

            Console.WriteLine();
            Console.WriteLine($"Done {numberOfSteps} steps");

            moons.ForEach(x=> { x.PotentialEnergy = Math.Abs(x.PositionX) + Math.Abs(x.PositionY) + Math.Abs(x.PositionZ);
                                x.KineticEnergy = Math.Abs(x.VelocityX) + Math.Abs(x.VelocityY) + Math.Abs(x.VelocityZ); 
                                x.TotalEnergy = x.PotentialEnergy * x.KineticEnergy; 
                               });

            moons.ForEach(x => Console.WriteLine(x.ReturnEnergy()));
            Console.WriteLine();
            Console.WriteLine($"Sum of total energy is {moons.Sum(x=>x.TotalEnergy)}");
        }

        private static void SolvePartTwo(List<Moon> moons)
        {
            int steps = 0;
            while (true)
            { 
                Console.WriteLine();
                Console.WriteLine($"Applying step { ++steps }");
                foreach (Moon targetMoon in moons)
                {
                    foreach (Moon oterMoon in moons.Where(x => x != targetMoon))
                    {
                        if (targetMoon.PositionX != oterMoon.PositionX)
                            _ = targetMoon.PositionX < oterMoon.PositionX ? targetMoon.VelocityX += 1 : targetMoon.VelocityX -= 1;

                        if (targetMoon.PositionY != oterMoon.PositionY)
                            _ = targetMoon.PositionY < oterMoon.PositionY ? targetMoon.VelocityY += 1 : targetMoon.VelocityY -= 1;

                        if (targetMoon.PositionZ != oterMoon.PositionZ)
                            _ = targetMoon.PositionZ < oterMoon.PositionZ ? targetMoon.VelocityZ += 1 : targetMoon.VelocityZ -= 1;
                    }
                }

                moons.ForEach(x => { x.PositionX += x.VelocityX; x.PositionY += x.VelocityY; x.PositionZ += x.VelocityZ; });

                foreach (Moon m in moons) 
                {
                    if (!m.FoundedLCM) 
                    {
                        m.CompareToInitialState(steps);
                    }
                }

                if (moons.TrueForAll(x => x.FoundedLCM))
                    break;
            }


            moons.ForEach(x=>Console.WriteLine(x));
            //Console.WriteLine("TT");
        }
    }

    class Moon
    {
        internal int PositionX { get; set; }
        internal int PositionY { get; set; }
        internal int PositionZ { get; set; }
        internal int VelocityX { get; set; }
        internal int VelocityY { get; set; }
        internal int VelocityZ { get; set; }
        internal int PotentialEnergy { get; set; }
        internal int KineticEnergy { get; set; }
        internal int TotalEnergy { get; set; }
        internal string InitialState { get; set; }
        internal int LCM = 0;
        internal bool FoundedLCM = false;

        public override string ToString()
        {
            return $"pos=<x= { PositionX }, y= { PositionY }, z= { PositionZ }>, vel=<x= { VelocityX }, y= { VelocityY }, z= { VelocityZ }>, LCM: {LCM}";
        }

        public string ReturnEnergy() 
        {
            return $"pot: {Math.Abs(PositionX)} + {Math.Abs(PositionY)} + {Math.Abs(PositionZ)};  kin: {Math.Abs(VelocityX)} + {Math.Abs(VelocityY)} + {Math.Abs(VelocityZ)}; total: {PotentialEnergy} * {KineticEnergy} = {TotalEnergy}";    
        }

        internal void CompareToInitialState(int number)
        {
            if (this.InitialState.Equals($"{this.PositionX}, {this.PositionY}, {this.PositionZ}")) 
            {
                this.LCM = number;
                this.FoundedLCM = true;
            }
        }
    }
}