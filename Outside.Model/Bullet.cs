using System.Numerics;

namespace UlernGame.Model
{
    public class Bullet : GameObject
    {
        public const int speed = 15;
        public Directions Direction { get; }
        public Bullet(Player player)
        {
            X = player.X + player.Hitbox.Width / 2;
            Y = player.Y + player.Hitbox.Height / 2;
            Direction = player.Direction;
        }

        public void Destroy()
        {
            // при соприкосновении с объектом, пуля уничтожится
        }

        public void Move()
        {
            switch (Direction)
            {
                case Directions.Right:
                    X += speed;
                    break;
                case Directions.Left:
                    X -= speed;
                    break;
                case Directions.Down:
                    Y += speed;
                    break;
                case Directions.Up:
                    Y -= speed;
                    break;
            }
        }
    }
}