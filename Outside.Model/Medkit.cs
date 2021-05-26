using System.Drawing;
using Outside.Model;

namespace Outside.Model
{
    public class Medkit : Item
    {
        public const int HealCount = 50;

        public Medkit(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}