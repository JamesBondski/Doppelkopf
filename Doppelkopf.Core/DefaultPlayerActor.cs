using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public class DefaultPlayerActor : IPlayerActor
    {
        private static Random rnd = new Random();

        public Player Player {
            get; set;
        }

        public Card GetCardForTrick(Trick trick) {
            var playable = this.Player.Hand.GetPlayableCards(trick).ToList();
            return playable[rnd.Next(playable.Count)];
        }
    }
}
