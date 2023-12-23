using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cricket.Domain
{
    public class Sleeper
    {
        public int EndMatchSleepTime { get; private set; }
        public int EndInningSleepTime { get; private set; }
        public int OutSleepTime { get; private set; }
        public int BallSleepTime { get; private set; }  

        public Sleeper(int sleepTime)
        {
            EndMatchSleepTime = sleepTime;
            EndInningSleepTime = sleepTime;
            OutSleepTime = sleepTime;
            BallSleepTime = sleepTime;
        }

        public Sleeper(int endMatchSleepTime, int endInningSleepTime, int outSleepTime, int ballSleepTime)
        {
            EndMatchSleepTime = endMatchSleepTime;
            EndInningSleepTime = endInningSleepTime;
            OutSleepTime = outSleepTime;
            BallSleepTime = ballSleepTime;
        }

        public void SleepForEndMatch()
        {
            Thread.Sleep(EndMatchSleepTime);
        }

        public void SleepForEndInning()
        {
            Thread.Sleep(EndInningSleepTime);
        }

        public void SleepForOut()
        {
            Thread.Sleep(OutSleepTime); 
        }

        public void SleepForBall()
        {
            Thread.Sleep(BallSleepTime);
        }
    }
}
