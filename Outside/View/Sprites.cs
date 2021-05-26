using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Outside.View
{
    public class Sprites
    {
        public Dictionary<string, Image> Player { get; }
        public Dictionary<string, Image> Monster { get; }
        public Image Wall { get; }
        public Image Medkit { get; }
        public Image AmmunitionCrate { get; }
        public Image OpenDoor { get; }
        public Image ClosedDoor { get; }
        public Image LockedDoor { get; }
        public Image Key { get; }

        public Sprites()
        {
            Player = LoadImagesEntity("survivor");
            Monster = LoadImagesEntity("zombie");
            Wall = LoadImage("wall");
            Medkit = LoadImage("medkit");
            AmmunitionCrate = LoadImage("ammo");
            OpenDoor = LoadImage("open_door");
            ClosedDoor = LoadImage("closed_door");
            LockedDoor = LoadImage("final_door");
            Key = LoadImage("key");
        }

        private Dictionary<string, Image> LoadImagesEntity(string file)
        {
            var result = new Dictionary<string, Image>();
            var directions = new[] {"Up", "Down", "Left", "Right"};
            foreach (var direction in directions)
            {
                result[direction] = new Bitmap(Image.FromFile("Resources/" + $"{file}{direction}.png"),
                    new Size(50, 50));
            }

            return result;
        }

        private Image LoadImage(string file) => Image.FromFile("Resources/" + file + ".png");
    }
}