using System.Drawing;

namespace UlernGame.Model
{
    public class Door : Obstacle
    {
        public bool isOpen = false;
        public bool isLocked;
        
        public Door(int x, int y, bool locked)
        {
            X = x;
            Y = y;
            isLocked = locked;
        }

        public void OpenClose()
        {
            if (!isOpen)
                isOpen = true;
            else
                isOpen = false;
        }
    }
}