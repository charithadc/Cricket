using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cricket.Domain
{
    public class Player
    {
        public string Name { get;}
        public double Points { get;}
        public double Average { get; }

        public bool IsBatting { get;  set; }
        public int Runs
        {
            get
            {
                return _Runs;
            }
        }
        public int Balls
        {
            get
            {
                return _Balls;
            }
        }
        public bool IsOut 
        { 
            get
            {
                return _IsOout;
            } 
        }
        private bool _IsOout = false;
        private int _Balls = 0;
        private int _Runs = 0;


        public Player(double average, string name = "") 
        { 
            Average = average;
            Points = average * 0.9;
            Name = name; 
        }

        public void Reset()
        {
            _Runs = 0;
            _Balls = 0;
            _IsOout = false;
        }

        public int PlayABall(double strikeRate)
        {
            if(!IsBatting || IsOut)
            {
                throw new Exception("Player out or not batting");
            }
            
            int thisBallRuns = 0;
            double currentRand = GetNext();
            double outPosibility = (strikeRate * strikeRate) / (Points * 100);
            if (outPosibility >= currentRand)//check for out
            {
                _IsOout = true;
                IsBatting = false;
                thisBallRuns  = - 1;
            }
            else
            {
                currentRand = GetNext();
                if (currentRand <= (strikeRate / 2))//hit
                {
                    currentRand = GetNext();
                    if (currentRand > 90)//6
                    {
                        thisBallRuns = 6;
                    }
                    else if (currentRand > 79)//4
                    {
                        thisBallRuns = 4;
                    }
                    else if (currentRand > 49)//2
                    {
                        thisBallRuns = 2;
                    }
                    else if (currentRand > 9)//1
                    {
                        thisBallRuns = 1;
                    }
                    else //0
                    {
                        thisBallRuns = 0;
                    }
                }
            }

            _Balls++;
            if (thisBallRuns != -1)
            {
                _Runs += thisBallRuns;
            }

            return thisBallRuns;

        }

        private double GetNext()
        {
            return RandomProvider.GetNextRnd();
        }
    }
}
