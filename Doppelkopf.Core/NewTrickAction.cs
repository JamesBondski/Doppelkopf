using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public class NewTrickAction : IAction
    {
        public string Name {
            get {
                return "NewTrick";
            }
        }

        public Trick LastTrick { get; }
        public Round Round { get; }

        private Player LastStartPlayer { get; set; }

        public NewTrickAction(Round round, Trick lastTrick) {
            this.Round = round;
            this.LastTrick = lastTrick;
        }

        public void Do() {
            this.LastStartPlayer = this.Round.StartPlayer;

            this.Round.StartPlayer = this.LastTrick.GetWinner();
            this.Round.StartPlayer.Tricks.Add(this.LastTrick);
        }

        public void Undo() {
            this.LastStartPlayer.Tricks.Remove(this.LastTrick);
            this.Round.StartPlayer = this.LastStartPlayer;
        }
    }
}
