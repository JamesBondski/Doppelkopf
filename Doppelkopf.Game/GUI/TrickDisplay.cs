using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppelkopf.Core;
using Microsoft.Xna.Framework;

namespace Doppelkopf.Client.GUI
{
    public class TrickDisplay : CardStackDisplay
    {
        public TrickDisplay(ScreenComponent parent, Rectangle area) : base(parent, new CardStack(), area) {
            this.FixedCapacity = 4;
        }
    }
}
