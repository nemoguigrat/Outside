using System.Drawing;

namespace UlernGame.Model
{
    public class Obstacle : GameObject
    {
        public Obstacle()
        {
            Hitbox = new Rectangle(new Point(0,0), new Size(80,80));
        }
    }
}