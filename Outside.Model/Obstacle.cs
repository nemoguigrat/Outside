using System.Drawing;

namespace UlernGame.Model
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