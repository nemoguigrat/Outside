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
            switch (Direction)
            {
                case Direction.Down:
                    X = player.X + player.Width / 2 - 12;
                    Y = player.Y + player.Height / 2 + 12;
                    break;
                case Direction.Up:
                    X = player.X + player.Width / 2 + 8;
                    Y = player.Y + player.Height / 2 - 10;
                    break;
                case Direction.Left:
                    X = player.X + player.Width / 2 - 15;
                    Y = player.Y + player.Height / 2 - 12;
                    break;
                case Direction.Right:
                    X = player.X + player.Width / 2 + 12;
                    Y = player.Y + player.Height / 2 + 10;
                    break;
            }
               
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