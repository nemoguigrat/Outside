namespace UlernGame.Model
{
    public class Player : Entity
    {
        private bool reload = false;
        private bool shoot = false;
        public readonly int damage = 3;
        public readonly int ammunition = 120;
        public int magazine = 10;
        public Player(int hp, int speed)
        {
            maxHeals = hp;
            heals = hp;
            this.speed = speed;
        }

        public Player(int hp, int speed, int x, int y)
        {
            maxHeals = hp;
            heals = hp;
            posX = x;
            posY = y;
            this.speed = speed;
        }

        public void Reload()
        {
            
        }

        public void Shoot()
        {
            
        }
    }
}