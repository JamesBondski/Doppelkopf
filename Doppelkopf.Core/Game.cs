using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public class Game
    {
        public List<Player> Players {
            get;
        }

        public Game() {
            this.Players = new List<Player>();
            for (int i = 0; i < 4; i++) {
                this.Players.Add(new Player() { Name = "Player " + i });
            }
        }

        public Round NewRound() {
            return new Round(this);
        }
    }
}
