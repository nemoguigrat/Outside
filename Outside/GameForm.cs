using System;
using System.Drawing;
using System.Windows.Forms;
using Outside.Model;
using Outside.View;
using Timer = System.Windows.Forms.Timer;

namespace Outside
{
    public partial class GameForm : Form
    {
        private Game Game { get; }
        private Painter Painter { get; }
        private Timer MainTimer { get; }
        private int FlashlightRadius { get; set; }
        
        public GameForm(Point location, string level)
        {
            //Параметры окна
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            BackColor = Color.Gray;
            Location = location;
            Icon = new Icon("Resources/icon.ico");
            // Создание игры
            Game = new Game(level);
            FlashlightRadius = 220;
            //Запуск таймеров
            MainTimer = new Timer();
            MainTimer.Interval = 20;
            MainTimer.Tick += (sender, args) => TimerTick();
            MainTimer.Start();
            SetTimer(500, DamageTimerTick); //таймер урона по монстрам 
            SetTimer(1200, () => FlashlightRadius--);
            SetTimer(5000, () =>
            {
                if (Game.Monsters.Count < 20) Game.SpawnMonsters();   //таймер спавна монстров 
            }); 
            SetTimer(1420, MonsterMovement); // обновление маршрута и движение монстра
            //Отрисовка
            AddControls();
            Painter = new Painter(Game);
            Paint += (sender, args) => Painter.Paint(args.Graphics, FlashlightRadius);
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

        protected override void OnKeyUp(KeyEventArgs e)
        {
            Game.PlayerAction(e.KeyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Game.PlayerMove(e.KeyData);
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
                Game.Player.ReceiveDamage(Monster.Damage);
        }

        private void MonsterMovement()
        {
            Game.Monsters.ForEach(x => x.FindTarget());
        }

        private void TimerTick()
        {
            if (FlashlightRadius < -4)
                Game.Player.ReceiveDamage(100);
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