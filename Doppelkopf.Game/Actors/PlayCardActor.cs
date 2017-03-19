using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppelkopf.Core;
using Microsoft.Xna.Framework;
using Doppelkopf.Client.GUI;

namespace Doppelkopf.Client.Actors
{
    public class PlayCardActor : IActionActor
    {
        private static readonly Point size = new Point(120, 175);
        private static readonly float animationTimeMs = 400;
        
        PlayCardAction action;
        public IAction Action {
            get { return action; }
        }

        public bool Done {
            get { return this.mover.Done; }
        }

        DoppelkopfGame game;
        GUI.CardDisplay display;

        CardMover mover;

        public PlayCardActor(DoppelkopfGame game, IAction action) {
            this.action = action as PlayCardAction;
            this.game = game;

            display = new GUI.CardDisplay(this.game.CurrentScreen);
            display.Card = this.action.Card;

            this.mover = new CardMover(display, size, CardMover.GetPlayerPosition(this.action.Player.ID, size), (this.game.CurrentScreen as MainScreen).TrickDisplay.Children.OfType<CardDisplay>().Where(display => display.Card == null).First().ScreenPosition, animationTimeMs);
        }
        
        public void Update(GameTime gameTime) {
            mover.Update(gameTime);

            if (this.Done) {
                this.game.CurrentScreen.Children.Remove(display);
                this.action.Card.CopyTo((this.game.CurrentScreen as MainScreen).TrickDisplay.Stack);
            }
        }
    }
}
