using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppelkopf.Core;
using Microsoft.Xna.Framework;
using Doppelkopf.Client.GUI;
using Doppelkopf.Core.Actions;

namespace Doppelkopf.Client.Actors
{
    public class NewTrickActor : IActionActor
    {
        NewTrickAction action;
        public IAction Action {
            get {
                return action;
            }
        }

        public bool Done {
            get; set;
        } = false;

        public DoppelkopfGame Game {
            get; set;
        }

        Label label;
        List<CardMover> movers = new List<CardMover>();

        public NewTrickActor(DoppelkopfGame game, IAction action) {
            this.action = (NewTrickAction)action;
            this.Game = game;
            game.Input.MouseDown += Input_MouseDown;

            Vector2 textDim = Game.TextRenderer.Fonts["Arial18"].MeasureString("Click to continue");
            
            label = new Label(this.Game.CurrentScreen) {
                Text = "Click to continue",
                FontName = "Arial18",
                Area = new Rectangle((int)(Game.GraphicsDevice.Viewport.Width / 2 - textDim.X / 2), Game.GraphicsDevice.Viewport.Height - 250, (int)textDim.X, (int)textDim.Y)
            };
        }

        private void Input_MouseDown(object sender, Input.MouseEventArgs e) {
            this.Game.Input.MouseDown -= Input_MouseDown;

            TrickDisplay trickDisplay = ((MainScreen)this.Game.CurrentScreen).TrickDisplay;
            foreach (CardDisplay cardDisplay in trickDisplay.Children.OfType<CardDisplay>()) {
                movers.Add(new CardMover(new CardDisplay(this.Game.CurrentScreen) { Card = cardDisplay.Card }, CardRenderer.DefaultRenderSize, cardDisplay.ScreenPosition, CardMover.GetPlayerPosition(this.action.WinningPlayer.ID, CardRenderer.DefaultRenderSize)));
            }

            trickDisplay.Stack = new CardStack();
            trickDisplay.StartPlayer = this.action.WinningPlayer.ID;
            
            label.Parent.Children.Remove(label);
        }

        public void Update(GameTime gameTime) {
            foreach(CardMover mover in this.movers) {
                mover.Update(gameTime);
            }

            if(movers.Any(mover => mover.Done)) {
                movers.ToList().ForEach(mover => {
                    this.Game.CurrentScreen.Children.Remove(mover.Display);
                    movers.Remove(mover);
                    
                });
                this.Done = true;
            }            
        }
    }
}
