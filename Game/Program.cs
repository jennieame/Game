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


            Console.WriteLine(">>--- KNOW YOUR KEYBOARD ---<<");

            Console.WriteLine("How do you want to play? ");
            Console.WriteLine("Tournament or regular game? (press t or r");
            GameType = Console.ReadKey(true);
            gameChar = GameType.KeyChar;

            // Console.WriteLine(gameChar); 

            if (GameType.KeyChar == 't')
            {
                Console.Clear();
                Console.WriteLine(" >>========== LET THE GAMES BEGIN ===========<<");
                Tournament.PlayersToTournament();
                Tournament.Separate();
            }
            else
            {

                Console.Write("\nEnter your name: ");
                string Name = Console.ReadLine().ToLower();
                player = new Player() { name = Name };

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Choose Level (e:easy, m:medium, h:hard | easy by defult): ");
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
                Console.WriteLine(">>--- START GAME ---<< \n (write y or yes)");
                string Play = Console.ReadLine();

                if (Play == "yes" || Play == "y")
                {
                    Console.Clear();
                    Console.WriteLine("Game starts in: ");
                    for (int i = 3; i > 0; i--)
                    {
                        ClearCurrentConsoleLine();
                        Console.Write(i);
                        Thread.Sleep(1000);
                    }
                    break;
                }

                else if (Play == "help" || Play == "h")
                {
                    Console.WriteLine("<<--- The Roules --->>");
                    Console.WriteLine("You got 30 sec, to write as many letters at possible.\n Just press write the same key as shows and press enter.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("I can't do that, sorry! Press enter to try again: ");
                    Console.ReadLine();
                }
            }
        }

        public static void Game(bool play, Player player, string ChoosenLevel)
        {
            Program.player = player;

            if (play)
            {
                Console.Clear();
                player.score = 0;

                Console.WriteLine("Time:  {0}", player.score);

                string randomChar;
                var gameEndsAt = DateTime.Now.AddSeconds(5);
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

                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine(randomChar);
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

                        answer = Console.ReadLine();
                    }
                    else
                    {
                        break;
                    }

                    if (randomChar == answer)
                    {
                        player.score++;

                        Console.WriteLine("Score: {0}", player.score);
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

            Thread.Sleep(1000);

            Console.WriteLine(gameChar);

            if (gameChar == 't')
            {
                PlayGame = true;

            }
            else
            {
                Console.WriteLine("\nPress enter to continue, q for exit...");
                string _startOver = Console.ReadLine();

                if (_startOver == "q")
                {
                    Console.WriteLine("Good bye!");
                    System.Environment.Exit(-1);
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


        public static void PlayersToTournament()
        {
            List<string> list = new List<string> {
                "Tobias", "Ervis", "Ulrik","Gustaf B", "Henrik", "Jens P",
                "Jonatan", "Karl","Lars","Gustav L", "Madeleine", "Mathias",
                "Max P","Fredrika", "Peter", "Robert", "Max S", "Emil", "Lovisa",
                "Patrik", "Robin", "John"
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


        }

        public static void Separate()
        {

            // The players: 
            var p1 = chosenPlayer.ElementAt(0);
            var p2 = chosenPlayer.ElementAt(1);
            var p3 = chosenPlayer.ElementAt(2);
            var p4 = chosenPlayer.ElementAt(3);

            Console.WriteLine("");

            Console.WriteLine("FIRST GAME: ");
            Console.WriteLine(" {0} vs. {1}", p1.name, p2.name);
            Console.WriteLine("");

            Console.WriteLine("SECOND GAME: ");
            Console.WriteLine(" {0} vs. {1}", p3.name, p4.name);

            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("FIRST UP: ");
            Console.WriteLine(" {0}", p1.name);
            Console.ReadLine();

            // Round 1: 
            for (int i = 0; i <= 1; i++)
            {
                Console.WriteLine(chosenPlayer.ElementAt(i).name);
                Program.Game(true, chosenPlayer.ElementAt(i), "e");

                Console.WriteLine(chosenPlayer.ElementAt(i).score);
                Console.WriteLine("ARE YOU READY?");
                Console.WriteLine(chosenPlayer.ElementAt(i + 1).name);
                Console.ReadLine();
            }

            for (int i = 2; i <= 3; i++)
            {
                Console.WriteLine(chosenPlayer.ElementAt(i).name);
                Program.Game(true, chosenPlayer.ElementAt(i), "e");

                Console.WriteLine(chosenPlayer.ElementAt(i).score);
                Console.WriteLine("ARE YOU READY?");
                Console.WriteLine(chosenPlayer.ElementAt(i + 1).name);
                Console.ReadLine();
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

