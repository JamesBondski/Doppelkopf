using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {

    public enum Suit {
        Diamonds,
        Hearts,
        Spades,
        Clubs
    }

    public enum Rank {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class Card {
        public Suit Suit {
            get; set;
        }

        public Rank Rank {
            get; set;
        }

        public CardStack Stack {
            get; set;
        }

        public Card(Rank rank, Suit suit) {
            this.Suit = suit;
            this.Rank = rank;
        }

        public void MoveTo(CardStack stack) {
            if(this.Stack != null) {
                this.Stack.RemoveCard(this);
            }
            
            stack.AddCard(this, this.Stack);
            this.Stack = stack;
        }
    }
}
