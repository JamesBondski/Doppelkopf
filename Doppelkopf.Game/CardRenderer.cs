using Doppelkopf.Core;
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
    public class CardRenderer : GameComponent
    {
        public static Point DefaultRenderSize = new Point(100, 146);

        Dictionary<string, Texture2D> cardGraphics = new Dictionary<string, Texture2D>();

        string[] ranks = "N,T,J,Q,K,A".Split(',');
        string[] suits = "D,H,S,C".Split(',');

        string[] rankNames = "9,10,jack,queen,king,ace".Split(',');
        string[] suitNames = "diamonds,hearts,spades,clubs".Split(',');

        public CardRenderer(Microsoft.Xna.Framework.Game game) : base(game) {
            game.Components.Add(this);
        }

        public Point CardSize {
            get; set;
        }

        public void LoadContent(ContentManager content) {
            for (int rankIndex = 0; rankIndex < ranks.Length; rankIndex++) {
                for (int suitIndex = 0; suitIndex < suits.Length; suitIndex++) {
                    cardGraphics[suits[suitIndex] + ranks[rankIndex]] = content.Load<Texture2D>("Cards\\" + rankNames[rankIndex] + "_of_" + suitNames[suitIndex]);
                }
            }
            CardSize = new Point(cardGraphics.First().Value.Width, cardGraphics.First().Value.Height);
        }

        /// <summary>
        /// Draws a card within the specified rectangle. Card is scaled so it fits within that rectangle.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="batch"></param>
        /// <param name="targetArea"></param>
        public void Draw(Card card, SpriteBatch batch, Rectangle targetArea, Color color) {
            Rectangle actualArea = GetActualArea(targetArea);

            Texture2D cardTexture = cardGraphics[card.Symbol];
            batch.Draw(cardGraphics[card.Symbol], actualArea, color);
        }

        public Rectangle GetActualArea(Rectangle targetArea) {
            float scale = Math.Min(targetArea.Height / (float)CardSize.Y, targetArea.Width / (float)CardSize.X);
            return new Rectangle(
                            (int)(targetArea.X + targetArea.Width / 2 - CardSize.X * scale / 2),
                            (int)(targetArea.Y + targetArea.Height / 2 - CardSize.Y * scale / 2),
                            (int)(CardSize.X * scale),
                            (int)(CardSize.Y * scale)
                            );
        }

        public void Draw(Card card, SpriteBatch batch, Rectangle targetArea) {
            Draw(card, batch, targetArea, Color.White);
        }
    }
}
