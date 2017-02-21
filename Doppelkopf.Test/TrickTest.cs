using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;
using System.Collections.Generic;

namespace Doppelkopf.Test
{
    [TestClass]
    public class TrickTest
    {
        [TestMethod]
        public void PlayedTest() {
            Trick trick = new Trick();
            Player player1 = new Player();
            Card card1 = new Card(Rank.Ace, Suit.Clubs);
            Player player2 = new Player();
            Card card2 = new Card(Rank.Ten, Suit.Diamonds);

            Assert.AreEqual(0, trick.Played.Count);

            card1.MoveTo(player1.Hand);
            player1.Play(trick);

            Assert.AreEqual(1, trick.Played.Count);
            Assert.AreSame(player1, trick.Played[0].Item1);
            Assert.AreSame(card1, trick.Played[0].Item2);

            card2.MoveTo(player2.Hand);
            player2.Play(trick);

            Assert.AreEqual(2, trick.Played.Count);
            Assert.AreSame(player1, trick.Played[0].Item1);
            Assert.AreSame(card1, trick.Played[0].Item2);
            Assert.AreSame(player2, trick.Played[1].Item1);
            Assert.AreSame(card2, trick.Played[1].Item2);
        }

        [TestMethod]
        public void GetWinner() {
            throw new NotImplementedException();
        }
    }
}
