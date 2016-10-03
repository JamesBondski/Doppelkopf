using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class Player {
        CardStack hand = new CardStack();

        public CardStack Hand {
            get { return hand; }
        }
    }
}
