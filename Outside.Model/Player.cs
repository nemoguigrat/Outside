using System;
using System.Drawing;
using System.Windows.Forms;

namespace UlernGame.Model
{
    public class Player : GameObject
    {
        public readonly int damage = 3;
        private readonly int maxAmmunition = 120;
        private readonly int fullMagazine = 15;
        private readonly int maxHeals = 100;
        private readonly int speed = 10;
        public int Ammunition { get; private set; }
        public int Magazine { get; private set; }
        public PlayerDirection Direction { get; private set; }
        
        public int Damage { get; }
        public int Heals { get; private set; }


        public Player(int x, int y)
        {
            Heals = maxHeals;
            X = x;
            Y = y;
            Magazine = fullMagazine;
            Ammunition = maxAmmunition;
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
            
        }

        public void Move(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    X -= speed;
                    Direction = PlayerDirection.Left;
                    break;
                case Keys.D:
                    X += speed;
                    Direction = PlayerDirection.Right;
                    break;
                case Keys.W:
                    Y -= speed;
                    Direction = PlayerDirection.Up;
                    break;
                case Keys.S:
                    Y += speed;
                    Direction = PlayerDirection.Down;
                    break;
                case Keys.Space:
                    Shoot();
                    break;
            } 
        }
    }
}