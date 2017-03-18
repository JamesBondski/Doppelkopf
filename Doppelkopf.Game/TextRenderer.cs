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
    public class TextRenderer : GameComponent
    {
        Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public TextRenderer(Game game) : base(game) {
            game.Components.Add(this);
        }

        public Dictionary<string, SpriteFont> Fonts {
            get {
                return fonts;
            }

            set {
                fonts = value;
            }
        }

        public void LoadContent(ContentManager content) {
            this.Fonts.Add("Arial18", content.Load<SpriteFont>("Arial18"));
        }
    }
}
