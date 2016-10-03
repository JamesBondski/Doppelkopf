using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;

namespace Doppelkopf.Test {
    [TestClass]
    public class CardStackTest {
        private const int runs = 1000000;
        private const int mean = runs / 3;
        private const int allowedDeviation = (int)(mean * 0.1);

        [TestMethod]
        public void Shuffle() {
            //Check distribution
            int[,] counts = new int[3, 3];

            for(int i=0; i<runs; i++) {
                CardStack testStack = new CardStack();
                new Card(Rank.Two, Suit.Clubs).MoveTo(testStack);
                new Card(Rank.Three, Suit.Clubs).MoveTo(testStack);
                new Card(Rank.Four, Suit.Clubs).MoveTo(testStack);

                testStack.Shuffle();
                for(int ci = 0; ci<3; ci++) {
                    Card card = testStack.Cards[ci];
                    counts[ci, (int)card.Rank]++;
                }
            }

            //Let's say it should always be less than 10% away from the mean
            foreach(int val in counts) {
                Assert.IsTrue(Math.Abs(val - mean) < allowedDeviation);
            }
        }
    }
}
