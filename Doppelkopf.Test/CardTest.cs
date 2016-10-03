using System;
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
    }
}
