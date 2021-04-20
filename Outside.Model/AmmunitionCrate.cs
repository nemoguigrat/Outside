

using System.Drawing;

namespace UlernGame.Model
{
    public class AmmunitionCrate : GameObject
    {
        public readonly int ammoCount = 30;
        
        public AmmunitionCrate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}