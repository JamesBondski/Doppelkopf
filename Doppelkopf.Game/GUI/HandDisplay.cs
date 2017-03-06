using Doppelkopf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Doppelkopf.Client.GameRunner;

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
                ClearChildren();
                Populate();
            }

            base.Update(time);
        }

        private void ClearChildren() {
            this.Children.ForEach(child => child.Click -= OnCardClick);
            this.Children.Clear();
        }

        private void Populate() {
            int effectiveWidth = this.Area.Width - this.Player.Hand.Cards.Count * this.Spacing;
            int cardWidth = (this.Area.Width - this.Player.Hand.Cards.Count * this.Spacing) / this.Player.Hand.Cards.Count;

            int curX = 0;
            foreach(Card card in this.Player.Hand.Cards) {
                CardDisplay display = new CardDisplay(this) { Card = card, Area = new Rectangle(curX, 0, cardWidth, this.Area.Height) };
                display.Click += OnCardClick;
                curX += cardWidth + this.Spacing;
            }

            this.lastNumOfCards = this.Player.Hand.Cards.Count;
        }

        private void OnCardClick(object sender, Input.MouseEventArgs e) {
            foreach(CardDisplay child in this.Children.OfType<CardDisplay>()) {
                if (child == sender) {
                    if (child.IsSelected) {
                        (this.Player.Actor as ClientActor).PlayCard(child.Card);
                    }
                    else {
                        child.IsSelected = true;
                    }
                }
                else {
                    child.IsSelected = false;
                }
            }
        }
    }
}
