using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace UlernGame
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            DoubleBuffered = true;
            ClientSize = new Size(1280, 720);
            BackColor = Color.Black;

            AddControls();
        }

        public void AddControls()
        {
            var gameName = new Label
            {
                Location = new Point(Width / 2 - 300, 150), BackColor = Color.Transparent, Size = new Size(600, 120),
                ForeColor = Color.Azure, Font = new Font(FontFamily.GenericMonospace, 72, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            gameName.Text = @"[Outside]";

            var startButton = new Button()
            {
                Text = "Начать",
                Font = new Font(FontFamily.GenericMonospace, 26, FontStyle.Bold),
                Location = new Point(Width / 2 - 130, 320),
                Size = new Size(260, 60),
                ForeColor = Color.Black,
                BackColor = Color.White
            };
            
            var tutorButton = new Button
            {
                Text = "Инструкция",
                Font = new Font(FontFamily.GenericMonospace, 26, FontStyle.Bold),
                Location = new Point(Width / 2 - 130, 420),
                Size = new Size(260, 60),
                ForeColor = Color.Black,
                BackColor = Color.White
            };
            
            var exitButton = new Button
            {
                Text = "Выйти",
                Font = new Font(FontFamily.GenericMonospace, 26, FontStyle.Bold),
                Location = new Point(Width / 2 - 130, 520),
                Size = new Size(260, 60),
                ForeColor = Color.Black,
                BackColor = Color.White
            };
            
            startButton.Click += (sender, args) =>
            {
                new GameForm(Location).Show();
                Hide();
            };
            exitButton.Click += (sender, args) => Application.Exit();
            Controls.Add(startButton);
            Controls.Add(exitButton);
            Controls.Add(tutorButton);
            Controls.Add(gameName);
        }
    }
}