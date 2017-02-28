using Doppelkopf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Doppelkopf.Client.GUI
{
    class HandDisplay : ScreenComponent
    {
        public Player Player {
            get;
        }

        public int Spacing {
            get; set;
        } = 5;

        private int lastNumOfCards = 0;

        public HandDisplay(ScreenComponent parent, Player player, Rectangle area) : base(parent) {
            this.Area = area;
            this.Player = player;
            this.Populate();
        }

        public override void Update(GameTime time) {
            if(this.Player.Hand.Cards.Count != this.lastNumOfCards) {
                this.Children.Clear();
                Populate();
            }

            base.Update(time);
        }

        private void Populate() {
            int effectiveWidth = this.Area.Width - this.Player.Hand.Cards.Count * this.Spacing;
            int cardWidth = (this.Area.Width - this.Player.Hand.Cards.Count * this.Spacing) / this.Player.Hand.Cards.Count;

            int curX = 0;
            foreach(Card card in this.Player.Hand.Cards) {
                this.Children.Add(new CardDisplay(this) { Card = card, Area = new Rectangle(curX, 0, cardWidth, this.Area.Height) });
                curX += cardWidth + this.Spacing;
            }
        }
    }
}
