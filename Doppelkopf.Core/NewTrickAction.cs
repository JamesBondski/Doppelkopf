using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public class NewTrickAction : IAction
    {
        public string Name {
            get {
                return "NewTrick";
            }
        }

        public Player WinningPlayer {
            get; set;
        }

        public NewTrickAction() {
        }

        public void Do(Game game) {
            this.WinningPlayer = game.CurrentRound.CurrentTrick.GetWinner();
            this.WinningPlayer.Tricks.Add(game.CurrentRound.CurrentTrick);
            game.CurrentRound.StartPlayer = this.WinningPlayer;
            game.CurrentRound.CurrentTrick = new Trick();
        }
    }
}
