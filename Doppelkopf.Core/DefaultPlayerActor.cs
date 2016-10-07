using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    class DefaultPlayerActor : IPlayerActor
    {
        public Player Player {
            get; set;
        }

        public Card GetCardForTrick(Trick trick) {
            return this.Player.Hand.GetPlayableCards(trick).First();
        }
    }
}
