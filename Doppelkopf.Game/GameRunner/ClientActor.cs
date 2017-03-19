using Doppelkopf.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doppelkopf.Client.GameRunner
{
    public class ClientActor : IPlayerActor
    {
        public Player Player {
            get; set;
        }

        public bool WaitingForMove {
            get; set;
        } = false;

        private BlockingCollection<Card> cardsToPlay = new BlockingCollection<Card>();

        public IReadOnlyList<Card> Cards {
            get { return Player.Hand.Cards; }
        }

        public Point LastPlayedCardPosition {
            get; set;
        }

        public Card GetCardForTrick(Trick trick) {
            WaitingForMove = true;
            Card card = this.cardsToPlay.Take();
            WaitingForMove = false;
            return card;
        }

        public ClientActor(Player player) {
            this.Player = player;
        }

        public void PlayCard(Card card) {
            cardsToPlay.Add(card);
        }
    }
}
