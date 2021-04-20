using System.Drawing;

namespace UlernGame.Model
{
    public class Door : Obstacle
    {
        public bool isOpen = false;
        
        public Door(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}