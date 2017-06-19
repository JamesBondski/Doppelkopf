using Doppelkopf.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Client
{
    public class ActionController : GameComponent
    {
        IAction currentAction;
        IActionActor CurrentActor { get; set; }

        public ActionController(DoppelkopfGame game) : base(game) {
            game.Components.Add(this);
        }

        public override void Update(GameTime gameTime) {
            UpdateCurrentActor();

            this.CurrentActor?.Update(gameTime);
            base.Update(gameTime);
        }

        private void UpdateCurrentActor() {
            if (this.CurrentActor != null && this.CurrentActor.Done) {
                this.currentAction = null;
                this.CurrentActor = null;
                (this.Game as DoppelkopfGame).Runner.ActionHandled.Set();
            }

            if (this.currentAction == null
                && (this.Game as DoppelkopfGame).Runner.Actions.TryDequeue(out this.currentAction)) {
                StartNewActor();
            }
        }

        private void StartNewActor() {
            switch (currentAction.Name) {
                case "PlayCard":
                    this.CurrentActor = new Actors.PlayCardActor(this.Game as DoppelkopfGame, currentAction);
                    break;
                case "EndTrick":
                    this.CurrentActor = new Actors.EndTrickActor(this.Game as DoppelkopfGame, currentAction);
                    break;
                case "NewRound":
                    this.CurrentActor = new Actors.NewRoundActor(this.Game as DoppelkopfGame, currentAction);
                    break;
                default:
                    Console.WriteLine("Unhandled Action:  " + currentAction.Name);
                    this.currentAction = null;
                    (this.Game as DoppelkopfGame).Runner.ActionHandled.Set();
                    break;
            }
        }
    }

    public interface IActionActor
    {
        IAction Action { get; }
        bool Done { get; }

        void Update(GameTime gameTime);
    }
}
