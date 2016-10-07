using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    interface IPlayerActor
    {
        Player Player {
            get; set;
        }

        Card GetCardForTrick(Trick trick);
    }
}
