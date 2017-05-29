using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;
using System.Collections.Generic;
using System.Linq;

namespace Doppelkopf.Test
{
    [TestClass]
    public class TrickTest
    {
        [TestMethod]
        public void PlayedTest() {
            Game game = new Game();
            game.NewRound(false);
            Round round = game.CurrentRound;
            Trick trick = game.CurrentRound.CurrentTrick;
            Player player1 = game.Players[0];
            Player player2 = game.Players[1];

            Assert.AreEqual(0, trick.Played.Count);

            player1.Play(round, trick);
            Card card1 = trick.Cards[trick.Cards.Count - 1];

            Assert.AreEqual(1, trick.Played.Count);
            Assert.AreSame(player1, trick.Played[0].Item1);
            Assert.AreSame(card1, trick.Played[0].Item2);

            player2.Play(round, trick);

            Card card2 = trick.Cards[trick.Cards.Count - 1];
            Assert.AreEqual(2, trick.Played.Count);
            Assert.AreSame(player1, trick.Played[0].Item1);
            Assert.AreSame(card1, trick.Played[0].Item2);
            Assert.AreSame(player2, trick.Played[1].Item1);
            Assert.AreSame(card2, trick.Played[1].Item2);
        }

        private Trick CreateTrick(string cards) {
            string[] cardCodes = cards.Split(' ');

            Trick trick = new Trick();
            int index = 0;
            foreach(string cardCode in cardCodes) {
                Card card = new Card(cardCode);
                card.MoveTo(new Player("Player "+(index+1)).Hand);
                card.MoveTo(trick);

                index++;
            }
            return trick;
        }

        [TestMethod]
        [ExpectedException(typeof(TrickNotCompleteException))]
        public void GetWinner_TooFewCards() {
            //Ungültig weil nur drei Karten
            Player winner = CreateTrick("HT HT DQ").GetWinner();
        }

        [TestMethod]
        [ExpectedException(typeof(TrickNotCompleteException))]
        public void GetWinner_TooManyCards() {
            //Ungültig weil nur drei Karten
            Player winner = CreateTrick("HT HT DQ HQ SQ").GetWinner();
        }

        [TestMethod]
        public void GetWinner_Positions() {
            Assert.AreEqual("Player 1", CreateTrick("SA ST SK SK").GetWinner().Name);
            Assert.AreEqual("Player 2", CreateTrick("ST SA SK SK").GetWinner().Name);
            Assert.AreEqual("Player 3", CreateTrick("ST SK SA SK").GetWinner().Name);
            Assert.AreEqual("Player 4", CreateTrick("ST SK SK SA").GetWinner().Name);
        }

        [TestMethod]
        public void GetWinner_Trump() {
            Assert.AreEqual("Player 2", CreateTrick("SA DA SK SK").GetWinner().Name);
            Assert.AreEqual("Player 3", CreateTrick("SA DA DJ SK").GetWinner().Name);
            Assert.AreEqual("Player 3", CreateTrick("DA DJ DQ SJ").GetWinner().Name);
        }

        [TestMethod]
        public void Trick_Points() {
            //Point values of individual cards are tested, so we just want to test addition
            Assert.AreEqual(28, CreateTrick("SA DT HK CQ").Points);
        }
    }
}
