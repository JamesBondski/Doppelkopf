using Doppelkopf.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client.GUI
{
    public class MainScreen : Screen
    {

        TrickDisplay trickDisplay;
        public TrickDisplay TrickDisplay {
            get { return trickDisplay; }
        }

        public MainScreen(DoppelkopfGame game) : base(game.Input) {
            this.Area = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            new HandDisplay(this, game.Runner.Player, new Rectangle(0, game.GraphicsDevice.Viewport.Height - CardRenderer.DefaultRenderSize.Y - 20, game.GraphicsDevice.Viewport.Width, CardRenderer.DefaultRenderSize.Y));
            this.trickDisplay = new TrickDisplay(this);
        }

        

        public override void Update(GameTime time) {
            base.Update(time);
        }
    }
}
