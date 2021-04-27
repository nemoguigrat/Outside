using System;
using System.Drawing;
using System.Windows.Forms;

namespace UlernGame.Model
{
    public class Player : GameObject
    {
        private const int maxAmmunition = 120;
        private const int fullMagazine = 15;
        private const int maxHeals = 100;
        private const int speed = 5;
        public const int damage = 3;
        public const int width = 75;
        public const int height = 66;
        private readonly Game gameModel;
        public int Ammunition { get; private set; }
        public int Magazine { get; private set; }
        public Directions Direction { get; private set; }
        
        public int Damage { get; }
        public int Heals { get; private set; }

        public Player(int x, int y, Game game)
        {
            gameModel = game;
            Heals = maxHeals;
            X = x;
            Y = y;
            Magazine = fullMagazine;
            Ammunition = maxAmmunition;
            Damage = 10;
            Hitbox = new Rectangle(new Point(0,0), new Size(50,50));
        }

        public void Reload()
        {
             var reload = fullMagazine - Magazine;
             if (reload > 0 && Ammunition - reload >= 0)
                Ammunition -= reload;
             Magazine += reload;
        }

        public void ReserveDamage(int damage)
        {
            Heals -= damage;
            if (Heals <= 0)
                throw new Exception();
        }
        public void Shoot()
        {
            if (Magazine <= 0) return;
            gameModel.Bullets.Add(new Bullet(this));
            
        }
        public void PlayerAction(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    if (!gameModel.CheckCollisionWithObstacle(X - speed, Y, Hitbox.Width, Hitbox.Height)) 
                        X -= speed;
                    Direction = Directions.Left;
                    break;
                case Keys.D:
                    if (!gameModel.CheckCollisionWithObstacle(X + speed, Y, Hitbox.Width, Hitbox.Height))
                        X += speed;
                    Direction = Directions.Right;
                    break;
                case Keys.W:
                    if (!gameModel.CheckCollisionWithObstacle(X, Y - speed, Hitbox.Width, Hitbox.Height))
                        Y -= speed;
                    Direction = Directions.Up;
                    break;
                case Keys.S:
                    if (!gameModel.CheckCollisionWithObstacle(X, Y + speed, Hitbox.Width, Hitbox.Height))
                        Y += speed;
                    Direction = Directions.Down;
                    break;
                case Keys.Space:
                    Shoot();
                    break;
            } 
        }
    }
}