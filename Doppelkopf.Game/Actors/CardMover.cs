using Doppelkopf.Client.GUI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client.Actors
{
    public class CardMover
    {
        Point startLocation;
        Point endLocation;
        Point size;
        CardDisplay display;

        float speedX = 0;
        float speedY = 0;
        float curX = 0;
        float curY = 0;

        public bool Done {
            get; set;
        } = false;

        public CardMover(CardDisplay display, Point size, Point start, Point end, float animationTimeMs = 400) {
            this.display = display;
            this.startLocation = start;
            this.endLocation = end;
            this.size = size;

            this.curX = this.startLocation.X;
            this.curY = this.startLocation.Y;
            this.speedX = (this.endLocation.X - this.startLocation.X) / animationTimeMs;
            this.speedY = (this.endLocation.Y - this.startLocation.Y) / animationTimeMs;
        }

        public void Update(GameTime gameTime) {
            curX += speedX * gameTime.ElapsedGameTime.Milliseconds;
            curY += speedY * gameTime.ElapsedGameTime.Milliseconds;
            display.Area = new Rectangle((int)curX, (int)curY, size.X, size.Y);

            if (((this.speedX > 0 && this.endLocation.X < this.curX) || (this.speedX <= 0 && this.endLocation.X > this.curX) || this.speedX == 0)
                && ((this.speedY > 0 && this.endLocation.Y < this.curY) || (this.speedY <= 0 && this.endLocation.Y >= this.curY) || this.speedY == 0)) {
                this.Done = true;
            }
        }

        //These indicate, where the animation should start. 0=Top/Left, 50=Middle, 100=Bottom/Right
        private static Dictionary<int, Point> startLocations = new Dictionary<int, Point>() {
            [0] = new Point(50, 100),
            [1] = new Point(0, 50),
            [2] = new Point(50, 0),
            [3] = new Point(100, 50)
        };

        public static Point GetPlayerPosition(int playerID, Point size) {
            return new Point(GetStartX(DoppelkopfGame.Instance, playerID, size), GetStartY(DoppelkopfGame.Instance, playerID, size));
        }

        private static int GetStartX(DoppelkopfGame game, int playerID, Point size) {
            int pos = game.GraphicsDevice.Viewport.Width * startLocations[playerID].X / 100;
            if (pos > 0) {
                pos -= size.X / 2;
            }
            return pos;
        }

        private static int GetStartY(DoppelkopfGame game, int playerID, Point size) {
            int pos = game.GraphicsDevice.Viewport.Height * startLocations[playerID].Y / 100;
            if (pos > 0) {
                pos -= size.Y / 2;
            }
            return pos;
        }
    }
}
