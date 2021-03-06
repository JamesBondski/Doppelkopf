﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Doppelkopf.Core;
using Microsoft.Xna.Framework;

namespace Doppelkopf.Client.GUI
{
    public class CardDisplay : ScreenComponent
    {
        public Card Card {
            get; set;
        } = null;

        public bool IsSelected {
            get; set;
        }

        public CardDisplay(ScreenComponent parent) : base(parent) {
        }

        public override void Draw(SpriteBatch batch) {
            if(this.Card != null) {
                DoppelkopfGame.Instance.CardRenderer.Draw(this.Card, batch, this.ScreenArea, this.IsSelected ? Color.LightGreen : Color.White);
            }
            else {
                DoppelkopfGame.Instance.PrimitivesRenderer.DrawRectangle(batch, this.ScreenArea, Color.White);
                DoppelkopfGame.Instance.PrimitivesRenderer.DrawRectangle(batch, new Rectangle(this.ScreenArea.X + 1, this.ScreenArea.Y + 1, this.ScreenArea.Width - 2, this.ScreenArea.Height -2), Color.Blue);
            }

            base.Draw(batch);
        }

        protected override bool IsInArea(Point position) {
            return DoppelkopfGame.Instance.CardRenderer.GetActualArea(this.ScreenArea).Contains(position);
        }
    }
}
