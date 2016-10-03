using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class Game {
        List<Player> Players {
            get; set;
        }

        CardStack deck;

        public Game() {
            this.deck = DeckBuilder.GetDoppelkopfDeck();
        }

        public void Deal() {
            
        }
    }
}
