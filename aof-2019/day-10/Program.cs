using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] map = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map.txt"));
            int lengh = map.Length;
            int width = map[0].Length;

            List<Asteroid> allAsteroids = ReadMap(map, lengh, width);
            Asteroid station = SolutionForPartOne(allAsteroids);
            //Console.ReadKey();

            allAsteroids = ReadMap(map, lengh, width);
            List<Asteroid> visibleOnes = GetVisibleAsteroids(station, allAsteroids);
            SolvePartTow(station, allAsteroids);
        }

        private static List<Asteroid> ReadMap(string[] map, int lengh, int width)
        {
            List<Asteroid> allAsteroids = new List<Asteroid>();
            for (int i = 0; i < lengh; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i][j].ToString().Equals("#"))
                    {
                        allAsteroids.Add(new Asteroid(j, i, 0));
                    }
                }
            }

            return allAsteroids;
        }

        private static void SolvePartTow(Asteroid station, List<Asteroid> visibleOnes)
        {
            // not my gratest momenet, solution "inspired" by others
            List<Asteroid> calculatedAsteroids = new List<Asteroid>(station.NumberOfVisibleAsteroids);
            foreach (Asteroid asteroid in visibleOnes) 
            {
                var dx = asteroid.X - station.X;
                var dy = asteroid.Y - station.Y;

                double ang = Math.PI / 2 + Math.Atan2(dy, dx);
                var leng = dx * dx + dy * dy;

                asteroid.AngleBetweenStationAndThisAsteroid = ang;
                asteroid.Lenght = leng;
                calculatedAsteroids.Add(asteroid);
            }

            List<Asteroid> sortedList = calculatedAsteroids.OrderBy(x => x.AngleBetweenStationAndThisAsteroid).ThenBy(p => p.Lenght).ToList();
            double currentAngle = -1;
            int count = 0;
            var startIndex = calculatedAsteroids.Where(x=>x.AngleBetweenStationAndThisAsteroid < 0).ToList().Count();
            while (true)
            {
                var ls = sortedList[startIndex % sortedList.Count];
                
                //is this asteroid behind last one
                if (ls.AngleBetweenStationAndThisAsteroid == currentAngle) 
                {
                    startIndex++;
                    continue;
                }

                currentAngle = ls.AngleBetweenStationAndThisAsteroid;
                count++;

                if (count == 200)
                {
                    System.Console.WriteLine($"2. ({ls.X},{ls.Y}) => {100 * ls.X + ls.Y}");
                    break;
                }

                startIndex++;
            }
        }
    
        private static Asteroid SolutionForPartOne(List<Asteroid> allAsteroids)
        {
            foreach (Asteroid possibleStation in allAsteroids)
            {
                foreach (Asteroid asteroid in allAsteroids)
                {
                    if (Object.Equals(possibleStation, asteroid))
                    {
                        continue;
                    }
                    bool flag = false;
                    foreach (Asteroid possibleBlocker in allAsteroids)
                    {
                        if (Object.Equals(asteroid, possibleBlocker) || Object.Equals(possibleStation, possibleBlocker))
                        {
                            continue;
                        }

                        if (IsBlocked(possibleStation, asteroid, possibleBlocker) && IsBlokerBeetweenAsteroidsByAsixX(possibleStation, asteroid, possibleBlocker) && IsBlokerBeetweenAsteroidsByAsixY(possibleStation, asteroid, possibleBlocker))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        possibleStation.NumberOfVisibleAsteroids++;
                    }
                }
            }

            Asteroid bestPosition = allAsteroids.Select(x => x).OrderByDescending(i => i.NumberOfVisibleAsteroids).First();
            Console.WriteLine(bestPosition.ToString());
            
            return bestPosition;
        }

        private static List<Asteroid> GetVisibleAsteroids(Asteroid bestPosition, List<Asteroid> allAsteroids)
        {
            List<Asteroid> visibleAsteroids = new List<Asteroid>();
            foreach (Asteroid asteroid in allAsteroids)
            {
                if (Object.Equals(bestPosition, asteroid))
                {
                    continue;
                }
                bool flag = false;
                foreach (Asteroid possibleBlocker in allAsteroids)
                {
                    if (Object.Equals(asteroid, possibleBlocker) || Object.Equals(bestPosition, possibleBlocker))
                    {
                        continue;
                    }

                    if (IsBlocked(bestPosition, asteroid, possibleBlocker) && IsBlokerBeetweenAsteroidsByAsixX(bestPosition, asteroid, possibleBlocker) && IsBlokerBeetweenAsteroidsByAsixY(bestPosition, asteroid, possibleBlocker))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    visibleAsteroids.Add(asteroid);
                }
            }

            return visibleAsteroids;
        }

        private static bool IsBlokerBeetweenAsteroidsByAsixY(Asteroid possibleStation, Asteroid asteroid, Asteroid possibleBlocker)
        {
            return ((possibleStation.Y <= possibleBlocker.Y && possibleBlocker.Y <= asteroid.Y) || (possibleStation.Y >= possibleBlocker.Y && possibleBlocker.Y >= asteroid.Y));
        }

        private static bool IsBlokerBeetweenAsteroidsByAsixX(Asteroid possibleStation, Asteroid asteroid, Asteroid possibleBlocker)
        {
            return ((possibleStation.X <= possibleBlocker.X && possibleBlocker.X <= asteroid.X) || (possibleStation.X >= possibleBlocker.X && possibleBlocker.X >= asteroid.X));
        }

        private static bool IsBlocked(Asteroid possibleStation, Asteroid asteroid, Asteroid possibleBlocker)
        {
            if (possibleStation.X == asteroid.X && possibleStation.X == possibleBlocker.X)
            {
                //in same asix
                return true;
            }
            else if (possibleStation.X == asteroid.X || possibleStation.X == possibleBlocker.X)
            {
                return false;
            }

            //return true if tan of the angles is same
            return Math.Abs((double)((double)(asteroid.Y - possibleStation.Y) / (double)(asteroid.X - possibleStation.X))) == Math.Abs((double)((double)(possibleBlocker.Y - possibleStation.Y) / (double)(possibleBlocker.X - possibleStation.X)));
        }
    }

    public class Asteroid 
    {
        public Asteroid(int i, int j, int v)
        {
            X = i;
            Y = j;
            NumberOfVisibleAsteroids = v;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int NumberOfVisibleAsteroids { get; set; }
        public double Lenght { get; set; }
        public double AngleBetweenStationAndThisAsteroid { get; set; }
        
        public override string ToString()
        {
            return $"X {X}, Y {Y}, NumberOfVisibleAsteroids: {NumberOfVisibleAsteroids}";
        }
    }
}