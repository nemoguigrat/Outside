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
using Outside.Model;
using Outside.View;

namespace Outside
{
    public partial class GameForm : Form
    {
        private Game Game { get; }
        private Painter Painter { get; }
        private Timer MainTimer { get; }

        public GameForm(Point location, string level)
        {
            //Параметры окна
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            BackColor = Color.Gray;
            Location = location;
            // Создание игры
            Game = new Game(level);
            Game.StartGame();
            //Запуск таймеров
            MainTimer = new Timer();
            MainTimer.Interval = 20;
            MainTimer.Tick += (sender, args) => TimerTick();
            MainTimer.Start();
            SetTimer(500, DamageTimerTick); //таймер урона по монстрам 
            SetTimer(1000, () => Game.SpawnMonsters()); //таймер спавна монстров 
            SetTimer(1500, MonsterMovement); // обновление маршрута и движение монстра
            //Отрисовка
            AddControls();
            Painter = new Painter(Game);
            Paint += (sender, args) => Painter.Paint(args.Graphics);
        }

        protected override void OnKeyDown(KeyEventArgs key)
        {
            Game.PlayerMoveDirection(key.KeyData);
        }

        protected override void OnKeyUp(KeyEventArgs key)
        {
            Game.PlayerAction(key.KeyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var form = new GameOverForm(Location);
            form.Show();
        }

        private void AddControls()
        {
            var ammunitionIndicator = new Label
            {
                Location = new Point(20, 70), AutoSize = true, BackColor = Color.Transparent,
                ForeColor = Color.Azure, Font = new Font(FontFamily.GenericMonospace, 20, FontStyle.Bold)
            };
            Controls.Add(ammunitionIndicator);
        }

        private void DamageTimerTick()
        {
            if (Game.CheckCollisionWithEnemy())
                Game.Player.ReserveDamage(Monster.Damage);
        }

        private void MonsterMovement()
        {
            Game.Monsters.ForEach(x => x.FindTarget());
        }

        private void TimerTick()
        {
            if (Game.GameOver || !Game.Player.Alive)
            {
                MainTimer.Stop();
                Close();
            }

            Game.Bullets.ForEach(x => x.Move());
            Game.Monsters.ForEach(x => x.Move());

            if (Game.Bullets.Count > 0)
                Game.BulletCollision();
            Game.CheckCollisionWithItems();
            if (Controls.Count > 0)
                Controls[0].Text = $"{Game.Player.Magazine} / {Game.Player.Ammunition}";
            Invalidate();
        }

        private void SetTimer(int interval, Action func)
        {
            var timer = new Timer();
            timer.Interval = interval;
            timer.Tick += (sender, args) => func();
            timer.Start();
        }
    }
}