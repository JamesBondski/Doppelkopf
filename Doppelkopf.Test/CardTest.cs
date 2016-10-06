using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;

namespace Doppelkopf.Test {
    [TestClass]
    public class CardTest {

        [TestMethod]
        public void Constructor() {
            Card testCard = new Card(Rank.Ace, Suit.Clubs);
            Assert.AreEqual(Rank.Ace, testCard.Rank);
            Assert.AreEqual(Suit.Clubs, testCard.Suit);
        }

        [TestMethod]
        public void MoveTo() {
            Card testCard = new Card(Rank.Ace, Suit.Clubs);
            CardStack firstStack = new CardStack();
            CardStack secondStack = new CardStack();

            //First move
            testCard.MoveTo(firstStack);
            Assert.AreEqual(1, firstStack.Cards.Count);
            Assert.AreSame(testCard, firstStack.Cards[0]);

            //Second move
            testCard.MoveTo(secondStack);
            Assert.AreEqual(1, secondStack.Cards.Count);
            Assert.AreSame(testCard, secondStack.Cards[0]);
            Assert.AreEqual(0, firstStack.Cards.Count);
        }

        [TestMethod]
        public void IsTrump() {
            //All Diamonds should be trump
            foreach (Rank rank in Enum.GetValues(typeof(Rank))) {
                Assert.IsTrue(new Card(rank, Suit.Diamonds).IsTrump);
            }

            //All Jacks and Queens should be trump
            foreach (Suit suit in Enum.GetValues(typeof(Suit))) {
                Assert.IsTrue(new Card(Rank.Queen, suit).IsTrump);
                Assert.IsTrue(new Card(Rank.Jack, suit).IsTrump);
            }

            //Heart 10 should be trump
            Assert.IsTrue(new Card(Rank.Ten, Suit.Hearts).IsTrump);

            //All others should not be trump
            foreach (Suit suit in Enum.GetValues(typeof(Suit))) {
                foreach (Rank rank in Enum.GetValues(typeof(Rank))) {
                    if (suit != Suit.Diamonds) {
                        if (rank != Rank.Queen && rank != Rank.Jack) {
                            if (rank != Rank.Ten || suit != Suit.Hearts) {
                                Assert.IsFalse(new Card(rank, suit).IsTrump);
                            }
                        }
                    }
                }
            }
        }
    }
}
