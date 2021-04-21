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
            var timer = new Timer();
            timer.Interval = 20;
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs key)
        {
            game.Player.PlayerAction(key.KeyData);
            game.KeyPressed = key.KeyData;
            
        }

        protected override void OnPaint(PaintEventArgs g)
        {
            var a = game.Player.Direction;
            g.Graphics.DrawImage(sprites.Player[a.ToString()], game.Player.X, game.Player.Y);
            g.Graphics.DrawImage(sprites.Monster["Up"], game.Monsters[0].X, game.Monsters[0].Y);
        }

        private void DrawBullet(PaintEventArgs g)
        {
            
        }
        
    }
}