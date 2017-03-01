using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client.Input
{
    public class InputManager : GameComponent
    {
        MouseState lastMouseState;

        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<MouseEventArgs> MouseMove;

        public InputManager(Game game) : base(game) {
            game.Components.Add(this);
        }

        public override void Initialize() {
            this.lastMouseState = Mouse.GetState();
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            MouseState currentMouseState = Mouse.GetState();

            UpdateMouse(currentMouseState);

            this.lastMouseState = currentMouseState;
            base.Update(gameTime);
        }

        private void UpdateMouse(MouseState currentMouseState) {
            if (this.MouseMove != null && (this.lastMouseState.Position.X != currentMouseState.Position.X || this.lastMouseState.Position.Y != currentMouseState.Position.Y)) {
                this.MouseMove(this, new MouseEventArgs(MouseEventType.Move, this.lastMouseState.Position, currentMouseState.Position));
            }

            if (this.MouseDown != null && (this.lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)) {
                this.MouseDown(this, new MouseEventArgs(MouseEventType.Down, this.lastMouseState.Position, currentMouseState.Position, MouseButton.Left));
            }

            if (this.MouseDown != null && (this.lastMouseState.RightButton == ButtonState.Released && currentMouseState.RightButton == ButtonState.Pressed)) {
                this.MouseDown(this, new MouseEventArgs(MouseEventType.Down, this.lastMouseState.Position, currentMouseState.Position, MouseButton.Right));
            }

            if (this.MouseUp != null && (this.lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)) {
                this.MouseUp(this, new MouseEventArgs(MouseEventType.Up, this.lastMouseState.Position, currentMouseState.Position, MouseButton.Left));
            }

            if (this.MouseUp != null && (this.lastMouseState.RightButton == ButtonState.Pressed && currentMouseState.RightButton == ButtonState.Released)) {
                this.MouseUp(this, new MouseEventArgs(MouseEventType.Up, this.lastMouseState.Position, currentMouseState.Position, MouseButton.Right));
            }
        }
    }
}
