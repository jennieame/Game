using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Game
{
    class Program
    {
        static bool PlayGame = true;
        static string ChoosenLevel { get; set; }
        static Player player;

        static ConsoleKeyInfo GameType;
        static char gameChar { get; set; }


        public static void Main()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(">>--- KNOW YOUR KEYBOARD ---<<");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("HOW DO YOU WANT TO PLAY? ");
            Console.WriteLine("");
            Console.WriteLine(" >>  Tournament ( press T )");
            Console.WriteLine(" >>  Single player ( press any key )");

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine(" >> help (press h)");

            GameType = Console.ReadKey(true);
            gameChar = GameType.KeyChar;


            if (gameChar == 't' || gameChar == 'T')
            {
                Console.Clear();
                Console.WriteLine("\n>>========== LET THE GAMES BEGIN ===========<<");
                Tournament.PlayersToTournament();
                Tournament.TournamentGame();
            }
            else if (gameChar == 'h' || gameChar == 'H')
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine(">>--- THE RULES ---<<");
                Console.WriteLine();

                Console.WriteLine("You got 15 sec, to write as many words or letters as possible.\n Just press the same key or type the word that shows and press enter.");

                Console.WriteLine("");
                Console.WriteLine(">>--- SINGLE PLAYER ---<<");
                Console.WriteLine("Play against yourself and try to beat your own score");
                Console.WriteLine("");
                Console.WriteLine("<<--- TOURNAMENT --->>");
                Console.WriteLine("Four players, two rounds and one final. Who will be the ultimate master of keys?");
                Console.WriteLine();
                Console.WriteLine("Press enter to return ");
                Console.ReadLine();
                Console.Clear();
                Main();


            }
            else
            {
                Console.Clear();
                Console.Write("\nEnter your name: ");
                string Name = Console.ReadLine().ToLower();
                player = new Player() { name = Name };

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n  Hi {0} \n", player.name);

                    Console.WriteLine("  Choose Level: \n \n - e:easy, \n - m:medium, \n - h:hard  \n\n (easy by defult): ");
                    ChoosenLevel = Console.ReadLine().ToLower();

                    BeforeGame();
                    Game(true, player, ChoosenLevel);

                }
            }
        }

        public static void BeforeGame()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n>>--- START GAME ---<< \n (write y or yes)");
                string Play = Console.ReadLine();

                if (Play == "yes" || Play == "y")
                {
                    break;
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("\nI can't do that, sorry! Press enter to try again: ");
                    Console.ReadLine();
                }
            }
        }

        public static void CountDown()
        {
            Console.Clear();
            Console.WriteLine(" \n\n          Game starts in: \n");
            for (int i = 3; i > 0; i--)
            {
                ClearCurrentConsoleLine();
                Console.Write("                 " + i);
                Thread.Sleep(1000);
            }
        }

        public static void Game(bool play, Player player, string ChoosenLevel)
        {
            Program.player = player;

            if (play)
            {
                player.score = 0;

                CountDown();
                Console.Clear();

                Console.WriteLine("\nPlayer: {0} \n\n", player.name);

                string randomChar;
                var gameEndsAt = DateTime.Now.AddSeconds(15);
                var date = DateTime.Now;
                while (PlayGame && DateTime.Now < gameEndsAt)
                {
                    if (ChoosenLevel == "m" || ChoosenLevel == "medium")
                    {
                        randomChar = Level.GetShortWord();
                    }
                    else if (ChoosenLevel == "h" || ChoosenLevel == "hard")
                    {
                        randomChar = Level.GetLongWord();
                    }
                    else
                    {
                        randomChar = Level.GetLetter();
                    }

                    Console.SetCursorPosition(5, 4);
                    Console.WriteLine(randomChar);
                    Console.WriteLine("\n__________________\n");
                    var answer = "";

                    ClearCurrentConsoleLine();

                    while (!Console.KeyAvailable)
                    {

                        if (DateTime.Now >= gameEndsAt)
                        {
                            PlayGame = false;
                            break;
                        }
                    }

                    if (PlayGame)
                    {
                        Console.Write("     ");
                        answer = Console.ReadLine();
                    }
                    else
                    {
                        break;
                    }

                    if (randomChar == answer)
                    {
                        player.score++;
                        Console.WriteLine("\n \n Score: {0}", player.score);
                        Console.SetCursorPosition(2, 2);
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 2);
                    }
                }

                Console.Clear();
                GameOver(gameChar, player);
            }
        }

        public static void GameOver(char gameChar, Player player)
        {
            PlayGame = false;
            Console.Clear();
            Console.WriteLine(@"===============================================");
            Console.WriteLine("    _____      _____     ___   ____   ______    ");
            Console.WriteLine(@"   |  ___|    /     \   |   \ /   |  |  ___|   ");
            Console.WriteLine(@"   |  |  __  |  /_\  |  |    _    |  |  _|     ");
            Console.WriteLine(@"   |  |_| |  |  | |  |  |   | |   |  |  |__    ");
            Console.WriteLine("   |______|  |__| |__|  |___| |___|  |_____|   ");
            Console.WriteLine();
            Console.WriteLine("     ______   ___    ___  _______   ______      ");
            Console.WriteLine(@"    |  __  | |   |  |   | |  ___|  |  __  |    ");
            Console.WriteLine(@"    | |  | |  \   \/   /  |  _|    |  |/  /    ");
            Console.WriteLine(@"    | |__| |   \      /   |  |__   |  |\  \    ");
            Console.WriteLine(@"    |______|    \____/    |_____|  |__| \__\   ");
            Console.WriteLine();
            Console.WriteLine("================================================");
            Console.WriteLine("      >>--- You got {0} points ---<<", player.score);



            if (gameChar == 't')
            {
                Console.ReadLine();
                Console.ReadLine();
                Console.Clear();
                PlayGame = true;

            }
            else
            {
                Console.WriteLine("\n\n         PRESS ENTER TO TRY AGAIN \n\n\n >> exit (q) \n >> return to start (r)");
                string _startOver = Console.ReadLine();

                if (_startOver == "q" || _startOver == "Q")
                {
                    Console.WriteLine("Good bye!");
                    System.Environment.Exit(-1);
                }
                else if (_startOver == "r" || _startOver == "R")
                {
                    Main();
                }
                else
                {
                    PlayGame = true;
                }
            }
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

    }

    class Tournament
    {
        static Random _random = new Random();
        static List<Player> chosenPlayer = new List<Player> { };
        static bool tie = false;

        // Players
        static Player p1;
        static Player p2;
        static Player p3;
        static Player p4;


        public static void PlayersToTournament()
        {
            List<string> list = new List<string> {
                "Tobias", "Ulrik", "Gustaf B", "Henrik", "Jens P",
                "Jonatan", "Karl", "Gustav L", "Mathias",
                "Max P", "Fredrika", "Peter", "Max S", "Emil", "Lovisa",
                "Robin", "John", "Ervis", "Lars"
            };


            while (chosenPlayer.Count != 4)
            {
                var player = _random.Next(list.Count);
                bool match = chosenPlayer.Exists(x => x.name == list[player]);

                if (chosenPlayer.Count <= 3 && !match)
                {
                    chosenPlayer.Add(new Player() { name = list[player] });
                }
            }

            p1 = chosenPlayer.ElementAt(0);
            p2 = chosenPlayer.ElementAt(1);
            p3 = chosenPlayer.ElementAt(2);
            p4 = chosenPlayer.ElementAt(3);


        }

        public static void PlayRound(int i, string type)
        {

            Console.WriteLine("\n {0} ARE YOU READY?", chosenPlayer.ElementAt(i).name.ToUpper());
            Console.WriteLine("\n(press enter)");

            Console.ReadLine();

            if (type == "regular")
            {
                Program.Game(true, chosenPlayer.ElementAt(i), "e");
            }
            else if (type == "tie")
            {
                Program.Game(true, chosenPlayer.ElementAt(i), "m");
            }
            else
            {
                Program.Game(true, chosenPlayer.ElementAt(i), "h");
            }
        }

        public static void TournamentGame()
        {

            Console.WriteLine("");

            Console.WriteLine("\n====>> FIRST GAME <<====\n ");
            Console.WriteLine(" {0} vs. {1}", p1.name, p2.name);
            Console.WriteLine("");

            Console.WriteLine("\n====>> SECOND GAME <<====\n ");
            Console.WriteLine(" {0} vs. {1}", p3.name, p4.name);

            Console.ReadLine();

            Console.Clear();

            Console.WriteLine("\n ====>> FIRST UP <<==== \n");
            Console.WriteLine(" {0} vs. {1}", p1.name, p2.name);
            Console.ReadLine();

            // Round 1: 
            while (!p1.winner && !p2.winner)
            {
                for (int i = 0; i <= 1; i++)
                {
                    if (tie)
                    {
                        PlayRound(i, "tie");

                    }
                    else
                    {
                        PlayRound(i, "regular");
                    }
                }

                Console.WriteLine("\n   " + IsWinner(p1, p2));

            }

            Console.WriteLine("\n ====>> NEXT ROUND <<==== \n ");
            Console.WriteLine("   {0} vs. {1}", p3.name, p4.name);
            Console.ReadLine();

            while (!p4.winner && !p3.winner)
            {
                for (int i = 2; i <= 3; i++)
                {
                    if (tie)
                    {
                        PlayRound(i, "tie");

                    }
                    else
                    {
                        PlayRound(i, "regular");
                    }
                }

                Console.WriteLine(IsWinner(p3, p4));

            }


            Console.ReadLine();

            var winner1 = chosenPlayer.First(player => player.winner == true);
            var winner2 = chosenPlayer.Last(player => player.winner == true);


            Console.WriteLine("\n ====> LA GRANDE FINALE <<==== \n ");
            Console.WriteLine("{0} vs. {1}", winner1.name, winner2.name);

            Console.ReadLine();

            FinalGame(winner1, winner2);

            var superWinner = chosenPlayer.Find(player => player.winner == true);

            Console.WriteLine("\n AND THE WINNER IS:");
            Thread.Sleep(2000);
            Console.WriteLine("=================== \n");
            Console.WriteLine("   {0}", superWinner.name);
            Console.WriteLine("\n=================== \n");

            Console.ReadLine();
            //Thread.Sleep(2000);
            Program.Main();






        }

        static string IsWinner(Player player1, Player player2)
        {
            if (player1.score > player2.score)
            {
                player1.winner = true;
                player2.winner = false;
                tie = false;
                return player1.name + " WON";
            }
            else if (player1.score < player2.score)
            {
                player2.winner = true;
                player1.winner = false;
                tie = false;
                return player2.name + " WON";

            }
            else
            {
                tie = true;
                return "\n IT'S A TIE AND IT'S GOING TO GET HARDER \n\n          <<--- PLAY AGAIN --->> \n  ";
            }



        }

        static void FinalGame(Player player1, Player player2)
        {

            tie = true;
            while (tie)
            {

                Console.WriteLine("\n ====>> FIRST UP <<==== \n");
                Console.WriteLine(" {0} ", player1.name);
                Console.ReadLine();
                Program.Game(true, player1, "h");
                
                Console.WriteLine("\n ====>> NEXT UP <<==== \n");
                Console.WriteLine(" {0} ", player2.name);
                Console.ReadLine();
                Program.Game(true, player2, "h");

                IsWinner(player1, player2);

                if (tie)
                {
                    Console.WriteLine("\n IT'S A TIE - GO AGAIN");
                    Console.ReadLine();
                }

            }



        }

    }

    static class Level
    {
        static Random _random = new Random();

        public static string GetLetter()
        {
            //one letter, Level Easy
            int num = _random.Next(0, 26);
            char let = (char)('a' + num);
            string letToString = let.ToString();
            return letToString;
        }

        public static string GetShortWord()
        {
            //three letter word, Level Medium
            List<string> list = new List<string> { 
                "add", "age", "abs", "and", "buy", "big", "bud", "bin", "cop", "car", "cry", "cop", "css", "dad", "duh",
                "dot", "day", "die", "did", "ear", "end", "eve", "fan", "fat", "foe", "fit", "fun", "gag", "gym", "gun",
                "ham", "hat", "her", "him", "hit", "ice", "int", "job", "jam", "joy", "kid", "key", "lap", "law", "let",
                "low", "mac", "mad", "net", "naw", "ods", "one", "off", "php", "pay", "pop", "que", "rad", "row", "rum",
                "rug", "sad", "sit", "sql", "tan", "two", "top", "use", "var", "van", "war", "wow", "wig", "why", "yaw",
                "www", "yep", "yes", "zip", "zoo"
            };

            var shortWord = _random.Next(list.Count);
            return list[shortWord];
        }

        public static string GetLongWord()
        {
            //five letter word, Level Hard...
            List<string> list = new List<string> { 
                "aside", "arrow", "audio", "apple", "above", "buffy", "brain", "build", "bring", "cards", "caput", "camel",
                "carbs", "cabin", "darns", "dated", "dance", "delta", "extra", "event", "evoke", "error", "ester", "fakir",
                "faded", "fanny", "false", "games", "galax", "genre", "hacks", "hairy", "hawks", "heigh", "idiot", "icons",
                "issue", "jelly", "joins", "jokes", "kebab", "knows", "koala", "knife", "lable", "lower", "level", "macro",
                "mains", "major", "maker", "magic", "nerds", "noise", "noble", "obese", "oddly", "offer", "owner", "paint",
                "panda", "paris", "paper", "pearl", "query", "quack", "queen", "quick", "radio", "rainy", "range", "ranks",
                "saint", "sakes", "scale", "sassy", "table", "tails", "taken", "tango", "ultra", "urban", "units", "under",
                "vaild", "value", "video", "views", "words", "watch", "world", "yahoo", "yards", "yummy", "zebra", "zombi"
            };
            var shortWord = _random.Next(list.Count);
            return list[shortWord];
        }
    }

    class Player
    {

        public bool winner { get; set; }
        public string name { get; set; }
        public int score { get; set; }

    }


}


