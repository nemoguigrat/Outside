using System;
using System.Drawing;
using System.Windows.Forms;

namespace UlernGame.View
{
    public class Sprite
    {
        public PictureBox spriteImage;
        public int Width { get; }
        public int Height { get; }

        public Sprite(string path)
        {
            spriteImage = new PictureBox();
            spriteImage.Image = Image.FromFile(path);
            Width = spriteImage.Size.Width;
            Height = spriteImage.Size.Height;
        }
    }
}