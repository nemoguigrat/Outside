

using System.Drawing;
using Outside.Model;

namespace UlernGame.Model
{
    public class AmmunitionCrate : Item
    {
        public const int ammoCount = 15;
        
        public AmmunitionCrate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}