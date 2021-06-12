using System;
using System.Drawing;
using System.Windows.Forms;

namespace Outside.Model
{
    public class Player : GameObject
    {
        private const int MaxAmmunition = 25;
        private const int FullMagazine = 10;
        private const int MaxHeals = 100;
        public const int Speed = 7;
        public int Ammunition { get; private set; }
        public int Magazine { get; private set; }
        public Direction Direction { get; private set; }

        public int Heals { get; private set; }
        public bool HaveKey { get; set; }
        public bool Alive { get; private set; }
        public Player(int x, int y)
        {
            Heals = MaxHeals;
            X = x;
            Y = y;
            Magazine = FullMagazine;
            Ammunition = MaxAmmunition;
            Width = 50;
            Height = 50;
            HaveKey = false;
            Alive = true;
        }

        public void Reload()
        {
            var reload = FullMagazine - Magazine;
            if (reload >= Ammunition)
            {
                Magazine += Ammunition;
                Ammunition = 0;
            }
            else
            {
                Magazine += reload;
                Ammunition -= reload;
            }
        }

        public void ReceiveDamage(int damage)
        {
            Heals -= damage;
            if (Heals <= 0)
                Alive = false;
        }

        public bool Shoot()
        {
            if (Magazine <= 0) return false;
            Magazine--;
            return true;
        }

        public void Heal()
        {
            if (Heals + Medkit.HealCount <= MaxHeals)
                Heals += Medkit.HealCount;
            else
                Heals = MaxHeals;
        }

        public void AddAmmo()
        {
            if (Ammunition + AmmunitionCrate.AmmoCount <= MaxAmmunition)
                Ammunition += AmmunitionCrate.AmmoCount;
            else
                Ammunition = MaxAmmunition;
        }

        public void Move(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public void SwitchDirection(Direction dir)
        {
            Direction = dir;
        }
    }
}