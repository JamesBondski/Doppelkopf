using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Doppelkopf.Client.GUI
{
    public class Label : ScreenComponent
    {
        public string Text {
            get; set;
        }

        public string FontName {
            get; set;
        } = "Arial18";

        public Label(ScreenComponent parent) : base(parent) {
        }

        public override void Draw(SpriteBatch batch) {
            base.Draw(batch);

            batch.DrawString(DoppelkopfGame.Instance.TextRenderer.Fonts[this.FontName], this.Text, new Vector2(this.ScreenArea.X, this.ScreenArea.Y), Color.White);
        }
    }
}
