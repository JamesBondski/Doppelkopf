using Doppelkopf.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public class Game
    {
        public event EventHandler<ActionEventArgs> Action;

        public List<Player> Players {
            get;
        }

        public Round CurrentRound {
            get {
                return this.currentRound;
            }
        }

        public List<Round> PreviousRounds {
            get;
        } = new List<Round>();

        public int RoundsLeft {
            get; set;
        } = 10;

        private Round currentRound;

        public Game() {
            this.Players = new List<Player>();
            for (int i = 0; i < 4; i++) {
                this.Players.Add(new Player() { Name = "Player " + i, ID = i });
            }
        }

        public void Start(int rounds = 1) {
            this.RoundsLeft = rounds;

            DoAction(new NewRoundAction());
        }

        public void NewRound(bool start = true) {
            if (this.RoundsLeft == 0) {
                return;
            }
            this.RoundsLeft--;

            //For now, while "Hochzeit" is not implemented, just repeat until we get a valid round
            do {
                this.currentRound = new Round(this);
            } while (this.currentRound.Teams[Team.Kontra].Count != 2);

            this.CurrentRound.Action += CurrentRound_Action;
            this.currentRound.Over += CurrentRound_Over;

            if (start) {
                this.CurrentRound.Play();
            }
        }

        private void CurrentRound_Over(object sender, RoundEventArgs e) {
            DoAction(new EndRoundAction());
            DoAction(new NewRoundAction());
        }

        private void DoAction(IAction action) {
            action.Do(this);
            if (this.Action != null) {
                this.Action(this, new ActionEventArgs() { Round = this.CurrentRound, Action = action });
            }
        }

        private void CurrentRound_Action(object sender, ActionEventArgs e) {
            if(this.Action != null) {
                this.Action(this, e);
            }
        }
    }
}
