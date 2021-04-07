namespace UlernGame.Model
{
    public class Entity
    {
        public int Heals { get; set; }
        public int Speed { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public double RotationAngle { get; set; }

        public Entity(int hp, int speed, int x = default, int y = default)
        {
            Heals = hp;
            Speed = speed;
            PosX = x;
            PosY = y;
        }

        public void Die()
        {
            
        }

        public void ReserveDamage()
        {
            
        }
    }
}