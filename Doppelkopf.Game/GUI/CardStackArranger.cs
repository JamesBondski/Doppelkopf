using Doppelkopf.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client.GUI
{
    public enum ArrangementType
    {
        Horizontal
    }

    interface IArranger
    {
        ArrangementType Arrangement {
            get;
        }

        void Arrange(CardStackDisplay display);
    }

    class HorizontalArranger : IArranger
    {
        public ArrangementType Arrangement {
            get {
                return ArrangementType.Horizontal;
            }
        }

        public void Arrange(CardStackDisplay stackDisplay) {
            if (stackDisplay.Stack.Cards.Count == 0) {
                return;
            }

            int cardWidth = 0;
            int curX = 0;

            if (stackDisplay.SpreadCards) {
                cardWidth = (stackDisplay.Area.Width - stackDisplay.Stack.Cards.Count * stackDisplay.Spacing) / stackDisplay.Stack.Cards.Count;
            }
            else {
                cardWidth = stackDisplay.Area.Height * DoppelkopfGame.Instance.CardRenderer.CardSize.X / DoppelkopfGame.Instance.CardRenderer.CardSize.Y;
                curX = stackDisplay.Area.Width / 2 - cardWidth * stackDisplay.Stack.Cards.Count / 2;
            }
            
            foreach (CardDisplay display in stackDisplay.Children.OfType<CardDisplay>()) {
                display.Area = new Rectangle(curX, 0, cardWidth, stackDisplay.Area.Height);
                curX += cardWidth + stackDisplay.Spacing;
            }
        }
    }
}
