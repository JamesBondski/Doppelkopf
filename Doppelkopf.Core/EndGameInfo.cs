using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public class EndGameInfo
    {
        public Team WinningTeam {
            get { return this.winningTeam; }
        }

        public IEnumerable<Player> WinningPlayers {
            get { return this.round.Teams[this.winningTeam]; }
        }

        public int Points {
            get { return points; }
        }

        Round round;
        Dictionary<Team, int> teamPoints;
        Team winningTeam;
        int points;

        public EndGameInfo(Round round) {
            this.round = round;

            FindWinners();
            CalculatePoints();
        }

        private void CalculatePoints() {
            int winningPoints = this.teamPoints[this.winningTeam];

            this.points = 0;
            if (winningPoints > 120) {
                this.points++;
            }
            if(winningPoints > 150) {
                this.points++;
            }
            if(winningPoints > 180) {
                this.points++;
            }
            if(winningPoints > 210) {
                this.points++;
            }
            if(winningPoints == 240) {
                this.points++;
            }
        }

        private void FindWinners() {
            this.teamPoints = new Dictionary<Team, int>() {
                [Team.Re] = this.round.Teams[Team.Re].Select(player => player.RoundPoints).Sum(),
                [Team.Kontra] = this.round.Teams[Team.Kontra].Select(player => player.RoundPoints).Sum()
            };

            if(this.teamPoints[Team.Re] > this.teamPoints[Team.Kontra]) {
                this.winningTeam = Team.Re;
            }
            else {
                this.winningTeam = Team.Kontra;
            }
        }
    }
}
