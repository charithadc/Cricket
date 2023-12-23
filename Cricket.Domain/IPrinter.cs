using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cricket.Domain
{
    public interface IPrinter
    {
        Task PrinFortBall(Team team, int runs, double strikeRate);
        Task PrintForOver(Team team, double strikeRate);
        Task PrintForOut(Team team);
        Task PrintForEndInning(Team team);
        Task PrintForEndMatch(Team team);
        Task ResetPrinter();
    }
}
