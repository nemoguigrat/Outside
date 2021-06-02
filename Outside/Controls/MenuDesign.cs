using System.Drawing;
using System.Windows.Forms;

namespace Outside.Controller
{
    public static class MenuDesign
    {
        public static Button MakeButton(Point location, string text, int fontSize=26, double buttonSize=1)
        {
            return new Button
            {
                Text = text,
                Font = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold),
                Location = location,
                Size = new Size((int) (260 * buttonSize), (int) (60 * buttonSize)),
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