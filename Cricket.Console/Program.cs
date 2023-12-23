using Cricket.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cricket.Con
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OnePlayTest_6();
        }

        static void OnePlayTest_6()
        {
            List<Player> playerList = new List<Player>();
            PopulatePlayers(playerList);
            Sleeper sleeper = new Sleeper(500);
            IPrinter printer = new ConsolePrinter();
            Team teamA = new Team(playerList, "Sri Lanka", sleeper, printer);

            List<Player> playerList2 = new List<Player>();
            PopulatePlayers2(playerList2);
            Sleeper sleeper2 = new Sleeper(500);
            IPrinter printer2 = new ConsolePrinter();
            Team teamB = new Team(playerList2, "New Zealand", sleeper2, printer2);

            Match match = new Match(teamA, teamB,300);
            match.PlayInning();
            Console.WriteLine("Done Inning one with score - " + teamA.Runs);
            Console.WriteLine("Press any key to start inning 2");
            Console.ReadLine();


            match.PlayInning();
            Console.WriteLine("Done Inning two with score - " + teamB.Runs);
            Console.WriteLine("Press any key to End");
            Console.ReadLine();
        }

        static void OnePlayTest_5()
        {
            List<Player> playerList = new List<Player>();
            PopulatePlayers(playerList);
            Sleeper sleeper = new Sleeper(500);
            IPrinter printer = new ConsolePrinter();
            Team teamA = new Team(playerList,"Sri Lanka", sleeper, printer);
            teamA.PlayInning(-1, 120);
            Console.WriteLine("Done Inning one with score - "+ teamA.Runs);
            Console.WriteLine("Press any key to start inning 2");
            Console.ReadLine();
            List<Player> playerList2 = new List<Player>();
            PopulatePlayers2(playerList2);
            Sleeper sleeper2 = new Sleeper(500);
            IPrinter printer2 = new ConsolePrinter();
            Team teamB = new Team(playerList2,"New Zealand", sleeper2, printer2);
            teamB.PlayInning(teamA.Runs, 120);
            Console.WriteLine("Done Inning two with score - "+ teamB.Runs);
            Console.WriteLine("Press any key to End");
            Console.ReadLine();
        }

        static void OnePlayTest_4() 
        { 
            List<Player> playerList = new List<Player>();
            PopulatePlayers(playerList);
            Sleeper sleeper = new Sleeper(700);
            IPrinter printer = new ConsolePrinter();
            Team team = new Team(playerList,"Sri Lanka", sleeper, printer);
            team.PlayInning(-1, 300);
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void PopulatePlayers(List<Player> playerList)
        {
            playerList.Add(new Player( 100, "Charitha Dissa"));
            playerList.Add(new Player( 39, "Pathum Nissanka"));
            playerList.Add(new Player( 30, "Kusal Perera"));
            playerList.Add(new Player( 32, "Kusal Mendis"));
            playerList.Add(new Player( 39, "Sadeera Samarawickrama"));
            playerList.Add(new Player( 39, "Charith Asalanka"));
            //playerList.Add(new Player( 25, "Dhananjaya de Silva"));
            
            playerList.Add(new Player( 26, "Chamika Karunaratne"));
            playerList.Add(new Player( 17, "Dunith Wellalage"));
            playerList.Add(new Player( 14, "Maheesh Theekshana"));
            playerList.Add(new Player( 14, "Dilshan Madushanka"));
            playerList.Add(new Player( 5, "Lahiru Kumara"));

        }

        static void PopulatePlayers2(List<Player> playerList)
        {
            playerList.Add(new Player( 46, "Devon Conway"));
            playerList.Add(new Player( 41, "Will Young"));
            playerList.Add(new Player( 47, "Rachin Ravindra"));
            playerList.Add(new Player( 50, "Daryl Mitchell"));
            playerList.Add(new Player( 35, "Tom Latham"));
            playerList.Add(new Player( 35, "Glenn Phillips"));
            //playerList.Add(new Player( 25, "Dhananjaya de Silva"));

            playerList.Add(new Player( 33, "Mark Champman"));
            playerList.Add(new Player( 28, "MItchell Santner"));
            playerList.Add(new Player( 11, "Matt Henry"));
            playerList.Add(new Player( 7, "Lockie Ferguson"));
            playerList.Add(new Player( 9, "Trent Boult"));

        }

        static void PopulatePlayersBK(List<Player> playerList)
        {

            playerList.Add(new Player( 39, "Pathum Nissanka"));
            playerList.Add(new Player( 30, "Kusal Perera"));
            playerList.Add(new Player( 32, "Kusal Mendis"));
            playerList.Add(new Player( 39, "Sadeera Samarawickrama"));
            playerList.Add(new Player( 39, "Charith Asalanka"));
            //playerList.Add(new Player( 25, "Dhananjaya de Silva"));
            playerList.Add(new Player( 100, "Charitha Dissa"));
            playerList.Add(new Player( 26, "Chamika Karunaratne"));
            playerList.Add(new Player( 17, "Dunith Wellalage"));
            playerList.Add(new Player( 14, "Maheesh Theekshana"));
            playerList.Add(new Player( 14, "Dilshan Madushanka"));
            playerList.Add(new Player( 5, "Lahiru Kumara"));

        }


        static void OnePlayTest_3()
        {
            Console.WriteLine("This line will be cleared in 3 seconds...");
            Console.WriteLine("This line will be cleared in 3 seconds...");
            Thread.Sleep(2000); // Wait for 3 seconds

            // Clear the line by writing spaces over it
            Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");

            // Now you can write something new on the same line
            Console.Write("Line cleared! New content here.");
            Console.ReadLine();
        }

        static void OnePlayTest_2()
        {
            Random rnd = new Random();
            Player plyr = new Player( 45, "test");
            int totalRuns = 0;
            int totalBalls = 0;
            for (int i = 0; i < 100; i++)
            {
                int runs = 0;
                do
                {
                    runs = plyr.PlayABall(180);

                } while (runs != -1);
                totalRuns += plyr.Runs;
                totalBalls += plyr.Balls;
                double average = totalRuns / (i + 1);
                double strikeRate = totalRuns * 100 / totalBalls;
                Console.WriteLine("This match run- " + plyr.Runs + " Matches- " + (i + 1) + " Runs- " + totalRuns + " Average- " + average + " SR- " + strikeRate);
                plyr.Reset();
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void OnePlayTest()
        {
            Random rnd = new Random();
            Player plyr = new Player(45, "test");
            int runs = 0;
            do
            {
                runs = plyr.PlayABall(90);
                Console.WriteLine("Total Runs- " + plyr.Runs + "/ Balls- " + plyr.Balls + " This Ball Runs-" + runs);
                Thread.Sleep(100);
            } while (runs != -1);
            Console.WriteLine("OUT");
            Console.ReadLine();
        }
    }
}
