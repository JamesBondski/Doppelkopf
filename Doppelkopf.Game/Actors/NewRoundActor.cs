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
    public class NewRoundActor : IActionActor
    {
        public IAction Action { get; set; }

        bool done = false;
        public bool Done {
            get { return done; }
        }

        DoppelkopfGame game;

        public NewRoundActor(DoppelkopfGame game, IAction action) {
            this.game = game;
            this.Action = action;
        }

        public void Update(GameTime gameTime) {
            //Clear trick display
            TrickDisplay trickDisplay = ((MainScreen)this.game.CurrentScreen).TrickDisplay;
            trickDisplay.Stack = new CardStack();
            trickDisplay.StartPlayer = game.Runner.Game.CurrentRound.StartPlayer.ID;

            done = true;
        }
    }
}
