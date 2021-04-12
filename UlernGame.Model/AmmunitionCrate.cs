namespace UlernGame.Model
{
    public class AmmunitionCrate : GameObject
    {
        public readonly int ammoIn = 30;
        
        public AmmunitionCrate(int x, int y)
        {
            posX = x;
            posY = y;
        }
    }
}