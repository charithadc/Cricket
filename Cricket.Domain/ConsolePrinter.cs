using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cricket.Domain
{
    public class ConsolePrinter : IPrinter
    {
        private Player leftPlayer;
        private Player rightPlayer;
        private ConsolePrinterDTO GetPrintDetails(Team team)
        {
            int overs = team.Balls / 6;
            int remainingBallsInOver = team.Balls % 6;
            string overString = string.Format("{0}.{1}", overs, remainingBallsInOver);
            string fullScoreString = string.Format("{0}/{1}", team.Runs, team.Wickets);
            string fullMatchDetail = string.Format("{0} for {1} overs", fullScoreString, overString);
            string battingPlayerString = string.Empty;
            if (team.BattingPlayer != null)
            {
                battingPlayerString = string.Format("{0}- {1}({2})*", team.BattingPlayer.Name, team.BattingPlayer.Runs, team.BattingPlayer.Balls);
            }
            string ballingPlayerString = string.Format("{0}- {1}({2})", team.OtherSidePlayer.Name, team.OtherSidePlayer.Runs, team.OtherSidePlayer.Balls);
            
            string outPlayerString = "Following are the Out Players";
            foreach (var player in team.PlayerList)
            {
                if(player.IsOut)
                {
                    outPlayerString += "\n";
                    outPlayerString += string.Format("{0}- {1}({2})",player.Name, player.Runs,player.Balls);
                }
            }

            string allPlayerString = "Following are All Players";
            foreach (var player in team.PlayerList)
            {
                if (team.BattingPlayer!=null && team.BattingPlayer.IsOut ==false && team.BattingPlayer== player)
                {
                    allPlayerString += "\n";
                    allPlayerString += string.Format("{0}- {1}({2})*", player.Name, player.Runs, player.Balls);
                }
                else if(team.OtherSidePlayer != null && team.OtherSidePlayer.IsOut == false && team.OtherSidePlayer==player)
                {
                    allPlayerString += "\n";
                    allPlayerString += string.Format("{0}- {1}({2})*", player.Name, player.Runs, player.Balls);
                }
                else
                {
                    allPlayerString += "\n";
                    allPlayerString += string.Format("{0}- {1}({2})", player.Name, player.Runs, player.Balls);
                }
            }

            string targetString = string.Empty;
            if(team.Target!=-1)
            {
                targetString = string.Format("{0} Need {1} Runs to get {2}",team.TeamName, team.Target + 1 - team.Runs, team.Target);
            }

            ConsolePrinterDTO dto = new ConsolePrinterDTO();
            dto.battingPlayerString = battingPlayerString;
            dto.ballingPlayerString = ballingPlayerString;
            dto.fullMatchDetail = fullMatchDetail;
            dto.outPlayerString = outPlayerString;
            dto.allPlayerString = allPlayerString;
            dto.targetString = targetString;
            return dto;
        }

        public async Task ResetPrinter()
        {
            leftPlayer = null;
            rightPlayer = null;
        }
        public async Task PrinFortBall(Team team, int runs, double strikeRate)
        {
            if (leftPlayer == null && rightPlayer == null)//start of a match
            {
                leftPlayer = team.BattingPlayer;
                rightPlayer = team.OtherSidePlayer;
            }
            else if(leftPlayer == null)
            {
                if(rightPlayer == team.BattingPlayer)
                {
                    leftPlayer = team.OtherSidePlayer;
                }
                else
                {
                    leftPlayer = team.BattingPlayer;
                }
            }
            else if(rightPlayer ==null)
            {
                if(leftPlayer==team.BattingPlayer)
                {
                    rightPlayer = team.OtherSidePlayer;
                }
                {
                    rightPlayer = team.BattingPlayer;
                }
            }
            ClearSpace();
            string strikeRateString = strikeRate.ToString("0.00");
            ConsolePrinterDTO dto = GetPrintDetails(team);
            string printBallString = string.Empty;
            if (leftPlayer == team.BattingPlayer)
            {
                printBallString = string.Format("{0} Runs -- {1}, {2}, {3} Try for SR- {4}", runs, dto.battingPlayerString,
                    dto.ballingPlayerString, dto.fullMatchDetail, strikeRateString);
            }
            else
            {
                printBallString = string.Format("{0} Runs -- {1}, {2}, {3} Try for SR- {4}", runs,
                    dto.ballingPlayerString, dto.battingPlayerString, dto.fullMatchDetail, strikeRateString);
            }
            Console.Write(printBallString);
            
        }

        public async Task PrintForOver(Team team, double strikeRate)
        {
            ClearSpace();
            string strikeRateString = strikeRate.ToString("0.00");
            ConsolePrinterDTO dto = GetPrintDetails(team);
            Console.WriteLine("");
            if(team.Target!= -1)
            {
                Console.WriteLine(dto.targetString);
            }
            Console.WriteLine("EndOfOver " +team.Balls/6+" Run Rate is "+(team.RunRate/100)*6+" Try for SR- "+ strikeRateString);
            Console.WriteLine("{0}, {1}", dto.battingPlayerString, dto.ballingPlayerString);
            Console.WriteLine(dto.fullMatchDetail);
            Console.WriteLine(dto.outPlayerString);
            Console.WriteLine("");
        }

        private void ClearSpace()
        {
            // Clear the line by writing spaces over it
            Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
        }

        public async Task PrintForEndInning(Team team)
        {
            ClearSpace();
            ConsolePrinterDTO dto = GetPrintDetails(team);
            Console.WriteLine("");
            if (team.Target != -1)
            {
                Console.WriteLine(dto.targetString);
            }
            Console.WriteLine("End Inning!!! for "+dto.fullMatchDetail);
            Console.WriteLine(dto.allPlayerString);
        }

        public async Task PrintForEndMatch(Team team)
        {
            throw new NotImplementedException();
        }

        public async Task PrintForOut(Team team)
        {
            if(team.BattingPlayer == leftPlayer)
            {
                leftPlayer = null;
            }
            else
            {
                rightPlayer = null;
            }
            ClearSpace();
            ConsolePrinterDTO dto = GetPrintDetails(team);
            string printOutString = string.Format("{0} is OUT!!!", dto.battingPlayerString);
            Console.Write(printOutString);
        }

    }

    public class ConsolePrinterDTO
    {
        public string fullMatchDetail;
        public string battingPlayerString;
        public string ballingPlayerString;
        public string outPlayerString;
        public string allPlayerString;
        public string targetString;
    }
}
