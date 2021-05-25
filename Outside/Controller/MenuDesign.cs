﻿using System.Drawing;
using System.Windows.Forms;

namespace UlernGame.Controller
{
    public static class MenuDesign
    {
        public static Button MakeButton(Point location, string text)
        {
            return new Button
            {
                Text = text,
                Font = new Font(FontFamily.GenericMonospace, 26, FontStyle.Bold),
                Location = location,
                Size = new Size(260, 60),
                ForeColor = Color.Black,
                BackColor = Color.White
            };
        }
        
        public static Label MakeName(Point location) => new Label
        {
            Location = location, BackColor = Color.Transparent, Size = new Size(600, 120),
            ForeColor = Color.Azure, Font = new Font(FontFamily.GenericMonospace, 72, FontStyle.Regular),
            TextAlign = ContentAlignment.MiddleCenter, Text = @"[Outside]"
        };
    }
}