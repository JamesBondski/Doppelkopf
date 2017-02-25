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
    }
}
