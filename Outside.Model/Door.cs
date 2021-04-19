namespace UlernGame.Model
{
    public class Door : Obstacle
    {
        public bool isOpen = false;
        
        public Door(int x, int y)
        {
            posX = x;
            posY = y;
        }
    }
}