using Doppelkopf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Console
{
    public abstract class Command
    {
        public abstract string Keyword {
            get;
        }

        public abstract void Execute(State state, string args);
    }

    public class NewGameCommand : Command
    {
        public override string Keyword {
            get {
                return "newgame";
            }
        }

        public override void Execute(State state, string args) {
            state.Game = new Game();
            state.Round = state.Game.NewRound();
            System.Console.WriteLine("New game started.");
        }
    }

    public class ShowHandsCommand : Command
    {
        public override string Keyword {
            get {
                return "showhands";
            }
        }

        public override void Execute(State state, string args) {
            foreach(Player player in state.Game.Players) {
                System.Console.Write(player.Name + ": ");
                foreach(Card card in player.Hand.Cards) {
                    System.Console.Write(card.Symbol + " ");
                }
                System.Console.WriteLine();
            }
        }
    }

    public class QuitCommand : Command
    {
        public override string Keyword {
            get {
                return "quit";
            }
        }

        public override void Execute(State state, string args) {
            state.DoQuit = true;
        }
    }

    public class PlayTrickCommand : Command
    {
        public override string Keyword {
            get {
                return "trick";
            }
        }

        public override void Execute(State state, string args) {
            Trick trick = state.Round.PlayTrick();
            foreach(Tuple<Player,Card> playedCard in trick.Played) {
                System.Console.Write(playedCard.Item1.Name + "->" + playedCard.Item2.Symbol + ", ");
            }
            System.Console.WriteLine();
        }
    }

    public class ControlPlayerCommand : Command
    {
        public override string Keyword {
            get {
                return "control";
            }
        }

        public override void Execute(State state, string args) {
            Player controlledPlayer = state.Game.Players.First(player => player.Name == args);
            controlledPlayer.Actor = new ConsolePlayerActor() { Player = controlledPlayer };
            System.Console.WriteLine("Controlling " + controlledPlayer.Name);
        }
    }
}
