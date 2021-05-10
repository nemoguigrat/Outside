using System.Drawing;
using Outside.Model;

namespace UlernGame.Model
{
    public class Medkit : Boosters
    {
        public const int healCount = 50;

        public Medkit(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}