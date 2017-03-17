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
            Round round = new Round(new Game());
            Trick trick = round.PlayTrick();

            //Check that 4 PlayCardActions are on the round
            Assert.AreEqual(4, round.Actions.Count);
            for(int i=0;i<4;i++) {
                Assert.IsInstanceOfType(round.Actions[i], typeof(PlayCardAction));
            }
        }
    }
}
