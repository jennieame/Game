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
        static int Score { get; set; }

        //static bool hej = false;

        public static void Main()
        {
            Console.WriteLine(">>--- KNOW YOUR KEYBOARD ---<<");
            Console.Write("Enter your name: ");
            Name = Console.ReadLine().ToLower();
            Console.Clear();
            bool hej = false;


            while(true)
            {
                BeforeGame();
                Game(true, Name);
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

            Console.WriteLine("\nPress any key to continue, q for exit...");
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

        static void Game(bool play, string name)
        {
            if (play)
            {
                Console.Clear();
                Score = 0;

                Console.WriteLine("Time:  {0}", Score);

                aTimer = new System.Timers.Timer(1000);
                aTimer.Elapsed += ClockEvent;
                aTimer.Enabled = true;

                while (PlayGame)
                {
                    string randomChar = RandomLetter.GetLetter();
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

        private static void EndGame(Object source, ElapsedEventArgs e)
        {
            PlayGame = false;
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

    static class RandomLetter
    {
        static Random _random = new Random();
        public static string GetLetter()
        {
            int num = _random.Next(0, 26);
            char let = (char)('a' + num);
            string letToString = let.ToString();
            return letToString;
        }
    }



}




