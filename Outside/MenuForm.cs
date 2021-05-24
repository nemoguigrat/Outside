using System.Drawing;
using System.Windows.Forms;

namespace UlernGame
{
    public partial class MenuForm : Form
    { 
        public MenuForm()
        {
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            BackColor = Color.Gray;
            AddControls();
        }

        public void AddControls()
        {
            var startButton = new Button() { Text = "Начать", AutoSize = true, Location = new Point(Width / 2, Height / 2)};
            var exitButton = new Button() { Text = "Выход", AutoSize = true, Location = new Point(Width / 2, Height / 2 + 50)};
            startButton.Click += (sender, args) =>
            {
                new GameForm().Show();
                Hide();
            };
            exitButton.Click += (sender, args) => Application.Exit();
            Controls.Add(startButton);
            Controls.Add(exitButton);
        }
    }
}