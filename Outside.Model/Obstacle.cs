using System.Drawing;

namespace Outside.Model
{
    public class Obstacle : GameObject
    {
        public Obstacle()
        {
            Width = MapCreator.TileSize;
            Height = MapCreator.TileSize;
        }
    }
}