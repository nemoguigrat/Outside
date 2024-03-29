﻿using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Outside.Controller;
using Outside.Model;

namespace Outside
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            BackColor = Color.Black;
            Icon = new Icon("Resources/icon.ico");
            AddControls();
        }

        private void AddControls()
        {
            var gameName = MenuDesign.MakeName(new Point(Width / 2 - 300, 150));

            var startButton = MenuDesign.MakeButton(new Point(Width / 2 - 130, 320), "Начать");

            var tutorButton = MenuDesign.MakeButton(new Point(Width / 2 - 130, 420), "Инструкция");

            var exitButton = MenuDesign.MakeButton(new Point(Width / 2 - 130, 520), "Выйти");

            startButton.Click += (sender, args) =>
            {
                new GameForm(Location, Levels.Dungeon).Show();
                Hide();
            };

            tutorButton.Click += (sender, args) => MessageBox.Show(
                @"Задача:
    Найти ключ и дверь, к которой он подходит и выбраться из замка, 
        но торопитесь, иначе тьма поглотит вас.

Управление:
    W,A,S,D - Передвижение 
    F - Открыть/Закрыть дверь 
    Space - Выстрелить",
                "Инструкция",
                MessageBoxButtons.OK,
                MessageBoxIcon.None);

            exitButton.Click += (sender, args) => Application.Exit();

            Controls.Add(startButton);
            Controls.Add(exitButton);
            Controls.Add(tutorButton);
            Controls.Add(gameName);
        }
    }
}