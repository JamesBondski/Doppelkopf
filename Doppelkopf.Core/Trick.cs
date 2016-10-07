﻿using System;
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

        public IReadOnlyList<Tuple<Player, Card>> Played {
            get { return PlayedCards.AsReadOnly(); }
        }

        public Trick() {
            this.PlayedCards = new List<Tuple<Player, Card>>();
        }

        protected internal override void AddCard(Card card, CardStack previousStack = null) {
            //We know this card must come from the hand of a player
            Trace.Assert(previousStack != null, "Card added to trick must come from a CardStack");
            Trace.Assert(previousStack.Owner != null, "Card added to trick must come from an owned CardStack.");

            //Check if this card was playable
            if(!previousStack.Owner.Hand.GetPlayableCards(this).Contains(card)) {
                throw new InvalidCardPlayedException("Card does not match suit.");
            }

            PlayedCards.Add(new Tuple<Player, Card>(previousStack.Owner, card));
            base.AddCard(card);
        }
    }

    [Serializable]
    public class InvalidCardPlayedException : Exception
    {
        public InvalidCardPlayedException() { }
        public InvalidCardPlayedException(string message) : base(message) { }
        public InvalidCardPlayedException(string message, Exception inner) : base(message, inner) { }
        protected InvalidCardPlayedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
