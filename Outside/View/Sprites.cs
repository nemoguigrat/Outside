using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace UlernGame.View
{
    public class Sprites
    {
        public Dictionary<string, Image> Player { get; }
        public Dictionary<string, Image> Monster { get; }
        public Image Wall { get; }

        public Sprites()
        {
            Player = LoadImagesEntity("survivor");
            Monster = LoadImagesEntity("zombie");
        }

        private Dictionary<string, Image> LoadImagesEntity(string file)
        {
            var result = new Dictionary<string, Image>();
            var directions = new[] {"Up", "Down", "Left", "Right"};
            foreach (var direction in directions)
            {
                result[direction] = Image.FromFile("Resources/" + $"{file}{direction}.png");
            }

            return result;
        }

        private Image LoadImage(string file) => Image.FromFile("Resources/" + file);
    }
}