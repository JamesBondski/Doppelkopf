using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{

    public enum Suit
    {
        Diamonds,
        Hearts,
        Spades,
        Clubs
    }

    public enum Rank
    {
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum CardType
    {
        Trump,
        Hearts,
        Spades,
        Clubs
    }

    public class Card
    {
        public Suit Suit {
            get; set;
        }

        public Rank Rank {
            get; set;
        }

        public CardStack Stack {
            get; set;
        }

        public CardType CardType {
            get {
                if(this.IsTrump) {
                    return CardType.Trump;
                }
                return (CardType)Enum.Parse(typeof(CardType), this.Suit.ToString());
            }
        }

        public string Symbol {
            get {
                return this.Suit.ToString().Substring(0,1) + this.Rank.ToString().Substring(0,1);
            }
        }

        public bool IsTrump {
            get {
                if (this.Suit == Suit.Diamonds) {
                    return true;
                }
                if (this.Rank == Rank.Queen || this.Rank == Rank.Jack) {
                    return true;
                }
                if (this.Rank == Rank.Ten && this.Suit == Suit.Hearts) {
                    return true;
                }
                return false;
            }
        }

        public Card(Rank rank, Suit suit) {
            this.Suit = suit;
            this.Rank = rank;
        }

        public void MoveTo(CardStack stack) {
            if (this.Stack != null) {
                this.Stack.RemoveCard(this);
            }

            stack.AddCard(this, this.Stack);
            this.Stack = stack;
        }
    }
}
