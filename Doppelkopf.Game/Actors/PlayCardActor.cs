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

        //These indicate, where the animation should start. 0=Top/Left, 50=Middle, 100=Bottom/Right
        Dictionary<int, Point> startLocations = new Dictionary<int, Point>() {
            [0] = new Point(50, 100),
            [1] = new Point(0, 50),
            [2] = new Point(50, 0),
            [3] = new Point(100, 50)
        };

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

            this.mover = new CardMover(display, size, new Point(GetStartX(game), GetStartY(game)), (this.game.CurrentScreen as MainScreen).TrickDisplay.Children.OfType<CardDisplay>().Where(display => display.Card == null).First().ScreenPosition, animationTimeMs);
        }

        private int GetStartX(DoppelkopfGame game) {
            int pos = game.GraphicsDevice.Viewport.Width * startLocations[this.action.Player.ID].X / 100;
            if (pos > 0) {
                pos -= size.X / 2;
            }
            return pos;
        }

        private int GetStartY(DoppelkopfGame game) {
            int pos = game.GraphicsDevice.Viewport.Height * startLocations[this.action.Player.ID].Y / 100;
            if (pos > 0) {
                pos -= size.Y / 2;
            }
            return pos;
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
