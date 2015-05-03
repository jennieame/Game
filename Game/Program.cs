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
        private static System.Timers.Timer aTimer;
        static bool PlayGame = true;
        static int _countDown = 5;
        static string Name { get; set; }
        static string ChoosenLevel { get; set; }
        static int Score { get; set; }

        public static void Main()
        {

            Console.WriteLine(">>--- KNOW YOUR KEYBOARD ---<<");
            Console.Write("Enter your name: ");
            Name = Console.ReadLine().ToLower();
            Console.Clear();
            Console.Write("Choose Level: ");
            ChoosenLevel = Console.ReadLine().ToLower();

            while(true)
            {
                BeforeGame();
                Game(true, Name, ChoosenLevel);
                GameOver();
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

        static void Game(bool play, string name, string ChoosenLevel)
        {
            if (play)
            {
                Console.Clear();
                Score = 0;

                Console.WriteLine("Time:  {0}", Score);

                aTimer = new System.Timers.Timer(1000);
                aTimer.Elapsed += ClockEvent;
                aTimer.Enabled = true;
                string randomChar;

                while (PlayGame)
                {
                    if (ChoosenLevel == "m")
                    {
                        randomChar = Level.GetShortWord();
                    }
                    else if (ChoosenLevel == "h")
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
                        Score++;

                        Console.WriteLine("Score: {0}", Score);
                        Console.SetCursorPosition(2, 2);
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 2);
                    }
                }
            }
        }

        static void GameOver() 
        {
            aTimer.Stop();
            PlayGame = false;
            Console.Clear();
            Console.WriteLine(@"===============================================");
            Console.WriteLine("    _____      _____     ___   ____   ______    ");
            Console.WriteLine(@"   |  ___|    /     \   |   \ /   |  |  ___|   ");
            Console.WriteLine(@"   |  |  __  |  /_\  |  |    _    |  |  _|     ");
            Console.WriteLine(@"   |  |_| |  |  | |  |  |   | |   |  |  |__    ");
            Console.WriteLine( "   |______|  |__| |__|  |___| |___|  |_____|   ");
            Console.WriteLine();
            Console.WriteLine("     ______   ___    ___  _______   ______      ");
            Console.WriteLine(@"    |  __  | |   |  |   | |  ___|  |  __  |    ");
            Console.WriteLine(@"    | |  | |  \   \/   /  |  _|    |  |/  /    ");
            Console.WriteLine(@"    | |__| |   \      /   |  |__   |  |\  \    ");
            Console.WriteLine(@"    |______|    \____/    |_____|  |__| \__\   ");
            Console.WriteLine();
            Console.WriteLine("================================================");
            Console.WriteLine("      >>--- {0}: you got {1} points ---<<", Name, Score);

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
                _countDown = 5;
            }
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        static void ClockEvent(Object source, ElapsedEventArgs e)
        {

            if (PlayGame)
            {
                if (_countDown-- <= 0)
                {
                    GameOver();
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


}




