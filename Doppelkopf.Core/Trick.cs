using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public class Trick : CardStack
    {
        List<Tuple<Player, Card>> PlayedCards {
            get; set;
        }

        public Trick() {
            this.PlayedCards = new List<Tuple<Player, Card>>();
        }

        protected internal override void AddCard(Card card, CardStack previousStack = null) {
            //We know this card must come from the hand of a player
            Trace.Assert(previousStack != null, "Card added to trick must come from a CardStack");
            Trace.Assert(previousStack.Owner != null, "Card added to trick must come from an owned CardStack.");

            PlayedCards.Add(new Tuple<Player, Card>(previousStack.Owner, card));
            base.AddCard(card);
        }
    }
}
