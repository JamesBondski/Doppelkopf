using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class CardStack {
        List<Card> cards = new List<Card>();

        public Player Owner {
            get; set;
        }
        
        public IReadOnlyList<Card> Cards {
            get { return cards.AsReadOnly(); }
        }

        public void Shuffle() {
            List<Card> shuffledCards = new List<Card>();
            Random rnd = new Random();

            while(cards.Count > 0) {
                Card randomCard = cards[rnd.Next(cards.Count)];
                shuffledCards.Add(randomCard);
                cards.Remove(randomCard);
            }
            cards = shuffledCards;
        }

        internal protected virtual void AddCard(Card card, CardStack previousStack = null) {
            cards.Add(card);
        }

        internal protected virtual void RemoveCard(Card card) {
            cards.Remove(card);
        }
    }
}
