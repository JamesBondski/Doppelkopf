using Doppelkopf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Console
{
    public class State
    {
        public Game Game {
            get; set;
        }

        public Round Round {
            get; set;
        }

        public bool DoQuit {
            get; set;
        }

        public State() {
            this.Game = new Game();
            this.Round = this.Game.NewRound();
            this.DoQuit = false;
        }
    }
}
