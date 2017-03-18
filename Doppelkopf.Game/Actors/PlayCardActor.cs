﻿using System;
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
        private static readonly Point size = new Point(120, 175);
        private static readonly float animationTimeMs = 500;

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
            get; set;
        } = false;

        Point startLocation;
        Point endLocation;
        float speedX = 0;
        float speedY = 0;
        float curX = 0;
        float curY = 0;

        DoppelkopfGame game;
        GUI.CardDisplay display;

        public PlayCardActor(DoppelkopfGame game, IAction action) {
            this.action = action as PlayCardAction;
            this.game = game;

            //Find out start location of card (Player's hand)
            this.startLocation = new Point(
                GetStartX(game),
                GetStartY(game)
            );

            this.endLocation = new Point(
                game.GraphicsDevice.Viewport.Width / 2 - size.X /2,
                game.GraphicsDevice.Viewport.Height / 2 - size.Y / 2
            );

            this.curX = this.startLocation.X;
            this.curY = this.startLocation.Y;
            this.speedX = (this.endLocation.X - this.startLocation.X) / animationTimeMs;
            this.speedY = (this.endLocation.Y - this.startLocation.Y) / animationTimeMs;

            //Find out end location of card (Trick display)
            display = new GUI.CardDisplay(this.game.CurrentScreen);
            display.Area = new Rectangle(this.startLocation, size);
            display.Card = this.action.Card;
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
            curX += speedX * gameTime.ElapsedGameTime.Milliseconds;
            curY += speedY * gameTime.ElapsedGameTime.Milliseconds;
            display.Area = new Rectangle((int)curX, (int)curY, size.X, size.Y);

            if(((this.speedX > 0 && this.endLocation.X < this.curX) || (this.speedX <= 0 && this.endLocation.X > this.curX) || this.speedX == 0)
                && ((this.speedY > 0 && this.endLocation.Y < this.curY) || (this.speedY <= 0 && this.endLocation.Y >= this.curY) || this.speedY == 0)) {
                this.Done = true;
            }

            if (this.Done) {
                this.game.CurrentScreen.Children.Remove(display);
            }
        }
    }
}
