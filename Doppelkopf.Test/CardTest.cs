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
        public void Card_CopyTo() {
            Card testCard = new Card(Rank.Ace, Suit.Clubs);
            CardStack firstStack = new CardStack();
            CardStack secondStack = new CardStack();

            testCard.MoveTo(firstStack);
            testCard.CopyTo(secondStack);

            Assert.AreEqual(1, firstStack.Cards.Count);
            Assert.AreEqual(1, secondStack.Cards.Count);
            Assert.AreNotSame(firstStack.Cards[0], secondStack.Cards[0]);
            Assert.AreEqual(firstStack.Cards[0].Symbol, secondStack.Cards[0].Symbol);
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

        [TestMethod]
        public void Value() {
            //Suit should not matter for this
            Assert.AreEqual(0, new Card(Rank.Nine, Suit.Clubs).Value);
            Assert.AreEqual(1, new Card(Rank.King, Suit.Clubs).Value);
            Assert.AreEqual(2, new Card(Rank.Ten, Suit.Clubs).Value);
            Assert.AreEqual(3, new Card(Rank.Ace, Suit.Clubs).Value);
            Assert.AreEqual(0, new Card(Rank.Nine, Suit.Hearts).Value);
            Assert.AreEqual(1, new Card(Rank.King, Suit.Hearts).Value);
            //Hearts 10 is a trump
            Assert.AreEqual(3, new Card(Rank.Ace, Suit.Hearts).Value);
            Assert.AreEqual(0, new Card(Rank.Nine, Suit.Spades).Value);
            Assert.AreEqual(1, new Card(Rank.King, Suit.Spades).Value);
            Assert.AreEqual(2, new Card(Rank.Ten, Suit.Spades).Value);
            Assert.AreEqual(3, new Card(Rank.Ace, Suit.Spades).Value);

            //Check trumps
            Assert.AreEqual(0, new Card(Rank.Nine, Suit.Diamonds).Value);
            Assert.AreEqual(1, new Card(Rank.King, Suit.Diamonds).Value);
            Assert.AreEqual(2, new Card(Rank.Ten, Suit.Diamonds).Value);
            Assert.AreEqual(3, new Card(Rank.Ace, Suit.Diamonds).Value);
            Assert.AreEqual(4, new Card(Rank.Jack, Suit.Diamonds).Value);
            Assert.AreEqual(5, new Card(Rank.Jack, Suit.Hearts).Value);
            Assert.AreEqual(6, new Card(Rank.Jack, Suit.Spades).Value);
            Assert.AreEqual(7, new Card(Rank.Jack, Suit.Clubs).Value);
            Assert.AreEqual(8, new Card(Rank.Queen, Suit.Diamonds).Value);
            Assert.AreEqual(9, new Card(Rank.Queen, Suit.Hearts).Value);
            Assert.AreEqual(10, new Card(Rank.Queen, Suit.Spades).Value);
            Assert.AreEqual(11, new Card(Rank.Queen, Suit.Clubs).Value);
            Assert.AreEqual(12, new Card(Rank.Ten, Suit.Hearts).Value);
        }

        public void CompareCardsBothWays(int expected, Card card1, Card card2) {
            Assert.AreEqual(expected, card1.CompareCardTo(card2));
            Assert.AreEqual(expected * -1, card2.CompareCardTo(card1));
        }

        [TestMethod]
        public void CompareCardTo() {
            //Check cards of non-trump suit
            CompareCardsBothWays(1, new Card(Rank.Ten, Suit.Spades), new Card(Rank.King, Suit.Spades));
            Assert.AreEqual(0, new Card(Rank.Ten, Suit.Spades).CompareCardTo(new Card(Rank.Ten, Suit.Spades)));

            //Compare trump with non-trump
            CompareCardsBothWays(1, new Card(Rank.Ten, Suit.Diamonds),new Card(Rank.Ten, Suit.Spades));
            CompareCardsBothWays(1, new Card(Rank.Ten, Suit.Diamonds), new Card(Rank.Ace, Suit.Spades));
            CompareCardsBothWays(1, new Card(Rank.Ten, Suit.Diamonds), new Card(Rank.King, Suit.Spades));

            //Check trump tiers
            CompareCardsBothWays(1, new Card(Rank.Jack, Suit.Diamonds), new Card(Rank.Ace, Suit.Diamonds));
            CompareCardsBothWays(1, new Card(Rank.Queen, Suit.Diamonds), new Card(Rank.Jack, Suit.Diamonds));
            CompareCardsBothWays(1, new Card(Rank.Ten, Suit.Hearts), new Card(Rank.Queen, Suit.Diamonds));
            CompareCardsBothWays(1, new Card(Rank.Ten, Suit.Hearts), new Card(Rank.Jack, Suit.Diamonds));
            CompareCardsBothWays(1, new Card(Rank.Ten, Suit.Hearts), new Card(Rank.Ace, Suit.Diamonds));

            //Check within trump tiers
            CompareCardsBothWays(1, new Card(Rank.Jack, Suit.Hearts), new Card(Rank.Jack, Suit.Diamonds));
            CompareCardsBothWays(1, new Card(Rank.Jack, Suit.Spades), new Card(Rank.Jack, Suit.Diamonds));
            CompareCardsBothWays(1, new Card(Rank.Jack, Suit.Clubs), new Card(Rank.Jack, Suit.Diamonds));
            CompareCardsBothWays(1, new Card(Rank.Queen, Suit.Hearts), new Card(Rank.Queen, Suit.Diamonds));
            CompareCardsBothWays(1, new Card(Rank.Queen, Suit.Spades), new Card(Rank.Queen, Suit.Diamonds));
            CompareCardsBothWays(1, new Card(Rank.Queen, Suit.Clubs), new Card(Rank.Queen, Suit.Diamonds));
        }

        [TestMethod]
        public void CardPoints() {
            Assert.AreEqual(11, new Card("DA").Points);
            Assert.AreEqual(10, new Card("DT").Points);
            Assert.AreEqual(4, new Card("DK").Points);
            Assert.AreEqual(3, new Card("DQ").Points);
            Assert.AreEqual(2, new Card("DJ").Points);
            Assert.AreEqual(0, new Card("DN").Points);
        }
    }
}
