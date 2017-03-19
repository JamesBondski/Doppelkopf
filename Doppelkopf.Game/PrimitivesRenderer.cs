using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client
{
    public class PrimitivesRenderer : GameComponent
    {
        Texture2D pixel;

        public PrimitivesRenderer(Game game) : base(game) {
            game.Components.Add(this);
        }

        public void LoadContent(ContentManager content) {
            pixel = new Texture2D(this.Game.GraphicsDevice, 1, 1);
            Color[] data = new Color[1] { Color.White };
            pixel.SetData(data);
        }

        public void DrawRectangle(SpriteBatch batch, Rectangle area, Color color) {
            batch.Draw(pixel, area, color);
        }
    }
}
