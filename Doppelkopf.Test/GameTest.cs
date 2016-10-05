using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;

namespace Doppelkopf.Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void DealTest() {
            Game game = new Game();
            game.Deal();

            foreach(Player player in game.Players) {
                Assert.AreEqual(10, player.Hand.Cards.Count);
            }
        }
    }
}
