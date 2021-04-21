using System;
using System.Drawing;
using System.Windows.Forms;

namespace UlernGame.Model
{
    public class Player : GameObject
    {
        public const int damage = 3;
        private const int maxAmmunition = 120;
        private const int fullMagazine = 15;
        private const int maxHeals = 100;
        private const int speed = 10;
        public const int width = 75;
        public const int height = 66;
        public int Ammunition { get; private set; }
        public int Magazine { get; private set; }
        public Directions Direction { get; private set; }
        
        public int Damage { get; }
        public int Heals { get; private set; }
        


        public Player(int x, int y)
        {
            Heals = maxHeals;
            X = x;
            Y = y;
            Magazine = fullMagazine;
            Ammunition = maxAmmunition;
            Damage = 10;
        }

        public void Reload()
        {
             var reload = fullMagazine - Magazine;
             if (reload > 0 && Ammunition - reload >= 0)
                Ammunition -= reload;
             Magazine += reload;
        }

        public void Shoot()
        {
            if (Magazine <= 0) return;
            var bullet = new Bullet(this);
            
            
        }

        public void PlayerAction(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    X -= speed;
                    Direction = Directions.Left;
                    break;
                case Keys.D:
                    X += speed;
                    Direction = Directions.Right;
                    break;
                case Keys.W:
                    Y -= speed;
                    Direction = Directions.Up;
                    break;
                case Keys.S:
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