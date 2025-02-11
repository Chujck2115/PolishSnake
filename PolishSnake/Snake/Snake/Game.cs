using System;
using System.Collections.Generic;
using System.IO;

namespace Snake
{
    class Game
    {
        private Snake snake;
        private Apple apple;
        private Board board;
        private int score;
        private int highScore;
        private string highScoreFile = "highscore.txt";
        private bool isPaused = false;
        private bool infiniteMode;
        private int frameDelayMilli;

        public Game(bool selectedInfiniteMode, int selectedFrameDelay)
        {
            board = new Board(50, 20);
            snake = new Snake(new Coord(10, 1));
            apple = new Apple(board);
            LoadHighScore();
            infiniteMode = selectedInfiniteMode;
            frameDelayMilli = selectedFrameDelay;
        }

        public void Run()
        {
            while (true)
            {
                HandleInput();

                if (isPaused)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Game Paused. Press 'P' to resume.");
                    while (isPaused)
                    {
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.P)
                        {
                            isPaused = false;
                        }
                    }
                    continue;
                }

                snake.Move();

                if (infiniteMode)
                {
                    HandleInfiniteMode();
                }

                if (snake.HasCollided(board, out bool hitObstacle))
                {
                    ResetGame();
                    continue;
                }

                if (snake.EatApple(apple))
                {
                    score++;
                    apple.Respawn(board, snake);
                    if (score > highScore)
                    {
                        highScore = score;
                        SaveHighScore();
                    }
                }

                RenderGame();
                DisplayScoreboard();

                DelayFrame();
            }
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        snake.ChangeDirection(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        snake.ChangeDirection(Direction.Right);
                        break;
                    case ConsoleKey.UpArrow:
                        snake.ChangeDirection(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        snake.ChangeDirection(Direction.Down);
                        break;
                    case ConsoleKey.P:
                        isPaused = true;
                        break;
                }
            }
        }

        private void HandleInfiniteMode()
        {
            Coord head = snake.Body[0];

            if (head.X < 1) head.X = board.Width - 2;
            if (head.X >= board.Width - 1) head.X = 1;
            if (head.Y < 1) head.Y = board.Height - 2;
            if (head.Y >= board.Height - 1) head.Y = 1;

            snake.Body[0] = head;
        }

        private void LoadHighScore()
        {
            if (File.Exists(highScoreFile))
            {
                int.TryParse(File.ReadAllText(highScoreFile), out highScore);
            }
        }

        private void SaveHighScore()
        {
            File.WriteAllText(highScoreFile, highScore.ToString());
        }

        private void ResetGame()
        {
            score = 0;
            snake = new Snake(new Coord(10, 1));
            apple = new Apple(board);
        }

        private void DelayFrame()
        {
            System.Threading.Thread.Sleep(frameDelayMilli);
        }

        private void RenderGame()
        {
            for (int y = 0; y < board.Height; y++)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Coord current = new Coord(x, y);
                    if (snake.Body.Contains(current))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("■");
                        Console.ResetColor();
                    }
                    else if (apple.Position.Equals(current))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("a");
                        Console.ResetColor();
                    }
                    else if (board.Obstacles.Contains(current))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("X");
                        Console.ResetColor();
                    }
                    else if (x == 0 || y == 0 || x == board.Width - 1 || y == board.Height - 1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
        }

        private void DisplayScoreboard()
        {
            Console.SetCursorPosition(0, board.Height + 1);
            Console.WriteLine($" Score: {score}  |  High Score: {highScore}  |  Mode: {(infiniteMode ? "Infinite" : "Normal")}  ");
        }
    }
}
