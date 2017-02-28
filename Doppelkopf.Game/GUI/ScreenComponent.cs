using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client.GUI
{
    public abstract class ScreenComponent
    {
        public ScreenComponent Parent {
            get; set;
        }

        public List<ScreenComponent> Children {
            get; set;
        } = new List<ScreenComponent>();

        public Rectangle Area {
            get; set;
        }

        public Point ScreenPosition {
            get {
                if(this.Parent != null) {
                    return new Point(this.Parent.ScreenPosition.X + this.Area.Left, this.Parent.ScreenPosition.Y + this.Area.Top);
                }
                else {
                    return new Point(0, 0);
                }
            }
        }

        public Rectangle ScreenArea {
            get {
                return new Rectangle(this.ScreenPosition, new Point(this.Area.Width, this.Area.Height));
            }
        }

        public ScreenComponent(ScreenComponent parent) {
            if(parent != null) {
                this.Parent = parent;
                this.Parent.Children.Add(this);
            }
        }

        public virtual void Draw(SpriteBatch batch) {
            this.Children.ForEach(child => child.Draw(batch));
        }

        public virtual void Update(GameTime time) {
            this.Children.ForEach(child => child.Update(time));
        }
    }
}
