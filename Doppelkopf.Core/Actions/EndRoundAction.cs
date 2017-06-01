using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core.Actions
{
    public class EndRoundAction : IAction
    {
        Round endedRound;

        public Round EndedRound {
            get { return this.endedRound; }
        }

        public string Name {
            get { return "EndRound"; }
        }

        public void Do(Game game) {
            this.endedRound = game.CurrentRound;
            this.endedRound.End();
            game.PreviousRounds.Add(this.endedRound);
        }
    }
}
