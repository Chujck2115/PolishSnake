using System;

namespace Snake
{
    class Apple
    {
        public Coord Position { get; private set; } = new Coord(1, 1);
        private Random random = new Random();

        public Apple(Board board)
        {
            Respawn(board, null); 
        }

        public void Respawn(Board board, Snake snake)
        {
            do
            {
                Position = new Coord(random.Next(1, board.Width - 1), random.Next(1, board.Height - 1));
            }
            while (board.Obstacles.Contains(Position) || (snake != null && snake.Body.Contains(Position)));
        }
    }
}
