using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppelkopf.Core;
using Microsoft.Xna.Framework;
using Doppelkopf.Client.GUI;

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

        public NewTrickActor(DoppelkopfGame game, IAction action) {
            this.action = (NewTrickAction)action;
            this.Game = game;
        }

        public void Update(GameTime gameTime) {
            ((MainScreen)this.Game.CurrentScreen).TrickDisplay.Stack = this.Game.Runner.Game.CurrentRound.CurrentTrick;
            Done = true;
        }
    }
}
