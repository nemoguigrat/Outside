using System.Drawing;

namespace Outside.Model
{
    public class Door : GameObject
    {
        public bool IsOpen { get; private set; }
        public bool IsLocked { get; }

        public Door(int x, int y, bool locked)
        {
            X = x;
            Y = y;
            IsLocked = locked;
            Width = MapCreator.TileSize;
            Height = MapCreator.TileSize;
        }

        public void OpenClose()
        {
            IsOpen = !IsOpen;
        }
    }
}