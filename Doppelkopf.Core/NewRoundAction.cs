using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public class NewRoundAction : IAction
    {
        public string Name {
            get {
                return "NewRound";
            }
        }

        public void Do(Game game) {
            game.NewRound();
        }
    }
}
