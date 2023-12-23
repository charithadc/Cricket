using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cricket.Domain
{
    public class Match
    {
        public Team TeamA { get; private set; }
        public Team TeamB { get; private set; }

        public int Balls { get; private set; }  

        private bool _isFirstInning = true;

        public Match(Team teamA, Team teamB,int balls)
        {
            TeamA = teamA;
            TeamB = teamB;
            Balls = balls;
            Toss();
        }

        private void Toss()
        {
            if (!_isFirstInning)
            {
                throw new Exception("Inning already played");
            }
            else
            {
                if(RandomProvider.GetNextRnd()>=50)
                {
                    Team tempTeam = TeamA;
                    TeamA = TeamB;
                    TeamB= tempTeam; 
                }
            }
        }
        
        public  void PlayInning()
        {
            if(_isFirstInning)
            {
                TeamA.PlayInning(-1, Balls);
                _isFirstInning= false;
            }
            else
            {
                TeamB.PlayInning(TeamA.Runs, Balls);
            }
        }
    }
}
