namespace UlernGame.Model
{
    public class Thorn : Obstacle
    {
        public readonly int damage = 10;

        public Thorn(int x, int y)
        {
            posX = x;
            posY = y;
        }
    }
}