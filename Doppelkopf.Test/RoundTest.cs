using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;

namespace Doppelkopf.Test
{
    [TestClass]
    public class RoundTest
    {
        Game game;
        Round round;

        [TestInitialize]
        public void Setup() {
            game = new Game();
            round = game.NewRound();
        }

        [TestMethod]
        public void DealTest() {
            foreach(Player player in round.Players) {
                Assert.AreEqual(10, player.Hand.Cards.Count);
            }
        }

        [TestMethod]
        public void PlayTrickTest() {
            Trick trick = round.PlayTrick();

            Assert.AreEqual(4, trick.Played.Count);
            Assert.AreSame(round.Players[0], trick.Played[0].Item1);
            Assert.AreSame(round.Players[1], trick.Played[1].Item1);
            Assert.AreSame(round.Players[2], trick.Played[2].Item1);
            Assert.AreSame(round.Players[3], trick.Played[3].Item1);

            Assert.AreSame(trick.GetWinner(), round.StartPlayer);
        }
    }
}
