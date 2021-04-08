namespace UlernGame.Model
{
    public class Monster : Entity
    {
        public readonly int damage;

        public Monster(int hp, int speed, int damage)
        {
            maxHeals = hp;
            heals = hp;
            this.speed = speed;
            this.damage = damage;
        }
        
        public Monster(int hp, int speed, int damage, int x, int y)
        {
            maxHeals = hp;
            heals = hp;
            this.speed = speed;
            posX = x;
            posY = y;
            this.damage = damage;
        }
        
        
    }
}