
using System.Drawing;

namespace UlernGame.Model
{
    public class GameObject
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Rectangle Hitbox { get; protected set; }
    }
}