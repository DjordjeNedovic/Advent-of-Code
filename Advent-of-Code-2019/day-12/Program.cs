using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_12
{
    class Program
    {
        //not 38688586308044 
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
                moon.InitialX = Int32.Parse(valueX[1].Trim());

                string[] valueY = cordinates[1].Split('=');
                moon.PositionY = Int32.Parse(valueY[1].Trim());
                moon.InitialY = Int32.Parse(valueY[1].Trim());

                string[] valueZ = cordinates[2].Split('=');
                moon.PositionZ = Int32.Parse(valueZ[1].Trim());
                moon.InitialZ = Int32.Parse(valueZ[1].Trim());

                moon.VelocityX = 0;
                moon.VelocityY = 0;
                moon.VelocityZ = 0;

                moon.FoundedX = false;
                moon.FoundedY = false;
                moon.FoundedZ = false;

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
                Console.WriteLine("################### PART TWO ###################");
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
                ++steps; 
                //Console.WriteLine($"Applying step {  }");
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
                    if (!m.FoundedX) 
                    {
                        m.CompareToInitialState(steps, "X");
                    }

                    if (!m.FoundedY)
                    {
                        m.CompareToInitialState(steps, "Y");
                    }
                    if (!m.FoundedZ)
                    {
                        m.CompareToInitialState(steps, "Z");
                    }

                    if (m.FoundedX && m.FoundedY && m.FoundedZ) 
                    {
                        var tt = String.Empty;
                    }
                }

                if (moons.TrueForAll(x => x.FoundedX && x.FoundedY && x.FoundedZ)) 
                {
                    Console.WriteLine($"Applying step { steps }");
                    break;
                }
            }

            moons.ForEach(x=>Console.WriteLine(x));

            //result = Utility.LCM(p1, Utility.LCM(p2, Utility.LCM(p3, p4)));

            var maxAsixX = moons.Max(x => x.LcmX);
            var maxAsisY = moons.Max(x => x.LcmY);
            var maxAsixZ = moons.Max(x => x.LcmZ);

            decimal result = Utility.LCM(maxAsixX, Utility.LCM(maxAsisY, maxAsixZ));

            Console.WriteLine($"it takes {result} steps to reach the first state");
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

        internal int InitialX { get; set; }
        internal int InitialY { get; set; }
        internal int InitialZ { get; set; }

        public bool FoundedX { get; internal set; }
        public bool FoundedY { get; internal set; }
        public bool FoundedZ { get; internal set; }

        internal decimal LcmX = 0;
        internal decimal LcmY = 0;
        internal decimal LcmZ = 0;

        public override string ToString()
        {
            return $"pos=<x= { PositionX }, y= { PositionY }, z= { PositionZ }>, vel=<x= { VelocityX }, y= { VelocityY }, z= { VelocityZ }> \n" +
                   $"LcmX: {LcmX}, LcmY: {LcmY}, LcmZ: {LcmZ}\n";

        }

        public string ReturnEnergy() 
        {
            return $"pot: {Math.Abs(PositionX)} + {Math.Abs(PositionY)} + {Math.Abs(PositionZ)};  kin: {Math.Abs(VelocityX)} + {Math.Abs(VelocityY)} + {Math.Abs(VelocityZ)}; total: {PotentialEnergy} * {KineticEnergy} = {TotalEnergy}";    
        }

        internal void CompareToInitialState(int number, string asix)
        {
            if (asix == "X") 
            {
                if (PositionX == InitialX && VelocityX == 0) 
                {
                    LcmX = number;
                    FoundedX = true;
                }
            }
            else if (asix == "Y")
            {
                if (PositionY == InitialY && VelocityY == 0)
                {
                    LcmY = number;
                    FoundedY = true;
                }
            }
            else if (asix == "Z")
            {
                if (PositionZ == InitialZ && VelocityZ == 0)
                {
                    LcmZ = number;
                    FoundedZ = true;
                }
            }
        }
    }

    internal class Utility 
    {
        static decimal GCD(decimal a, decimal b)
        {
            if (a % b == 0) return b;
            return GCD(b, a % b);
        }

        internal static decimal LCM(decimal a, decimal b)
        {
            return a * b / GCD(a, b);
        }
    }
}