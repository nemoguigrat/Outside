using System.Numerics;

namespace UlernGame.Model
{
    public class Bullet : GameObject
    {
        public int bulletSpeed = 100;
        public Bullet(Player player)
        {
            X = player.X;
            Y = player.Y;
        }

        public void Destroy()
        {
            // при соприкосновении с объектом, пуля уничтожится
        }
    }
}