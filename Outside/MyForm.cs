using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
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
            game.StartGame();
            var timer = new Timer();
            timer.Interval = 20;
            timer.Tick += TimerTick;
            timer.Start();
            DamageTimerTick();
            MonsterTimerTick();
        }

        private void DamageTimerTick()
        {
            var damageTimer = new Timer();
            damageTimer.Interval = 500;
            damageTimer.Tick += (sender, arg) =>
            {
                if (game.CheckCollisionWithEnemy())
                    game.Player.ReserveDamage(Monster.damage);
            };
            damageTimer.Start();
        }

        private void MonsterTimerTick()
        {
            var spawnTimer = new Timer();
            spawnTimer.Interval = 2000;
            spawnTimer.Tick += (sender, args) => game.SpawnMonsters();
            spawnTimer.Start();
        }
        
        private void TimerTick(object sender, EventArgs e)
        {
            game.Monsters.ForEach(x => x.MoveTo(game.Player.X, game.Player.Y));
            game.Bullets.ForEach(x => x.Move());

            if (game.Bullets.Count > 0)
                game.BulletCollision();
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs key)
        {
            game.Player.PlayerMove(key.KeyData);
        }

        protected override void OnKeyUp(KeyEventArgs key)
        {
            game.Player.PlayerAction(key.KeyData);
        }

        protected override void OnPaint(PaintEventArgs g)
        {
            var graphic = g.Graphics;
            DrawMap(graphic);
            DrawPlayer(graphic);
            DrawMonster(graphic);
            graphic.FillRectangle(Brushes.PaleGreen, 
                new Rectangle(20, 20, game.Player.Heals * 5, 20));
            DrawBullets(graphic);
            
        }
        void DrawMap(Graphics gr)
        {
            foreach (var e in game.Map.Objects)
            {
                if (e is Wall)
                    gr.DrawImage(sprites.Wall, e.X, e.Y);
            }
        }

        private void DrawPlayer(Graphics gr)
        {
            var a = game.Player.Direction;
            gr.DrawImage(sprites.Player[a.ToString()], game.Player.X, game.Player.Y);
            // gr.DrawRectangle(Pens.Chartreuse, game.Player.X, game.Player.Y, game.Player.Hitbox.Width, game.Player.Hitbox.Height);
        }

        private void DrawMonster(Graphics gr)
        {
            for (var i = 0; i < game.Monsters.Count; i++)
                gr.DrawImage(sprites.Monster[game.Monsters[i].Direction.ToString()], game.Monsters[i].X, game.Monsters[i].Y);
            
        }
        private void DrawBullets(Graphics g)
        {
            foreach (var bullet in game.Bullets)
            {
                g.FillEllipse(Brushes.Blue, bullet.X, bullet.Y, 6,6);
            }
        }
        
    }
}