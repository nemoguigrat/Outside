using System.Numerics;

namespace UlernGame.Model
{
    public class Bullet : GameObject
    {
        public const int speed = 100;
        public Directions Direction { get; }
        public Bullet(Player player)
        {
            X = player.X;
            Y = player.Y;
            Direction = player.Direction;
        }

        public void Destroy()
        {
            // при соприкосновении с объектом, пуля уничтожится
        }
    }
}