using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;
using System.Linq;

namespace Doppelkopf.Test
{
    [TestClass]
    public class EndGameInfoTest
    {
        Game game;
        Round round;
        EndGameInfo info;

        [TestInitialize]
        public void Init() {
            game = new Game();
            round = game.CurrentRound;
            game.Play();
            info = round.End();
        }

        [TestMethod]
        public void EndGameInfo_WinningTeam() {
            Assert.IsTrue(round.Teams[info.WinningTeam].All(player => info.WinningPlayers.Contains(player)));
            Assert.IsTrue(info.WinningPlayers.Sum(player => player.RoundPoints) >= 120);
        }

        [TestMethod]
        public void EndGameInfo_Points() {
            int winningPoints = info.WinningPlayers.Sum(player => player.RoundPoints);
            if(winningPoints == 240) {
                Assert.AreEqual(5, info.Points);
            }
            else if(winningPoints > 210) {
                Assert.AreEqual(4, info.Points);
            }
            else if(winningPoints > 180) {
                Assert.AreEqual(3, info.Points);
            }
            else if(winningPoints > 150) {
                Assert.AreEqual(2, info.Points);
            }
            else {
                Assert.AreEqual(1, info.Points);
            }
        }
    }
}
