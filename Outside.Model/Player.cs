namespace UlernGame.Model
{
    public class Player : Entity
    {
        public readonly int damage = 3;
        public readonly int maxAmmunition = 120;
        public readonly int fullMagazine = 15;
        private bool reload = false;
        private bool shoot = false;
        public int ammunition;
        public int magazine;
        
        public Player(int hp, int speed, int x, int y)
        {
            maxHeals = hp;
            heals = hp;
            posX = x;
            posY = y;
            this.speed = speed;
            magazine = fullMagazine;
            ammunition = maxAmmunition;
        }

        public void Reload()
        {
             var reload = fullMagazine - magazine;
             if (reload > 0 && ammunition - reload >= 0)
                ammunition -= reload;
             magazine += reload;
        }

        public void Shoot()
        {
            // создаст объект типа Bullet, с вектором направления 
        }
    }
}