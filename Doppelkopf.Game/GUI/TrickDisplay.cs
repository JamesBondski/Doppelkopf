using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppelkopf.Core;
using Microsoft.Xna.Framework;

namespace Doppelkopf.Client.GUI
{
    public class TrickDisplay : CardStackDisplay
    {
        private static readonly int spacing = 10;

        public int StartPlayer {
            get; set;
        }

        public TrickDisplay(ScreenComponent parent) : base(parent, new CardStack(), GetTrickArea()) {
            this.FixedCapacity = 4;
            this.Arrangement = ArrangementType.Diamond;
            this.Spacing = spacing;
            this.SpreadCards = false;

            this.PopulationComplete += TrickDisplay_PopulationComplete;
        }

        /// <summary>
        /// Sort the card displays so that the card of Player 0 is always the first CardDisplay in Children.
        /// </summary>
        private void TrickDisplay_PopulationComplete(object sender, EventArgs e) {
            for(int i=0; i<this.StartPlayer; i++) {
                CardDisplay lastDisplay = this.Children.OfType<CardDisplay>().Last();
                this.Children.Remove(lastDisplay);
                this.Children.Insert(0, lastDisplay);
            }
        }

        private static Rectangle GetTrickArea() {
            int width = (CardRenderer.DefaultRenderSize.X + spacing) * 3;
            int height = (CardRenderer.DefaultRenderSize.Y + spacing) * 2;

            return new Rectangle(DoppelkopfGame.Instance.GraphicsDevice.Viewport.Width / 2 - width / 2, DoppelkopfGame.Instance.GraphicsDevice.Viewport.Height / 2 - height / 2, width, height);
        }
    }
}
