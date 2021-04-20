using System.Drawing;

namespace UlernGame.Model
{
    public class Medkit : GameObject
    {
        public readonly int healCount = 50;

        public Medkit(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}