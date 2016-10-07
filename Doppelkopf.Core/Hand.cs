using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public class Hand : CardStack
    {
        public IEnumerable<Card> GetPlayableCards(Trick trick) {
            if(trick.Cards.Count == 0) {
                return this.Cards;
            }

            //Cards with same CardType
            var cardsSameSuit = this.Cards.Where(card => card.CardType == trick.Cards[0].CardType);
            if(cardsSameSuit.Count() > 0) {
                return cardsSameSuit;
            }

            return this.Cards;
        }
    }
}
