using Doppelkopf.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public enum Team
    {
        Re,
        Kontra
    }

    public class Round {
        CardStack deck;
        Game Game {
            get;
        }

        public bool IsOver {
            get; set;
        } = false;

        public Trick CurrentTrick {
            get; set;
        }

        public event EventHandler<ActionEventArgs> Action;
        public event EventHandler<RoundEventArgs> Over;

        public List<IAction> Actions {
            get;
        } = new List<IAction>();

        public List<Player> Players {
            get;
        }

        public Dictionary<Team, List<Player>> Teams {
            get; set;
        } = new Dictionary<Team, List<Player>>();

        public Player StartPlayer {
            get; set;
        }

        public Round(Game game, Player startPlayer = null) {
            this.Game = game;
            this.deck = new CardStack();
            this.Players = this.Game.Players;
            this.StartPlayer = startPlayer != null ? startPlayer : this.Players[0];
            this.CurrentTrick = new Trick();

            this.Teams[Team.Kontra] = new List<Player>();
            this.Teams[Team.Re] = new List<Player>();

            //Move all cards from Tricks and Hands to the deck
            this.Players.ForEach(player => {
                player.Tricks.ForEach(trick => trick.Cards.ToList().ForEach(card => card.MoveTo(this.deck)));
                player.Tricks.Clear();
                player.Hand.Cards.ToList().ForEach(card => card.MoveTo(this.deck));
                });

            //If we don't have enough cards (i.e. at the beginning of a game), get a new deck
            if(this.deck.Cards.Count != 40) {
                this.deck = DeckBuilder.GetDoppelkopfDeck();
            }

            this.Deal();
        }

        public EndGameInfo End() {
            return new EndGameInfo(this);
        }

        private void Deal() {
            this.deck.Shuffle();
            while(this.deck.Cards.Count > 0) {
                foreach(Player player in this.Players) {
                    this.deck.Cards[0].MoveTo(player.Hand);
                }
            }
            this.Players.ForEach(player => player.Hand.Sort());

            //Players with the Queen of Clubs are the Re team
            this.Teams[Team.Re].Clear();
            this.Teams[Team.Kontra].Clear();

            this.Players.ForEach(player => {
                if(player.Hand.Cards.Any(card => card.Symbol == "CQ")) {
                    this.Teams[Team.Re].Add(player);
                }
                else {
                    this.Teams[Team.Kontra].Add(player);
                }
            });
        }

        public void DoAction(IAction action) {
            action.Do(this.Game);
            this.Actions.Add(action);
            if (this.Action != null) {
                this.Action(this, new ActionEventArgs() { Action = action, Round = this });
            }
        }

        public void CheckOver() {
            if (!this.IsOver && this.Players.All(player => player.Hand.Cards.Count == 0)) {
                this.IsOver = true;
                if (this.Over != null) {
                    this.Over(this, new RoundEventArgs() { Round = this });
                }
            }
        }

        public void Play() {
            while(!this.IsOver) {
                PlayTrick();
            }
        }

        public void PlayTrick() {
            if(this.IsOver) {
                throw new RoundFinishedException();
            }

            Queue<Player> trickQueue = new Queue<Player>();
            for(int i=this.Players.IndexOf(this.StartPlayer); i<4; i++) {
                this.Players[i].Play(this, this.CurrentTrick);
            }
            for (int i = 0; i<this.Players.IndexOf(this.StartPlayer); i++) {
                this.Players[i].Play(this, this.CurrentTrick);
            }

            if (!this.IsOver) {
                this.DoAction(new EndTrickAction());
                this.DoAction(new NewTrickAction());
            }
        }
    }


    public class ActionEventArgs : RoundEventArgs
    {
        public IAction Action { get; set; }
    }

    public class RoundEventArgs : EventArgs
    {
        public Round Round { get; set; }
    }

    [Serializable]
    public class RoundFinishedException : Exception
    {
        public RoundFinishedException() { }
        public RoundFinishedException(string message) : base(message) { }
        public RoundFinishedException(string message, Exception inner) : base(message, inner) { }
        protected RoundFinishedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
