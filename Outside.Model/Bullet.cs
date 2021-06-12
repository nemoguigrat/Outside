using System.Numerics;

namespace Outside.Model
{
    public class Bullet : GameObject
    {
        private const int Speed = 15;
        private Direction Direction { get; }

        public Bullet(Player player)
        {
            Direction = player.Direction;
            X = player.X + player.Width / 2;
            Y = player.Y + player.Height / 2;
        }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.Right:
                    X += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Up:
                    Y -= Speed;
                    break;
            }
        }
    }
}