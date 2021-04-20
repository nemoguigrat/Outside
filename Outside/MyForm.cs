using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using UlernGame.Model;
using UlernGame.View;

namespace UlernGame
{
    public partial class MyForm : Form
    {
        public readonly Game game;
        public readonly Sprites sprites = new Sprites();
        public MyForm()
        {
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            game = new Game();
        }

        protected override void OnKeyDown(KeyEventArgs key)
        {
            game.Player.Move(key.KeyData);
            game.KeyPressed = key.KeyData;
        }

        protected override void OnPaint(PaintEventArgs g)
        {
            var a = game.Player.Direction;
            g.Graphics.DrawImage(sprites.Player[a.ToString()], game.Player.X, game.Player.Y);
            Invalidate();
        }
        
    }
}