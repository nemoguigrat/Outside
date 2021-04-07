namespace UlernGame.Model
{
    public class Monster : Entity
    {
        public int Damage { get; set; }

        public Monster(int hp, int speed, int damage, int x, int y = default) : base(hp, speed, x, y)
        {
            Damage = damage;
        }
        
    }
}