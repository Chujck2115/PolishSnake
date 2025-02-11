using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Snake
{
    class Menu
    {
        private static bool difficultySet = false;
        private static bool modeSet = false;
        private static int selectedFrameDelay = 100;
        private static bool selectedInfiniteMode = false;

        public static void ShowMenu()
        {
            int highScore = LoadHighScore();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GRA W WĘŻA ===");
                Console.WriteLine("1. Rozpocznij grę" + (difficultySet && modeSet ? "" : " (Najpierw ustaw trudność i tryb!)"));
                Console.WriteLine("2. Wybierz trudność " + (difficultySet ? "( Ustawiono)" : ""));
                Console.WriteLine("3. Wybierz tryb " + (modeSet ? "( Ustawiono)" : ""));
                Console.WriteLine("4. Pokaż najlepszy wynik (" + highScore + ")");
                Console.WriteLine("5. Resetuj najlepszy wynik");
                Console.WriteLine("6. Wyjdź");
                Console.Write("Wybierz opcję: ");

                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        if (!difficultySet || !modeSet)
                        {
                            Console.WriteLine("Najpierw ustaw trudność i tryb!");
                            Console.ReadKey();
                        }
                        else
                        {
                            StartGame();
                        }
                        break;
                    case ConsoleKey.D2:
                        SetDifficulty();
                        break;
                    case ConsoleKey.D3:
                        SetMode();
                        break;
                    case ConsoleKey.D4:
                        Console.WriteLine("Najlepszy wynik: " + highScore);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D5:
                        ResetHighScore();
                        highScore = 0;
                        break;
                    case ConsoleKey.D6:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void StartGame()
        {
            Game game = new Game(selectedInfiniteMode, selectedFrameDelay);
            game.Run();

            Console.Clear();
            Console.WriteLine("=== KONIEC GRY ===");
            Console.WriteLine("Przegrałeś! Wciśnij ENTER, aby wrócić do menu.");
            Thread.Sleep(2000);
            Console.ReadLine();
            ShowMenu();
        }

        private static void SetDifficulty()
        {
            Console.Clear();
            Console.WriteLine("Wybierz trudność: \n1. Łatwy (150ms)\n2. Średni (100ms)\n3. Trudny (50ms)");
            ConsoleKey key = Console.ReadKey(true).Key;
            selectedFrameDelay = key switch
            {
                ConsoleKey.D1 => 150,
                ConsoleKey.D2 => 100,
                ConsoleKey.D3 => 50,
                _ => selectedFrameDelay
            };
            difficultySet = true;
        }

        private static void SetMode()
        {
            Console.Clear();
            Console.WriteLine("Wybierz tryb: \n1. Normalny \n2. InfinityLoop (możesz przechodzić przez ściany)");
            ConsoleKey key = Console.ReadKey(true).Key;
            selectedInfiniteMode = key == ConsoleKey.D2;
            modeSet = true;
        }

        private static int LoadHighScore()
        {
            if (File.Exists("highscore.txt"))
            {
                int.TryParse(File.ReadAllText("highscore.txt"), out int highScore);
                return highScore;
            }
            return 0;
        }

        private static void ResetHighScore()
        {
            File.WriteAllText("highscore.txt", "0");
        }
    }
}
