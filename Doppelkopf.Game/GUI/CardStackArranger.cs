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
        Horizontal,
        Diamond
    }

    interface IArranger
    {
        ArrangementType Arrangement {
            get;
        }

        void Arrange(CardStackDisplay display);
        Point SuggestCardSize(Rectangle area);
    }

    class DiamondArranger : IArranger
    {
        public ArrangementType Arrangement {
            get {
                return ArrangementType.Diamond;
            }
        }

        public void Arrange(CardStackDisplay display) {
            var displays = display.Children.OfType<CardDisplay>().ToList();

            if(displays.Count != 4) {
                throw new ArgumentException("CardStackDisplays for diamond have to contain exactly 4 cards.");
            }

            //Positionen 0 (unten) und 2 (oben)
            //X-Koordinate ist zentriert
            int xPos02 = display.Area.Width / 2 - display.CardSize.X / 2;

            //Y-Koordinate für 0 ist entweder gemessen vom unteren Rand (wenn Spread true) oder vom Mittelpunkt
            int yPos0 = display.SpreadCards
                ? display.Area.Height - display.Spacing - display.CardSize.Y
                : display.Area.Height / 2 + display.Spacing;
            displays[0].Area = new Rectangle(xPos02, yPos0, display.CardSize.X, display.CardSize.Y);

            int yPos2 = display.SpreadCards
                ? display.Spacing
                : display.Area.Height / 2 - display.Spacing - display.CardSize.Y;
            displays[2].Area = new Rectangle(xPos02, yPos2, display.CardSize.X, display.CardSize.Y);

            //Positionen 1 (links) und 2 (rechts)
            int yPos13 = display.Area.Height / 2 - display.CardSize.Y / 2;

            int xPos1 = display.SpreadCards
                ? display.Spacing
                : display.Area.Width / 2 - display.Spacing - display.CardSize.X;
            displays[1].Area = new Rectangle(xPos1, yPos13, display.CardSize.X, display.CardSize.Y);

            int xPos3 = display.SpreadCards
                ? display.Area.Width - display.Spacing - display.CardSize.X
                : display.Area.Width / 2 + display.Spacing;
            displays[3].Area = new Rectangle(xPos3, yPos13, display.CardSize.X, display.CardSize.Y);
        }

        public Point SuggestCardSize(Rectangle area) {
            return CardRenderer.DefaultRenderSize;
        }
    }

    class HorizontalArranger : IArranger
    {
        public ArrangementType Arrangement {
            get {
                return ArrangementType.Horizontal;
            }
        }

        public void Arrange(CardStackDisplay stackDisplay) {
            if (stackDisplay.CardCount == 0) {
                return;
            }

            int cardWidth = 0;
            int curX = 0;

            if (stackDisplay.SpreadCards) {
                cardWidth = (stackDisplay.Area.Width - stackDisplay.CardCount * stackDisplay.Spacing) / stackDisplay.CardCount;
            }
            else {
                cardWidth = stackDisplay.Area.Height * DoppelkopfGame.Instance.CardRenderer.CardSize.X / DoppelkopfGame.Instance.CardRenderer.CardSize.Y;
                curX = stackDisplay.Area.Width / 2 - cardWidth * stackDisplay.CardCount / 2;
            }
            
            foreach (CardDisplay display in stackDisplay.Children.OfType<CardDisplay>()) {
                display.Area = new Rectangle(curX, 0, cardWidth, stackDisplay.Area.Height);
                curX += cardWidth + stackDisplay.Spacing;
            }
        }

        public Point SuggestCardSize(Rectangle area) {
            if(area.Height >= CardRenderer.DefaultRenderSize.Y) {
                return CardRenderer.DefaultRenderSize;
            }
            else {
                float scale = area.Height / (float)CardRenderer.DefaultRenderSize.Y;
                return new Point((int)(CardRenderer.DefaultRenderSize.X * scale), (int)(CardRenderer.DefaultRenderSize.Y * scale));
            }
        }
    }
}
