using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;

namespace Doppelkopf.Test
{
    [TestClass]
    public class ActionTest
    {
        [TestMethod]
        public void TrickActions() {
            Game game = new Game();
            Trick trick = game.CurrentRound.CurrentTrick;
            game.CurrentRound.PlayTrick();

            //Check that 4 PlayCardActions and 1 NewTrickAction are on the round
            Assert.AreEqual(5, game.CurrentRound.Actions.Count);
            for(int i=0;i<4;i++) {
                Assert.IsInstanceOfType(game.CurrentRound.Actions[i], typeof(PlayCardAction));
            }
            Assert.IsInstanceOfType(game.CurrentRound.Actions[4], typeof(NewTrickAction));

            Assert.AreEqual(trick.GetWinner(), game.CurrentRound.StartPlayer);
        }
    }
}
