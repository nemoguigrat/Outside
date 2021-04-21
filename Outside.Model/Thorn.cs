using System.Drawing;

namespace UlernGame.Model
{
    public class Thorn : Obstacle
    {
        public const int damage = 10;

        public Thorn(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}