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

        public Trick PlayTrick(Player startPlayer) { 
            Queue<Player> trickQueue = new Queue<Player>();
            Trick trick = new Trick();
            for(int i=this.Players.IndexOf(startPlayer); i<4; i++) {
                this.Players[i].Play(trick);
            }
            for (int i = 0; i<this.Players.IndexOf(startPlayer); i++) {
                this.Players[i].Play(trick);
            }
            return trick;
        }
    }
}
