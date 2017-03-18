using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppelkopf.Core;
using Microsoft.Xna.Framework;

namespace Doppelkopf.Client.Actors
{
    public class PlayCardActor : IActionActor
    {
        PlayCardAction action;
        public IAction Action {
            get { return action; }
        }

        public bool Done {
            get; set;
        } = false;

        DoppelkopfGame game;
        GUI.CardDisplay display;
        
        public PlayCardActor(DoppelkopfGame game, IAction action) {
            this.action = action as PlayCardAction;
            this.game = game;

            //Find out start location of card (Player's hand)
            //Find out end location of card (Trick display)
            display = new GUI.CardDisplay(this.game.CurrentScreen);
            display.Area = new Rectangle(50, 50, 100, 100);
            display.Card = this.action.Card;
        }

        bool goingRight = true;

        public void Update(GameTime gameTime) {
            if(goingRight) {
                display.Area = new Rectangle((int)(display.Area.X + 1 * gameTime.ElapsedGameTime.Milliseconds / 1.5), display.Area.Y, display.Area.Width, display.Area.Height);
                if(display.Area.X > this.game.GraphicsDevice.Viewport.Width-400) {
                    this.Done = true;
                }
            }

            if(this.Done) {
                this.game.CurrentScreen.Children.Remove(display);
            }
        }
    }
}
