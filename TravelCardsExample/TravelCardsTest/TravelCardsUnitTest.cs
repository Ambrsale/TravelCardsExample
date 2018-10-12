using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelCards.Entities;
using TravelCards.Managers.TravelSolver;
using System.Collections.Generic;
using System.Linq;


namespace TravelCardsTest
{
    [TestClass]
    public class TravelCardsUnitTest
    {
        private List<TravelCard> prepareDirectTestItems()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("e", "f"));

            return result;
        }

        private List<TravelCard> prepareCircleTestItems()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("e", "f"));
            result.Add(new TravelCard("f", "a"));

            return result;
        }

        private List<TravelCard> prepareOneCrossTestItems()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("e", "f"));
            result.Add(new TravelCard("f", "d"));
            result.Add(new TravelCard("d", "g"));
            result.Add(new TravelCard("g", "h"));

            return result;
        }

        private List<TravelCard> prepareOnePointThreeTimesCrossWithExitTestItems()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("e", "f"));
            result.Add(new TravelCard("f", "d"));
            result.Add(new TravelCard("d", "g"));
            result.Add(new TravelCard("g", "h"));
            result.Add(new TravelCard("h", "d"));
            result.Add(new TravelCard("d", "i"));

            return result;
        }

        private List<TravelCard> prepareOnePointThreeTimesCrossNoExitTestItems()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("e", "f"));
            result.Add(new TravelCard("f", "d"));
            result.Add(new TravelCard("d", "g"));
            result.Add(new TravelCard("g", "h"));
            result.Add(new TravelCard("h", "d"));

            return result;
        }

        private List<TravelCard> prepareStartAndCircleTestItems()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("e", "f"));
            result.Add(new TravelCard("f", "c"));

            return result;
        }

        private List<TravelCard> prepareCircleAndExitTestItems()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("d", "a"));
            result.Add(new TravelCard("a", "f"));
            result.Add(new TravelCard("f", "g"));

            return result;
        }

        private List<TravelCard> prepareCircleToCircleTestItems()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "a"));
            result.Add(new TravelCard("a", "d"));
            result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("e", "a"));

            return result;
        }


        private List<TravelCard> prepareBrokenTravel()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            //result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("e", "f"));
            result.Add(new TravelCard("f", "g"));

            return result;
        }


        private List<TravelCard> prepareComplicated1Travel()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("e", "c"));
            result.Add(new TravelCard("c", "f"));
            result.Add(new TravelCard("f", "b"));
            result.Add(new TravelCard("b", "g"));
            result.Add(new TravelCard("g", "e"));
            result.Add(new TravelCard("e", "d"));
            result.Add(new TravelCard("d", "h"));

            return result;
        }

        // special order of prepareComplicated1Travel, error resolved
        private List<TravelCard> prepareComplicated1Travel_Case2()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("d", "e"));
            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("e", "c"));
            result.Add(new TravelCard("f", "b"));
            result.Add(new TravelCard("e", "d"));
            result.Add(new TravelCard("b", "g"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("g", "e"));
            result.Add(new TravelCard("d", "h"));
            result.Add(new TravelCard("c", "f"));
            return result;
        }

        private List<TravelCard> prepareComplicated2Travel()
        {
            List<TravelCard> result = new List<TravelCard>();

            result.Add(new TravelCard("a", "b"));
            result.Add(new TravelCard("b", "c"));
            result.Add(new TravelCard("c", "d"));
            result.Add(new TravelCard("d", "c"));
            result.Add(new TravelCard("c", "b"));
            result.Add(new TravelCard("b", "e"));


            return result;
        }


        /// <summary>
        /// Random shuffle given list.
        /// </summary>
        private List<TravelCard> shuffleList(List<TravelCard> source)
        {
            Random random = new Random();
            return source.OrderBy(item => random.Next()).ToList();
        }

        [TestMethod]
        public void TestDirectTravel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareDirectTestItems()));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "f");
        }

        [TestMethod]
        public void TestCircleTravel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareCircleTestItems()));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
        }

        [TestMethod]
        public void TestOneCrossTravel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareOneCrossTestItems()));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "h");
        }

        [TestMethod]
        public void TestOnePointThreeTimesCrossWithExitTravel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareOnePointThreeTimesCrossWithExitTestItems()));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "i");
        }

        [TestMethod]
        public void TestOnePointThreeTimesCrossNoExitTravel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareOnePointThreeTimesCrossNoExitTestItems()));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "d");
        }

        [TestMethod]
        public void TestStartAndCircleTravel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareStartAndCircleTestItems()));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "c");
        }

        [TestMethod]
        public void TestCircleAndExitTravel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareCircleAndExitTestItems()));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "g");
        }

        [TestMethod]
        public void TestCircleToCircleTravel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareCircleToCircleTestItems()));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "a");
        }

        [TestMethod]
        public void TestBrokenTravel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareBrokenTravel()));
            ts.Solve();
            Assert.IsNull(ts.Result);
        }

        [TestMethod]
        public void TestComplicatedTravel_Case2()
        {
            // special case with previous implementation error
            TravelSolution ts = new TravelSolution(prepareComplicated1Travel_Case2());
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "h");
        }

        [TestMethod]
        public void TestComplicatedTravel()
        {
            var data = shuffleList(prepareComplicated1Travel());
            TravelSolution ts = new TravelSolution(new List<TravelCard>(data));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "h");
        }

        [TestMethod]
        public void TestComplicated2Travel()
        {
            TravelSolution ts = new TravelSolution(shuffleList(prepareComplicated2Travel()));
            ts.Solve();
            Assert.IsNotNull(ts.Result);
            Assert.AreEqual(ts.Result.StartPoint, "a");
            Assert.AreEqual(ts.Result.EndPoint, "e");
        }
    }
}
