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

        public NewTrickAction() {
        }

        public void Do(Game game) {
            Player winningPlayer = game.CurrentRound.CurrentTrick.GetWinner();
            winningPlayer.Tricks.Add(game.CurrentRound.CurrentTrick);
            game.CurrentRound.StartPlayer = winningPlayer;
            game.CurrentRound.CurrentTrick = new Trick();
        }
    }
}
