using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Doppelkopf.Core;
using System.Collections.Concurrent;

namespace Doppelkopf.Client.GameRunner
{
    public class Runner
    {
        Thread dokoThread;

        public bool Running {
            get; set;
        }

        public Game Game {
            get;
        }

        public Player Player {
            get;
        }

        public ConcurrentQueue<IAction> Actions {
            get;
        } = new ConcurrentQueue<IAction>();

        public ClientActor Actor {
            get { return Player.Actor as ClientActor; }
        }

        public Runner(Game game = null, Player player = null) {
            this.dokoThread = new Thread(new ThreadStart(Run));

            this.Game = game != null ? game : new Game();
            this.Player = player != null ? player : this.Game.Players[0];

            this.Player.Actor = new ClientActor(this.Player);

            this.Game.Action += Game_Action;
        }

        private void Game_Action(object sender, ActionEventArgs e) {
            this.Actions.Enqueue(e.Action);
        }

        public void Start() {
            this.dokoThread.Start();
        }

        public void Stop() {
            this.dokoThread.Abort();
        }

        private void Run() {
            this.Game.Play();
        }
    }
}
