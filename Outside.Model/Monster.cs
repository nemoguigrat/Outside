using System.Drawing;

namespace UlernGame.Model
{
    public class Monster : GameObject
    {
        public const int damage = 20;
        public const int speed = 10;
        // public const int width = 75;
        // public const int height = 66;
        
        public Directions Direction { get; private set; }

        public Monster(int x, int y, Player player)
        {
            X = x;
            Y = y;
        }
    }
}