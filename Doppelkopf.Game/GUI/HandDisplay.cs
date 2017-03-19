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
    class HandDisplay : CardStackDisplay
    {
        public Player Player {
            get;
        }
        
        public HandDisplay(ScreenComponent parent, Player player, Rectangle area) : base(parent, player.Hand, area) {
            this.CardCreated += HandDisplay_CardCreated;
            this.CardRemoved += HandDisplay_CardRemoved;

            this.Player = player;
            this.SpreadCards = false;

            this.Repopulate();
        }

        private void HandDisplay_CardRemoved(object sender, CardDisplayEventArgs e) {
            e.Display.Click -= OnCardClick;
        }

        private void HandDisplay_CardCreated(object sender, CardDisplayEventArgs e) {
            e.Display.Click += OnCardClick;
        }
        
        private void OnCardClick(object sender, Input.MouseEventArgs e) {
            foreach(CardDisplay child in this.Children.OfType<CardDisplay>()) {
                if (child == sender) {
                    if (child.IsSelected) {
                        (this.Player.Actor as ClientActor).LastPlayedCardPosition = child.ScreenPosition;
                        (this.Player.Actor as ClientActor).PlayCard(child.Card);
                        child.IsSelected = false;
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
