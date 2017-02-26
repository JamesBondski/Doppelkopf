using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client.Input
{
    public enum MouseButton
    {
        Left,
        Right
    }

    public class MouseEventArgs : EventArgs
    {
        public Point CurrentPosition {
            get;
        }

        public Point LastPosition {
            get;
        }

        public Point PositionDelta {
            get;
        }

        public MouseButton Button {
            get;
        }

        public MouseEventArgs(Point lastPosition, Point currentPosition) {
            this.CurrentPosition = currentPosition;
            this.LastPosition = lastPosition;
            this.PositionDelta = new Point(currentPosition.X - lastPosition.X, currentPosition.Y - lastPosition.Y);
        }

        public MouseEventArgs(Point lastPosition, Point currentPosition, MouseButton button) : this(lastPosition, currentPosition) {
            this.Button = button;
        }
    }
}
