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
    public class CardStackDisplay : ScreenComponent
    {
        public int Spacing {
            get; set;
        } = 5;

        public CardStack Stack {
            get {
                return stack;
            }

            set {
                ClearChildren();
                stack = value;
                Populate();
            }
        }

        public event EventHandler<CardDisplayEventArgs> CardCreated;
        public event EventHandler<CardDisplayEventArgs> CardRemoved;

        private int lastNumOfCards = -1;

        CardStack stack;

        public CardStackDisplay(ScreenComponent parent, CardStack stack, Rectangle area) : base(parent) {
            this.Area = area;
            this.Stack = stack;
        }

        public override void Update(GameTime time) {
            if (this.Stack.Cards.Count != this.lastNumOfCards) {
                Repopulate();
            }

            base.Update(time);
        }

        protected void Repopulate() {
            ClearChildren();
            Populate();
        }

        private void ClearChildren() {
            if (this.CardRemoved != null) {
                this.Children.OfType<CardDisplay>().ToList()
                    .ForEach(screenComponent => {
                        this.CardRemoved(this, new CardDisplayEventArgs() { Display = screenComponent as CardDisplay });
                    });
            }
            this.Children.Clear();
        }

        protected void Populate() {
            this.lastNumOfCards = this.Stack.Cards.Count;
            if (this.Stack.Cards.Count == 0) {
                return;
            }

            int effectiveWidth = this.Area.Width - this.Stack.Cards.Count * this.Spacing;
            int cardWidth = (this.Area.Width - this.Stack.Cards.Count * this.Spacing) / this.Stack.Cards.Count;

            int curX = 0;
            foreach (Card card in this.Stack.Cards) {
                CardDisplay display = new CardDisplay(this) { Card = card, Area = new Rectangle(curX, 0, cardWidth, this.Area.Height) };
                curX += cardWidth + this.Spacing;
                if (this.CardCreated != null) {
                    this.CardCreated(this, new CardDisplayEventArgs() { Display = display });
                }
            }
        }
    }

    public class CardDisplayEventArgs : EventArgs
    {
        public CardDisplay Display { get; set; }
    }
}
