using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_Of_Code_2020_Tests
{
    [TestClass]
    public class UnitTestCollection
    {
        [TestMethod]
        public void DayOneExampleTest()
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test inputs//day-1//day-1-example-1.txt"));
            List<int> expenceReport = (Array.ConvertAll(input, s => Int32.Parse(s))).ToList();
            expenceReport.Sort();
            Assert.AreEqual(514579, day_1.Program.SolvePartOne(expenceReport));
            Assert.AreEqual(241861950, day_1.Program.SolvePartTwo(expenceReport));
        }

        [TestMethod]
        public void DayNeighteenExampleTest()
        {
            //string input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test inputs//day-19//day-19-example-1.txt"));

            //Assert.AreEqual(2, day_19.Program.SolvePartOne(input));
        }
    }
}
