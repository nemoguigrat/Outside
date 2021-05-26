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

        public GameForm(Point location, string level)
        {
            //Параметры окна
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            BackColor = Color.Gray;
            Location = location;
            // Создание игры
            game = new Game(level);
            game.StartGame();
            //Запуск таймеров
            mainTimer = new Timer();
            mainTimer.Interval = 20;
            mainTimer.Tick += (sender, args) => TimerTick();
            mainTimer.Start();
            SetTimer(500, DamageTimerTick); //таймер урона по монстрам 
            SetTimer(3000, () => game.SpawnMonsters()); //таймер спавна монстров 
            SetTimer(1300, MonsterMovement); // обновление маршрута и движение монстра
            //Отрисовка
            AddControls();
            painter = new Painter(game);
            Paint += (sender, args) => painter.Paint(args.Graphics);
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
            if (game.CheckCollisionWithEnemy())
                game.Player.ReserveDamage(Monster.damage);
        }

        private void MonsterMovement()
        {
            game.Monsters.ForEach(x => x.FindTarget());
        }

        private void TimerTick()
        {
            if (game.GameOver || !game.Player.Alive)
            {
                mainTimer.Stop();
                Close();
            }
            game.Bullets.ForEach(x => x.Move());
            game.Monsters.ForEach(x => x.Move());

            if (game.Bullets.Count > 0)
                game.BulletCollision();
            game.CheckCollisionWithItems();
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
    }
}