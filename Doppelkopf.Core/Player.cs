using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class Player {

        public IPlayerActor Actor {
            get; set;
        }

        public string Name {
            get; set;
        }

        public Hand Hand {
            get; set;
        }

        public Player(string name = null) {
            this.Name = name;
            this.Hand = new Hand() { Owner = this };
            this.Actor = new DefaultPlayerActor() { Player = this };
        }

        public void Play(Trick trick) {
            this.Actor.GetCardForTrick(trick).MoveTo(trick);
        }
    }
}
