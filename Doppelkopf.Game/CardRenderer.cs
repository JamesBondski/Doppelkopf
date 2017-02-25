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
    public class CardRenderer
    {
        Dictionary<string, Texture2D> cardGraphics = new Dictionary<string, Texture2D>();

        string[] ranks = "N,T,J,Q,K,A".Split(',');
        string[] suits = "D,H,S,C".Split(',');

        string[] rankNames = "9,10,jack,queen,king,ace".Split(',');
        string[] suitNames = "diamonds,hearts,spades,clubs".Split(',');

        public float Scale {
            get; set;
        } = 1;

        public CardRenderer(ContentManager content) {
            for(int rankIndex = 0; rankIndex<ranks.Length; rankIndex++) {
                for(int suitIndex = 0; suitIndex<suits.Length; suitIndex++) {
                    cardGraphics[suits[suitIndex] + ranks[rankIndex]] = content.Load<Texture2D>("Cards\\"+rankNames[rankIndex]+"_of_"+suitNames[suitIndex]);
                }
            }
        }

        public void Draw(Card card, SpriteBatch batch, Point target) {
            Texture2D cardTexture = cardGraphics[card.Symbol];
            Rectangle dst = new Rectangle(target.X, target.Y, (int)(cardTexture.Width * this.Scale), (int)(cardTexture.Height * this.Scale));
            batch.Draw(cardGraphics[card.Symbol], dst, Color.White);
        }
    }
}
