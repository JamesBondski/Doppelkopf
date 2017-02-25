using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class Round {
        CardStack deck;
        Game Game {
            get;
        }

        public List<Player> Players {
            get;
        }

        public Round(Game game) {
            this.Game = game;
            this.deck = DeckBuilder.GetDoppelkopfDeck();
            this.Players = this.Game.Players;

            this.Deal();
        }

        public void Deal() {
            this.deck.Shuffle();
            while(this.deck.Cards.Count > 0) {
                foreach(Player player in this.Players) {
                    this.deck.Cards[0].MoveTo(player.Hand);
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
