namespace UlernGame.Model
{
    public class Bullet : GameObject
    {
        public int bulletSpeed = 100;

        public Bullet(Player player)
        {
            posX = player.posX;
            posY = player.posY;
        }

        public void Destroy()
        {
            
        }
    }
}