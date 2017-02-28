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

        public MainScreen(DoppelkopfGame game) {
            this.Area = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            new HandDisplay(this, game.Game.Player, new Rectangle(0, game.GraphicsDevice.Viewport.Height - cardHeight - 20, game.GraphicsDevice.Viewport.Width, cardHeight));
        }
    }
}
