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
    public enum Alignment
    {
        Min,
        Mid,
        Max
    }

    public class Label : ScreenComponent
    {
        public string Text {
            get; set;
        } = "";

        public string FontName {
            get; set;
        } = "Arial18";

        public Alignment HorizontalAlignment {
            get; set;
        } = Alignment.Min;

        public Alignment VerticalAlignment {
            get; set;
        } = Alignment.Min;

        public Label(ScreenComponent parent) : base(parent) {
        }

        public override void Draw(SpriteBatch batch) {
            base.Draw(batch);
            Vector2 pos = CalculatePosition();

            batch.DrawString(DoppelkopfGame.Instance.TextRenderer.Fonts[this.FontName], this.Text, pos, Color.White);
        }

        private Vector2 CalculatePosition() {
            Vector2 pos = new Vector2(this.ScreenArea.X, this.ScreenArea.Y);
            Vector2 size = DoppelkopfGame.Instance.TextRenderer.Fonts[this.FontName].MeasureString(this.Text);
            pos = AdjustHorizontalPosition(pos, size);
            pos = AdjustVerticalPosition(pos, size);
            return pos;
        }

        private Vector2 AdjustVerticalPosition(Vector2 pos, Vector2 size) {
            switch (this.VerticalAlignment) {
                case Alignment.Max:
                    pos.Y = ScreenArea.Bottom - size.Y;
                    break;
                case Alignment.Mid:
                    pos.Y = pos.Y + this.ScreenArea.Width / 2 - size.Y / 2;
                    break;
                case Alignment.Min:
                    break;
            }

            return pos;
        }

        private Vector2 AdjustHorizontalPosition(Vector2 pos, Vector2 size) {
            switch (this.HorizontalAlignment) {
                case Alignment.Max:
                    pos.X = ScreenArea.Right - size.X;
                    break;
                case Alignment.Mid:
                    pos.X = pos.X + this.ScreenArea.Width / 2 - size.X / 2;
                    break;
                case Alignment.Min:
                    break;
            }

            return pos;
        }
    }
}
