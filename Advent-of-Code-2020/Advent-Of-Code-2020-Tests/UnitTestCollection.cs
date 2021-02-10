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
        public void DayTwoExampleTest()
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test inputs//day-2//day-2-example-1.txt"));
            Assert.AreEqual(2, day_2.Program.SolvePartOne(input));
            Assert.AreEqual(1, day_2.Program.SolvePartTwo(input));
        }

        [TestMethod]
        public void DayThreeExampleTest()
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test inputs//day-3//day-3-example-1.txt"));
            Assert.AreEqual(7, day_3.Program.SolvePartOne(input));
            Assert.AreEqual(336, day_3.Program.SolvePartTwo(input));
        }

        [TestMethod]
        public void DayFourExampleTest() 
        {
            string[] input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test inputs//day-4//day-4-example-1.txt")).Split(Environment.NewLine);
            List<string> candidates = day_4.Program.FindPassowrdsCandidates(input);

            Assert.AreEqual(2, day_4.Program.SolvePartOne(candidates));
        }
    }
}
