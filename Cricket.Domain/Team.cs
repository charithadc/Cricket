using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cricket.Domain
{
    public class Team
    {
        public List<Player> PlayerList { get; private set; }
        public Player BattingPlayer { get; private set; }
        public Player OtherSidePlayer { get; private set; }

        public string TeamName { get; private set; }    
        public int Runs {
            get
            {
                int runs = 0;
                foreach (Player p in PlayerList) 
                {
                    runs += p.Runs;
                }
                return runs;
            }
        }
        public int Balls
        {
            get
            {
                int balls = 0;
                foreach (Player p in PlayerList)
                {
                    balls += p.Balls;
                }
                return balls;
            }
        }

        public int Wickets
        {
            get
            {
                int wickets = 0;
                foreach (Player p in PlayerList)
                {
                    if(p.IsOut)
                    {
                        wickets++;
                    }
                }
                return wickets;
            }
        }
    
        public double RunRate { get { return Runs * 100 / Balls; } }

        private Sleeper _Sleeper;

        private IPrinter _Printer;

        public int Target { get; private set; }

        public Team(List<Player> playerList,string teamName, Sleeper sleeper, IPrinter printer) 
        { 
            if(playerList.Count!=11)
            {
                throw new Exception("Not Enough Players");
            }
            PlayerList = playerList;
            BattingPlayer = playerList[0];
            BattingPlayer.IsBatting = true;
            OtherSidePlayer = playerList[1];
            _Sleeper = sleeper;
            _Printer = printer;
            TeamName = teamName;
            Target = -1;
        }

        private void SwapBattingSide()
        {
            Player tempPlayer = OtherSidePlayer;
            OtherSidePlayer = BattingPlayer; 
            BattingPlayer = tempPlayer;
            BattingPlayer.IsBatting = true;
            OtherSidePlayer.IsBatting = false;
        }

        public void PlayInning(int target, int totalBalls)
        {
            Target = target;
            while(PlayBall(GetRequiredStrikeRate(Target,  totalBalls)))
            {

            }
        }

        private double GetRequiredStrikeRate(int target, int totalBalls)
        {
            if(target ==-1)
            {
                double remainingTotalPoints = 0;
                foreach (Player p in PlayerList)
                {
                    if (!p.IsOut && p!=OtherSidePlayer)
                    {
                        remainingTotalPoints += p.Points;
                    }
                }
                double strikeRate = Math.Sqrt(remainingTotalPoints/(totalBalls - Balls))*100;
                return strikeRate;
            }
            else
            {
                int remainingBalls = totalBalls - Balls;
                double strikeRate = (target+10-Runs) *100 / remainingBalls;
                return strikeRate;
            }
        }

        private bool PlayBall(double strikeRate)
        {
            if (Target != -1 && Runs> Target)
            {
                _Printer.PrintForEndInning(this);
                _Sleeper.SleepForEndInning();
                return false;
            }

            int runs = BattingPlayer.PlayABall(strikeRate);
            if (runs == -1)
            {
                _Printer.PrintForOut(this);
                BattingPlayer = GetNextBattingPlayer();
                if (BattingPlayer == null)
                {
                    _Printer.PrintForEndInning(this);
                    _Sleeper.SleepForEndInning();
                    return false;
                }
                else
                {                   
                    _Sleeper.SleepForOut();
                    BattingPlayer.IsBatting = true;
                }

            }
            else
            {
                _Printer.PrinFortBall(this,runs, strikeRate);
                _Sleeper.SleepForBall();
            }
            if (runs == 1)
            {
                SwapBattingSide();
            }
            bool isOverEnd = Balls % 6 == 0 ? true : false;
            if (isOverEnd)
            {
                _Printer.PrintForOver(this,strikeRate);
                SwapBattingSide();
            }
            return true;

        }

        private Player GetNextBattingPlayer()
        {
            foreach (var player in PlayerList) 
            { 
                if(!player.IsOut)
                {
                    if (player != BattingPlayer && player != OtherSidePlayer)
                    {
                        return player;
                    }
                }
            }
            return null;
        }
    }
}
