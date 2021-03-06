﻿using Doppelkopf.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class Player {

        public IPlayerActor Actor {
            get; set;
        }

        public string Name {
            get; set;
        }

        public Hand Hand {
            get; set;
        }

        public int ID {
            get; set;
        }

        public List<Trick> Tricks {
            get;
        } = new List<Trick>();

        public int RoundPoints {
            get {
                return this.Tricks.Select(trick => trick.Points).Sum();
            }
        }

        public int GamePoints {
            get; set;
        } = 0;

        public Player(string name = null) {
            this.Name = name;
            this.Hand = new Hand() { Owner = this };
            this.Actor = new DefaultPlayerActor() { Player = this };
        }

        public void Play(Round round, Trick trick) {
            round.DoAction(new PlayCardAction(this, trick, this.Actor.GetCardForTrick(trick)));
        }
    }
}
