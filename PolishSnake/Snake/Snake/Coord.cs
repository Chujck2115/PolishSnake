namespace Snake
{
    class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void ApplyMovementDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: Y--; break;
                case Direction.Down: Y++; break;
                case Direction.Left: X--; break;
                case Direction.Right: X++; break;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is Coord other)
                return X == other.X && Y == other.Y;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
