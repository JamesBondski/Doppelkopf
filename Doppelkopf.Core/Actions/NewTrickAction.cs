using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core.Actions
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
            game.CurrentRound.CheckOver();
            if (!game.CurrentRound.IsOver) {
                game.CurrentRound.CurrentTrick = new Trick();
            }
        }
    }
}
