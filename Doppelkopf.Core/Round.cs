using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class Round {
        CardStack deck;
        Game Game {
            get;
        }

        public List<Player> Players {
            get;
        }

        public Player StartPlayer {
            get; set;
        }

        public bool IsRunning {
            get {
                return this.Players[0].Hand.Cards.Count() > 0;
            }
        }

        public Round(Game game, Player startPlayer = null) {
            this.Game = game;
            this.deck = DeckBuilder.GetDoppelkopfDeck();
            this.Players = this.Game.Players;
            this.StartPlayer = startPlayer != null ? startPlayer : this.Players[0];

            //Need to clear all tricks from the players when a new round starts
            this.Players.ForEach(player => player.Tricks.Clear());

            this.Deal();
        }

        private void Deal() {
            this.deck.Shuffle();
            while(this.deck.Cards.Count > 0) {
                foreach(Player player in this.Players) {
                    this.deck.Cards[0].MoveTo(player.Hand);
                }
            }
            this.Players.ForEach(player => player.Hand.Sort());
        }

        public void Play() {
            while(IsRunning) {
                PlayTrick();
            }
        }

        public Trick PlayTrick() {
            if(!this.IsRunning) {
                throw new RoundFinishedException();
            }

            Queue<Player> trickQueue = new Queue<Player>();
            Trick trick = new Trick();
            for(int i=this.Players.IndexOf(this.StartPlayer); i<4; i++) {
                this.Players[i].Play(trick);
            }
            for (int i = 0; i<this.Players.IndexOf(this.StartPlayer); i++) {
                this.Players[i].Play(trick);
            }
            this.StartPlayer = trick.GetWinner();
            trick.GetWinner().Tricks.Add(trick);
            return trick;
        }
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
