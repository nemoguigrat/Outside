using System.Drawing;
using System.Windows.Forms;

namespace UlernGame
{
    public partial class GameOverForm : Form
    {
        public GameOverForm(Point location)
        {
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            BackColor = Color.Gray;
            Location = location;
            AddControls();
        }

        public void AddControls()
        {
            var restartButton = new Button() { Text = "Рестарт", AutoSize = true, Location = new Point(Width / 2, Height / 2)};
            var exitButton = new Button() { Text = "выход", AutoSize = true, Location = new Point(Width / 2, Height / 2 + 50)};
            restartButton.Click += (sender, args) =>
            {
                new GameForm(Location).Show();
                Close();
            };
            exitButton.Click += (sender, args) => Application.Exit();
            Controls.Add(restartButton);
            Controls.Add(exitButton);
        }
    }
}