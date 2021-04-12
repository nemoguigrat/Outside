namespace UlernGame.Model
{
    public class Entity : GameObject
    {
        public bool alive = true;
        public int maxHeals;
        public int speed;
        public int heals;
        public double rotationAngle;
        public void Die()
        {
            alive = false;
        }

        public void ReserveDamage(int damage)
        {
            if (heals > damage)
                heals -= damage;
            else
            {
                heals = 0;
                Die();
            }
        }
    }
}