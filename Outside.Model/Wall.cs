using System.Drawing;

namespace Outside.Model
{
    public class Wall : GameObject

    {
    public Wall(int x, int y)
    {
        X = x;
        Y = y;
        Width = MapCreator.TileSize;
        Height = MapCreator.TileSize;
    }
    }
}