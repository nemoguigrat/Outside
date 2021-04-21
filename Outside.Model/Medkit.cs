using System.Drawing;

namespace UlernGame.Model
{
    public class Medkit : GameObject
    {
        public const int healCount = 50;

        public Medkit(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}