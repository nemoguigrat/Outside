using System.Drawing;

namespace UlernGame.Model
{
    public class Monster : GameObject
    {
        public readonly int damage = 20;
        public readonly int speed = 10;

        public Monster(int x, int y, Player player)
        {
            X = x;
            Y = y;
        }
        
        
    }
}