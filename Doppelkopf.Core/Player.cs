using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class Player {

        public string Name {
            get; set;
        }

        public Hand Hand {
            get; set;
        }

        public Player() {
            this.Hand = new Hand() { Owner = this };
        }

        public void Play(Trick trick) {
            Hand.Cards[0].MoveTo(trick);
        }
    }
}
