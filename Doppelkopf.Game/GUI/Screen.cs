using Doppelkopf.Client.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client.GUI
{
    public class Screen : ScreenComponent
    {
        InputManager input;

        public Screen(InputManager input) : base(null) {
            this.input = input;
        }

        public override void Activate() {
            base.Activate();

            this.input.MouseDown += Input_MouseDown;
            this.input.MouseUp += Input_MouseUp;
            this.input.MouseMove += Input_MouseMove;
        }

        private void Input_MouseMove(object sender, MouseEventArgs e) {
            this.HandleMouseEvent(e);
        }

        private void Input_MouseUp(object sender, MouseEventArgs e) {
            this.HandleMouseEvent(e);
        }

        private void Input_MouseDown(object sender, MouseEventArgs e) {
            this.HandleMouseEvent(e);
        }

        public override void Deactivate() {
            base.Deactivate();

            this.input.MouseDown -= Input_MouseDown;
            this.input.MouseUp -= Input_MouseUp;
            this.input.MouseMove -= Input_MouseMove;
        }
    }
}
