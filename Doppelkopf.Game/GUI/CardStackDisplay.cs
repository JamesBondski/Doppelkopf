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

        public Point CardSize {
            get; set;
        }

        public int CardCount {
            get { return this.Children.OfType<CardDisplay>().Count(); }
        }

        public int FixedCapacity {
            get; set;
        } = -1;

        static Dictionary<ArrangementType, IArranger> arrangers = new Dictionary<ArrangementType, IArranger>() {
            [ArrangementType.Horizontal] = new HorizontalArranger(),
            [ArrangementType.Diamond] = new DiamondArranger()
        };

        public ArrangementType Arrangement {
            get { return arranger.Arrangement; }
            set {
                this.arranger = arrangers[value];
                this.CardSize = this.arranger.SuggestCardSize(this.Area);
            }
        }

        /// <summary>
        /// If set to true, the component will try to use the whole available area by spreading out the cards.
        /// </summary>
        public bool SpreadCards {
            get; set;
        } = true;

        public event EventHandler<CardDisplayEventArgs> CardCreated;
        public event EventHandler<CardDisplayEventArgs> CardRemoved;

        public event EventHandler<PopulationEventArgs> PopulationComplete;

        private int lastNumOfCards = -1;
        private IArranger arranger;

        CardStack stack;

        List<CardDisplay> displays = new List<CardDisplay>();

        public CardStackDisplay(ScreenComponent parent, CardStack stack, Rectangle area) : base(parent) {
            this.Area = area;
            this.Stack = stack;
            this.Arrangement = ArrangementType.Horizontal;
            this.CardSize = new Point(area.Width, area.Height);
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
        protected virtual void Repopulate() {
            if (this.lastNumOfCards == -1) {
                displays.Clear();
                this.Children.OfType<CardDisplay>().ToList().ForEach(cardDisplay => {
                    if (this.CardRemoved != null) {
                        this.CardRemoved(this, new CardDisplayEventArgs() { Display = cardDisplay });
                    }
                    this.Children.Remove(cardDisplay);
                });
            }

            int displayCount = this.FixedCapacity >= 0 ? this.FixedCapacity : this.Stack.Cards.Count;
            bool displaysChanged = false;

            //Make sure our display list has the same number of cards as our stack
            while (displays.Count > displayCount) {
                if (this.CardRemoved != null) {
                    this.CardRemoved(this, new CardDisplayEventArgs() { Display = displays[displays.Count - 1] });
                }
                displays.RemoveAt(displays.Count - 1);
                this.Children.Remove(this.Children.OfType<CardDisplay>().Last());
                displaysChanged = true;
            }
            while (displays.Count < displayCount) {
                displays.Add(new CardDisplay(this));
                if (this.CardCreated != null) {
                    this.CardCreated(this, new CardDisplayEventArgs() { Display = displays[displays.Count - 1] });
                }
                displaysChanged = true;
            }

            //Synchronize cards with card displays
            for (int i = 0; i < Math.Min(displayCount, this.Stack.Cards.Count); i++) {
                displays[i].Card = this.Stack.Cards[i];
            }

            if (PopulationComplete != null) {
                PopulationComplete(this, new PopulationEventArgs() { StackChanged = displaysChanged });
            }

            this.arranger.Arrange(this);
        }
    }

    public class CardDisplayEventArgs : EventArgs
    {
        public CardDisplay Display { get; set; }
    }

    public class PopulationEventArgs : EventArgs
    {
        public bool StackChanged { get; set; } = false;
    }
}
