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
        private static readonly int cardHeight = 175;
        private static readonly int cardWidth = (int)(175.0 / 726 * 500);

        TrickDisplay trickDisplay;
        public TrickDisplay TrickDisplay {
            get { return trickDisplay; }
        }

        public MainScreen(DoppelkopfGame game) : base(game.Input) {
            this.Area = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            new HandDisplay(this, game.Runner.Player, new Rectangle(0, game.GraphicsDevice.Viewport.Height - cardHeight - 20, game.GraphicsDevice.Viewport.Width, cardHeight));
            this.trickDisplay = new TrickDisplay(this, GetTrickArea(game));
        }

        private Rectangle GetTrickArea(DoppelkopfGame game) {
            int width = (cardWidth + 5) * 4;
            return new Rectangle(game.GraphicsDevice.Viewport.Width / 2 - width / 2, game.GraphicsDevice.Viewport.Height / 2 - cardHeight / 2, width, cardHeight);
        }

        public override void Update(GameTime time) {
            base.Update(time);
        }
    }
}
