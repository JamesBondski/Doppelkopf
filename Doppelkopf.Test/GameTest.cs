using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;

namespace Doppelkopf.Test
{
    [TestClass]
    public class GameTest
    {
        Game game;

        [TestInitialize]
        public void Setup() {
            game = new Game();
            game.Deal();
        }

        [TestMethod]
        public void DealTest() {
            foreach(Player player in game.Players) {
                Assert.AreEqual(10, player.Hand.Cards.Count);
            }
        }

        [TestMethod]
        public void PlayTrickTest() {
            Trick trick = game.PlayTrick(game.Players[1]);

            Assert.AreEqual(4, trick.Played.Count);
            Assert.AreSame(game.Players[1], trick.Played[0].Item1);
            Assert.AreSame(game.Players[2], trick.Played[1].Item1);
            Assert.AreSame(game.Players[3], trick.Played[2].Item1);
            Assert.AreSame(game.Players[0], trick.Played[3].Item1);
        }
    }
}
