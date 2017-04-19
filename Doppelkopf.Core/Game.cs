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

        private Round currentRound;

        public Game() {
            this.Players = new List<Player>();
            for (int i = 0; i < 4; i++) {
                this.Players.Add(new Player() { Name = "Player " + i, ID = i });
            }

            this.currentRound = new Round(this);
        }

        public void Play() {
            this.CurrentRound.Action += CurrentRound_Action;
            this.CurrentRound.Play();
        }

        public void NewRound() {
            //For now, while "Hochzeit" is not implemented, just repeat until we get a valid round
            do {
                this.currentRound = new Round(this);
            } while (this.currentRound.Teams[Team.Kontra].Count != 2);
        }

        private void CurrentRound_Action(object sender, ActionEventArgs e) {
            if(this.Action != null) {
                this.Action(this, e);
            }
        }
    }
}
