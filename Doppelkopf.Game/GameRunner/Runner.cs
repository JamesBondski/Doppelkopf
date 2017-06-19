using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Doppelkopf.Core;
using System.Collections.Concurrent;
using NLog;

namespace Doppelkopf.Client.GameRunner
{
    public class Runner
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

        public AutoResetEvent ActionHandled {
            get; set;
        } = new AutoResetEvent(false);

        public ClientActor Actor {
            get { return Player.Actor as ClientActor; }
        }

        public Runner(Game game = null, Player player = null) {
            this.dokoThread = new Thread(new ThreadStart(Run));

            this.Game = game != null ? game : new Game();
            this.Player = player != null ? player : this.Game.Players[0];

            //this.Player.Actor = new ClientActor(this.Player);

            this.Game.Action += Game_Action;
        }

        private void Game_Action(object sender, ActionEventArgs e) {
            logger.Debug("Action received: " + e.Action.ToString());
            this.Actions.Enqueue(e.Action);
            this.ActionHandled.WaitOne();
            logger.Debug("Action handled: " + e.Action.ToString());
        }

        public void Start() {
            logger.Info("Starting Doko thread...");
            this.dokoThread.Start();
        }

        public void Stop() {
            logger.Info("Stopping Doko thread...");
            this.dokoThread.Abort();
        }

        private void Run() {
            try {
                this.Game.Start(40);
            }
            catch(Exception ex) {
                logger.Error("Exception in Doko thread: " + ex.ToString());
            }
        }
    }
}
