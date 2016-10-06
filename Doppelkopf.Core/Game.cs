using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class Game {
        public List<Player> Players {
            get; set;
        }

        CardStack deck;

        public Game() {
            this.deck = DeckBuilder.GetDoppelkopfDeck();

            this.Players = new List<Player>();
            for(int i=0; i<4; i++) {
                this.Players.Add(new Player() { Name = "Player " + i });
            }
        }

        public void Deal() {
            deck.Shuffle();
            while(deck.Cards.Count > 0) {
                foreach(Player player in this.Players) {
                    deck.Cards[0].MoveTo(player.Hand);
                }
            }
        }

        public void PlayTrick(Player startPlayer) {
        }
    }
}
