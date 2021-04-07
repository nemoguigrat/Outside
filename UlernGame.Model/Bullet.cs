namespace UlernGame.Model
{
    public class Bullet
    {
        public readonly int bulletSpeed = 100;
        public int PosX { get; }
        public int PosY { get; }

        public Bullet(Player player)
        {
            PosX = player.PosX;
            PosY = player.PosY;
        }

        public void Destroy()
        {
            
        }
    }
}