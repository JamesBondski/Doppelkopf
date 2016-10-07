using Doppelkopf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Console
{
    class ConsolePlayerActor : IPlayerActor
    {
        public Player Player {
            get; set;
        }

        public Card GetCardForTrick(Trick trick) {
            System.Console.Write("Your hand: ");
            foreach (Card card in this.Player.Hand.Cards) {
                System.Console.Write(card.Symbol + " ");
            }
            System.Console.WriteLine();

            System.Console.Write("Trick so far: ");
            foreach (Tuple<Player, Card> playedCard in trick.Played) {
                System.Console.Write(playedCard.Item1.Name + "->" + playedCard.Item2.Symbol + ", ");
            }
            System.Console.WriteLine();
            Card selectedCard = null;

            while (selectedCard == null) {
                System.Console.Write("Which card do you want to play? ");
                string cardSymbol = System.Console.ReadLine();
                selectedCard = this.Player.Hand.Cards.FirstOrDefault(card => card.Symbol == cardSymbol);
            }
            return selectedCard;
        }
    }
}
