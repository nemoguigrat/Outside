using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using UlernGame.Model;
using UlernGame.View;

namespace UlernGame
{
    public partial class GameForm : Form
    {
        private Game game;
        private Painter painter;
        private Timer mainTimer;
        private string level;

        public GameForm(Point location, string level)
        {
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            BackColor = Color.Gray;
            Location = location;
            this.level = level;
        }

        protected override void OnLoad(EventArgs eventArgs)
        {
            game = new Game(level);
            painter = new Painter(game);
            game.StartGame();
            AddControls();
            mainTimer = new Timer();
            mainTimer.Interval = 20;
            mainTimer.Tick += (sender, args) => TimerTick();
            mainTimer.Start();
            // SetTimer(20, TimerTick); //основной игровой таймер
            SetTimer(500, DamageTimerTick); //таймер урона по монстрам 
            SetTimer(4000, () => game.SpawnMonsters()); //таймер спавна монстров 
            SetTimer(1500, MonsterMovement); // обновление маршрута и движение монстра
            Paint += (sender, args) => painter.Paint(args.Graphics);

        }
        private void AddControls()
        {
            var ammunition = new Label
            {
                Location = new Point(20, 70), AutoSize = true, BackColor = Color.Transparent,
                ForeColor = Color.Azure, Font = new Font(FontFamily.GenericMonospace, 20, FontStyle.Bold)
            };
            Controls.Add(ammunition);
        }

        private void DamageTimerTick()
        {
            if (game.CheckCollisionWithEnemy())
                game.Player.ReserveDamage(Monster.damage);
        }

        private void MonsterMovement()
        {
            game.Monsters.ForEach(x => x.MoveTo());
        }

        private void TimerTick()
        {
            if (game.GameOver || !game.Player.Alive)
            {
                mainTimer.Stop();
                Close();
            }
            game.Bullets.ForEach(x => x.Move());
            if (game.Bullets.Count > 0)
                game.BulletCollision();
            game.CheckCollisionWithBoosters();
            if (Controls.Count > 0)
                Controls[0].Text = $"{game.Player.Magazine} / {game.Player.Ammunition}";
            Invalidate();
        }

        private void SetTimer(int interval, Action func)
        {
            var timer = new Timer();
            timer.Interval = interval;
            timer.Tick += (sender, args) => func();
            timer.Start();
        }

        protected override void OnKeyDown(KeyEventArgs key)
        {
            game.PlayerMoveDirection(key.KeyData);
        }

        protected override void OnKeyUp(KeyEventArgs key)
        {
            game.PlayerAction(key.KeyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            var form = new GameOverForm(Location);
            form.Show();
        }
    }
}