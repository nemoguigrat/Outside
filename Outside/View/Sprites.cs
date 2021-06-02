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
        public Image OpenDoorV { get; }
        public Image ClosedDoor { get; }
        public Image LockedDoor { get; }
        public Image Key { get; }
        public Image Battery { get; }
        public Image OpenDoorH { get; }

        public Sprites()
        {
            Player = LoadImagesEntity("survivor");
            Monster = LoadImagesEntity("zombie");
            Wall = LoadImage("wall");
            Medkit = LoadImage("medkit");
            AmmunitionCrate = LoadImage("ammo");
            OpenDoorV = LoadImage("open_door_v");
            OpenDoorH = LoadImage("open_door_h");
            ClosedDoor = LoadImage("closed_door");
            LockedDoor = LoadImage("final_door");
            Key = LoadImage("key");
            Battery = LoadImage("battery");
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