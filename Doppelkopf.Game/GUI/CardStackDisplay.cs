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
                stack = value;
                this.lastNumOfCards = -1;
            }
        }

        static Dictionary<ArrangementType, IArranger> arrangers = new Dictionary<ArrangementType, IArranger>() {
            [ArrangementType.Horizontal] = new HorizontalArranger()
        };

        public ArrangementType Arrangement {
            get { return arranger.Arrangement; }
            set { this.arranger = arrangers[value]; }
        }

        /// <summary>
        /// If set to true, the component will try to use the whole available area by spreading out the cards.
        /// </summary>
        public bool SpreadCards {
            get; set;
        }

        public event EventHandler<CardDisplayEventArgs> CardCreated;
        public event EventHandler<CardDisplayEventArgs> CardRemoved;

        private int lastNumOfCards = -1;
        private IArranger arranger;

        CardStack stack;

        public CardStackDisplay(ScreenComponent parent, CardStack stack, Rectangle area) : base(parent) {
            this.Area = area;
            this.Stack = stack;
            this.Arrangement = ArrangementType.Horizontal;
        }

        public override void Update(GameTime time) {
            if (this.Stack.Cards.Count != this.lastNumOfCards) {
                Repopulate();
                this.lastNumOfCards = this.Stack.Cards.Count;
            }

            base.Update(time);
        }

        /// <summary>
        /// Synchronize child elements with the associated CardStack and trigger rearrangement of cards
        /// </summary>
        protected void Repopulate() {
            //Remove cards that are in the display but not in the stack
            this.Children.OfType<CardDisplay>().Where(display => !this.Stack.Cards.Any(card => card == display.Card))
                .ToList().ForEach(display => {
                    if(this.CardRemoved != null) {
                        this.CardRemoved(this, new CardDisplayEventArgs() { Display = display });
                    }
                    this.Children.Remove(display);
                });

            //Add cards that are in the stack but not in the display
            this.Stack.Cards.Where(card => !this.Children.OfType<CardDisplay>().Any(display => display.Card == card))
                .ToList().ForEach(card => {
                    CardDisplay display = new CardDisplay(this) { Card = card };
                    if (this.CardCreated != null) {
                        this.CardCreated(this, new CardDisplayEventArgs() { Display = display });
                    }
                });

            this.arranger.Arrange(this);
        }
    }

    public class CardDisplayEventArgs : EventArgs
    {
        public CardDisplay Display { get; set; }
    }
}
