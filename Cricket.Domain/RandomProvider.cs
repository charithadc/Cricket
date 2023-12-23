using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cricket.Domain
{
    public class RandomProvider
    {
        private static Random rnd;

        private RandomProvider()
        {
        }

        private static Random GetRandom()
        {
            if (rnd == null)
            {
                rnd = new Random();
            }
            return rnd;
        }

        public static double GetNextRnd()
        {
            return GetRandom().NextDouble()*100;
        }
    }
}
