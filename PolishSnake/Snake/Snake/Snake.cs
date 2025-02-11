using System;
using System.Collections.Generic;

namespace Snake
{
    class Snake
    {
        public List<Coord> Body { get; private set; }
        private Direction direction;

        public Snake(Coord startPos)
        {
            Body = new List<Coord> { startPos };
            direction = Direction.Right;
        }

        public void Move()
        {
            Coord head = new Coord(Body[0].X, Body[0].Y);
            head.ApplyMovementDirection(direction);
            Body.Insert(0, head);
            Body.RemoveAt(Body.Count - 1);
        }

        public void ChangeDirection(Direction newDirection)
        {
            if ((direction == Direction.Left && newDirection != Direction.Right) ||
                (direction == Direction.Right && newDirection != Direction.Left) ||
                (direction == Direction.Up && newDirection != Direction.Down) ||
                (direction == Direction.Down && newDirection != Direction.Up))
            {
                direction = newDirection;
            }
        }

        public bool EatApple(Apple apple)
        {
            if (Body[0].Equals(apple.Position))
            {
                Body.Add(new Coord(Body[Body.Count - 1].X, Body[Body.Count - 1].Y));
                return true;
            }
            return false;
        }

        public bool HasCollided(Board board, out bool hitObstacle)
        {
            hitObstacle = board.Obstacles.Contains(Body[0]);
            return Body[0].X <= 0 || Body[0].Y <= 0 || Body[0].X >= board.Width || Body[0].Y >= board.Height || hitObstacle;
        }
    }
}
