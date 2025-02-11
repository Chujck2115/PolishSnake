using System;
using System.Collections.Generic;

namespace Snake
{
    class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Coord> Obstacles { get; private set; } = new List<Coord>();

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            GenerateObstacles();
        }

        private void GenerateObstacles()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                Obstacles.Add(new Coord(random.Next(1, Width - 1), random.Next(1, Height - 1)));
            }
        }

        public void Render(Snake snake, Apple apple)
        {
            Console.Clear();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
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
                    else if (Obstacles.Contains(current))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("X");
                        Console.ResetColor();
                    }
                    else if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
