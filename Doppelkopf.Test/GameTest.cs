using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;

namespace Doppelkopf.Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Game_NumberofPlayers() {
            Assert.AreEqual(4, new Game().Players.Count);
        }

        [TestMethod]
        public void Game_Start() {
            Game game = new Game();
            game.Start(2);
            Assert.AreEqual(2, game.PreviousRounds.Count);

            game = new Game();
            game.Start();
            Assert.AreEqual(1, game.PreviousRounds.Count);
        }
    }
}
