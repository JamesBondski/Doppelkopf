using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Doppelkopf.Core;

namespace Doppelkopf.Client.GUI
{
    public class CardDisplay : ScreenComponent
    {
        public Card Card {
            get; set;
        } = null;

        public CardDisplay(ScreenComponent parent) : base(parent) {
        }

        public override void Draw(SpriteBatch batch) {
            if(this.Card != null) {
                CardRenderer.Draw(this.Card, batch, this.ScreenArea);
            }

            base.Draw(batch);
        }
    }
}
