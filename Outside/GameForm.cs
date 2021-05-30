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
            //Запуск таймеров
            MainTimer = new Timer();
            MainTimer.Interval = 20;
            MainTimer.Tick += (sender, args) => TimerTick();
            MainTimer.Start();
            SetTimer(500, DamageTimerTick); //таймер урона по монстрам 
            SetTimer(5000, () =>
            {
                if (Game.Monsters.Count < 30) Game.SpawnMonsters();   //таймер спавна монстров 
            }); 
            SetTimer(1500, MonsterMovement); // обновление маршрута и движение монстра
            //Отрисовка
            AddControls();
            Painter = new Painter(Game);
            Paint += (sender, args) => Painter.Paint(args.Graphics);
            //Контроллер??
            KeyUp += (sender, args) => Game.PlayerAction(args.KeyData);
            KeyDown += (sender, args) => Game.PlayerMoveDirection(args.KeyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Game.Player.Alive && !Game.GameOver)
                new GameOverForm(Location, "[Неужели вы решили сдаться?]").Show();
            else if (!Game.Player.Alive)
                new GameOverForm(Location, "[Вас поглотила тьма...]").Show();
            else
                new GameOverForm(Location, "[В этот раз вы смогли спастись!]").Show();
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
            Game.KillMonstersInDoor();
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