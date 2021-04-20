
using System.Drawing;

namespace UlernGame.Model
{
    public class GameObject
    {
        public bool collision;
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public GameObject()
        {
            
        }

        public bool IsCollision(GameObject first, GameObject second)
        {
            return first.X == second.X && first.Y == second.Y;
        }
    }
}