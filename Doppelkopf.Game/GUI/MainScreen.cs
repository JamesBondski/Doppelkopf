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
            new HandDisplay(this, game.Runner.Player, new Rectangle(0, game.GraphicsDevice.Viewport.Height - CardRenderer.DefaultRenderSize.Y - 40, game.GraphicsDevice.Viewport.Width, CardRenderer.DefaultRenderSize.Y));
            this.trickDisplay = new TrickDisplay(this);

            new Label(this) {
                Text = game.Runner.Game.Players[0].Name,
                Area = new Rectangle(0, game.GraphicsDevice.Viewport.Height - 30, game.GraphicsDevice.Viewport.Width, 20),
                HorizontalAlignment = Alignment.Mid,
                ID = "Player0Name"
            };

            new Label(this) {
                Text = game.Runner.Game.Players[1].Name,
                Area = new Rectangle(10, game.GraphicsDevice.Viewport.Height / 2 - CardRenderer.DefaultRenderSize.Y / 2 - 50, 150, 30),
                ID = "Player1Name"
            };

            new Label(this) {
                Text = game.Runner.Game.Players[2].Name,
                Area = new Rectangle(0, 10, game.GraphicsDevice.Viewport.Width, 30),
                HorizontalAlignment = Alignment.Mid,
                ID = "Player2Name"
            };

            new Label(this) {
                Text = game.Runner.Game.Players[3].Name,
                Area = new Rectangle(game.GraphicsDevice.Viewport.Width - 170, game.GraphicsDevice.Viewport.Height / 2 - CardRenderer.DefaultRenderSize.Y / 2 - 50, 150, 30),
                HorizontalAlignment = Alignment.Max,
                ID = "Player3Name"
            };
        }

        public override void Update(GameTime time) {
            base.Update(time);
        }
    }
}
