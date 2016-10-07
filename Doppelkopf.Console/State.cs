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

        public bool DoQuit {
            get; set;
        }

        public State() {
            this.Game = new Game();
            this.Game.Deal();
            this.DoQuit = false;
        }
    }
}
