using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;
using System.Linq;

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
            game.NewRound(false);
            round = game.CurrentRound;
        }

        [TestMethod]
        public void DealTest() {
            foreach(Player player in round.Players) {
                Assert.AreEqual(10, player.Hand.Cards.Count);
            }
        }

        [TestMethod]
        public void PlayTrickTest() {
            Trick trick = round.CurrentTrick;
            round.PlayTrick();

            Assert.AreEqual(4, trick.Played.Count);
            Assert.AreSame(round.Players[0], trick.Played[0].Item1);
            Assert.AreSame(round.Players[1], trick.Played[1].Item1);
            Assert.AreSame(round.Players[2], trick.Played[2].Item1);
            Assert.AreSame(round.Players[3], trick.Played[3].Item1);

            Assert.AreSame(trick.GetWinner(), round.StartPlayer);
        }

        [TestMethod]
        [ExpectedException(typeof(RoundFinishedException))]
        public void PlayTrick_RoundFinished() {
            int numCards = round.Players[0].Hand.Cards.Count;
            try {
                for (int i = 0; i < numCards; i++) {
                    round.PlayTrick();
                }
            }
            catch(RoundFinishedException) {
                //Exception should only be thrown on last call
                Assert.Fail();
            }

            round.PlayTrick();
        }

        [TestMethod]
        public void Round_TotalPoints() {
            while(!round.IsOver) {
                round.PlayTrick();
            }
            Assert.AreEqual(240, round.Players.Select(player => player.RoundPoints).Sum());
        }

        [TestMethod]
        public void Round_Teams() {
            Assert.AreEqual(2, round.Teams.Count);
            Assert.AreEqual(2, round.Teams[Team.Kontra].Count);
            Assert.AreEqual(2, round.Teams[Team.Re].Count);

            foreach(var team in round.Teams) {
                foreach(var player in team.Value) {
                    int queenOfClubsCount = player.Hand.Cards.Count(card => card.Symbol == "CQ");
                    if(team.Key == Team.Re) {
                        Assert.AreEqual(1, queenOfClubsCount);
                    }
                    else {
                        Assert.AreEqual(0, queenOfClubsCount);
                    }
                }
            }
        }

        bool eventRaised = false;
        int callCount = 0;

        [TestMethod]
        public void Round_Over() {
            Assert.IsFalse(eventRaised);
            Assert.AreSame(round, game.CurrentRound);
            round.Over += Round_OverEvent;
            round.Play();
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(1, callCount);
        }

        private void Round_OverEvent(object sender, RoundEventArgs e) {
            eventRaised = true;
            callCount++;
        }
    }
}
