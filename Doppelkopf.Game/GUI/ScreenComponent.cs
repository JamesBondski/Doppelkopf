using Doppelkopf.Client.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client.GUI
{
    public abstract class ScreenComponent
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ScreenComponent Parent {
            get; set;
        }

        public List<ScreenComponent> Children {
            get; set;
        } = new List<ScreenComponent>();

        public Rectangle Area {
            get; set;
        }

        public string ID {
            get; set;
        }

        public bool Visible {
            get; set;
        } = true;

        public event EventHandler<MouseEventArgs> Click;

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
            this.Children.Where(child => child.Visible).ToList().ForEach(child => child.Draw(batch));
        }

        public virtual void Update(GameTime time) {
            this.Children.ForEach(child => child.Update(time));
        }

        public virtual void Activate() {
        }

        public virtual void Deactivate() {
        }

        bool clickInProgress = false;

        internal bool HandleMouseEvent(MouseEventArgs args) {
            bool handledByChild = false;
            this.Children.ForEach(child => {
                if (child.HandleMouseEvent(args)) {
                    handledByChild = true;
                }
            });

            bool isInArea = IsInArea(args.CurrentPosition);
            HandleLeftClick(args, handledByChild, isInArea);

            return handledByChild || isInArea;
        }

        protected virtual bool IsInArea(Point position) {
            return this.ScreenArea.Contains(position);
        }

        private void HandleLeftClick(MouseEventArgs args, bool handledByChild, bool isInArea) {
            if (isInArea) {
                //Click begun
                if (this.Click != null && !handledByChild && args.EventType == MouseEventType.Down && args.Button == MouseButton.Left && this.ScreenArea.Contains(args.CurrentPosition)) {
                    logger.Debug("Click begun...");
                    this.clickInProgress = true;
                }
            }

            if (clickInProgress && args.EventType == MouseEventType.Up && args.Button == MouseButton.Left) {
                //Click over, successful only if inside
                clickInProgress = false;
                if (isInArea && !handledByChild && this.Click != null) {
                    logger.Debug("Click!");
                    this.Click(this, args);
                }
            }
        }
    }
}
