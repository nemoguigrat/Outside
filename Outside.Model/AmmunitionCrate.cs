﻿using System.Drawing;
using Outside.Model;

namespace Outside.Model
{
    public class AmmunitionCrate : Item
    {
        public const int AmmoCount = 5;

        public AmmunitionCrate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}