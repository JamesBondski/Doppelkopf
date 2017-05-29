using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core.Actions
{
    public class PlayCardAction : IAction {
        public string Name {
            get {
                return "PlayCard";
            }
        }

        public Player Player { get; }
        public Trick Trick { get; }
        public Card Card { get; }

        public PlayCardAction(Player player, Trick trick, Card card) {
            this.Player = player;
            this.Trick = trick;
            this.Card = card;
        }

        public void Do(Game game) {
            game.Players.First(player => player.Name == this.Player.Name)
                .Hand.Cards.First(card => card.Symbol == this.Card.Symbol)
                .MoveTo(game.CurrentRound.CurrentTrick);
        }
    }
}
