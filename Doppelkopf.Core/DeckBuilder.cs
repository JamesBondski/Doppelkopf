using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public static class DeckBuilder {
        public static CardStack GetDoppelkopfDeck() {
            CardStack deck = new CardStack();
            for (int i = 1; i <= 2; i++) {
                foreach (var suit in Enum.GetValues(typeof(Suit))) {
                    foreach (var rank in Enum.GetValues(typeof(Rank))) {
                        if ((Rank)rank > Rank.Nine) {
                            Card card = new Card((Rank)rank, (Suit)suit);
                            card.MoveTo(deck);
                        }
                    }
                }
            }
            return deck;
        }
    }
}
