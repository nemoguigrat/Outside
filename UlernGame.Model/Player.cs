namespace UlernGame.Model
{
    public class Player : Entity
    {
        public readonly int damage = 3;
        
        public int Ammunition { get; set; }
        public int Magazine { get; set; }

        public Player(int hp, int speed, int x = default, int y = default) : base(hp, speed, x, y) { }

        public void Reload()
        {
            
        }

        public void Shoot()
        {
            
        }
    }
}