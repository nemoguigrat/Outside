using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Outside.Controller;
using Outside.Model;

namespace Outside
{
    public partial class GameOverForm : Form
    {
        private string CurrentLevel { get; set; }
        private Dictionary<string, string> LevelsDict { get; }

        public GameOverForm(Point location, string message)
        {
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            BackColor = Color.Black;
            Location = location;
            Icon = new Icon("Resources/icon.ico");
            LevelsDict = Levels.MakeDict();
            CurrentLevel = LevelsDict.Values.First();
            FormClosing += (sender, args) => Application.Exit();
            AddControls(message);
        }

        private void AddControls(string message)
        {
            var gameName = MenuDesign.MakeName(new Point(Width / 2 - 300, 150));
            var startButton = MenuDesign.MakeButton(new Point(Width / 6 - 130, 320), "Начать");
            var exitButton = MenuDesign.MakeButton(new Point(5 * Width / 6 - 130, 320), "Выйти");
            var gameOverMessage = new Label
            {
                Text = message,
                Location = new Point(Width / 2 - 300, 425),
                BackColor = Color.Transparent,
                Size = new Size(600, 200),
                ForeColor = Color.Azure,
                Font = new Font(FontFamily.GenericMonospace, 26, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            var levelChanger = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(Width / 2 - 130, 325),
                Font = new Font(FontFamily.GenericMonospace, 26, FontStyle.Bold),
                Size = new Size(260, 90),
                ForeColor = Color.Black,
                BackColor = Color.White,
                FlatStyle = FlatStyle.System
            };

            levelChanger.Items.AddRange(LevelsDict.Keys.ToArray());
            levelChanger.SelectedIndex = 0;
            levelChanger.SelectedIndexChanged += (sender, args) =>
                CurrentLevel = LevelsDict[levelChanger.SelectedItem.ToString()];

            startButton.Click += (sender, args) =>
            {
                new GameForm(Location, CurrentLevel).Show();
                Hide();
            };
            exitButton.Click += (sender, args) => Application.Exit();
            Controls.Add(levelChanger);
            Controls.Add(gameName);
            Controls.Add(startButton);
            Controls.Add(exitButton);
            Controls.Add(gameOverMessage);
        }
    }
}