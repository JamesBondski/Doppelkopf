using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{

    public enum Suit
    {
        Diamonds = 0,
        Hearts = 1,
        Spades = 2,
        Clubs = 3
    }

    public enum Rank
    {
        Nine = 0,
        Ten = 2,
        Jack = 4,
        Queen = 5,
        King = 1,
        Ace = 3
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
        static readonly Dictionary<string, Rank> RankMap = new Dictionary<string, Rank>() {
            ["N"] = Rank.Nine,
            ["T"] = Rank.Ten,
            ["J"] = Rank.Jack,
            ["Q"] = Rank.Queen,
            ["K"] = Rank.King,
            ["A"] = Rank.Ace
        };

        static readonly Dictionary<string, Suit> SuitMap = new Dictionary<string, Suit>() {
            ["D"] = Suit.Diamonds,
            ["H"] = Suit.Hearts,
            ["S"] = Suit.Spades,
            ["C"] = Suit.Clubs
        };

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

        /// <summary>
        /// Returns the numeric value of the card inside its card type.
        /// </summary>
        public int Value {
            get {
                if(!this.IsTrump || (this.Suit == Suit.Diamonds && this.Rank <= Rank.Ace)) {
                    return (int)this.Rank;
                }
                //Trump tiers: Diamonds (3 kinds), Jacks (4 kinds), Queens (4 kinds), Ten of Hearts (1)
                if(this.Rank == Rank.Jack) {
                    return 4 + (int)this.Suit; //Highest Diamond = Ace (Value of 3) + 1 + Rank
                }
                if(this.Rank == Rank.Queen) {
                    return 8 + (int)this.Suit;
                }

                Trace.Assert(this.Rank == Rank.Ten && this.Suit == Suit.Hearts, "Card must be Ten of hearts");
                return 12; //Ten of Hearts
            }
        }

        public Card(Rank rank, Suit suit) {
            this.Suit = suit;
            this.Rank = rank;
        }

        public Card(string code) {
            if(code.Length != 2 || !SuitMap.ContainsKey(code[0].ToString()) || ! RankMap.ContainsKey(code[1].ToString())) {
                throw new ArgumentException("Invalid card code: " + code);
            }

            this.Suit = SuitMap[code[0].ToString()];
            this.Rank = RankMap[code[1].ToString()];
        }

        public void MoveTo(CardStack stack) {
            stack.AddCard(this, this.Stack);
            if (this.Stack != null) {
                this.Stack.RemoveCard(this);
            }
            this.Stack = stack;
        }

        /// <summary>
        /// Checks if the this card is higher than the other one. This depends on what Suit was played originally.
        /// <returns>
        /// 1 if this card is higher (either same card type and higher rank or trump)
        /// 0 if they are equal or from different non-trump suits
        /// -1 if the other card is higher
        /// </returns>
        /// </summary>
        public int CompareCardTo(Card other) {
            //If this card is a trump and the other is not, this one is higher.
            if(this.IsTrump && !other.IsTrump) {
                return 1;
            }
            //If this card is not a trump and the other one is, the other one is higher.
            if(!this.IsTrump && other.IsTrump) {
                return -1;
            }
            //If both are of the same card type, the one with higher value wins
            if(this.CardType == other.CardType) {
                return this.Value.CompareTo(other.Value);
            }
            //Otherwise we can't tell since both cards are of different suits
            return 0;
        }
    }
}
