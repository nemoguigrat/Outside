using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace UlernGame.View
{
    public class CustomFonts
    {
        public Font Franklin { get; }
        public Font Agency { get; }

        // private readonly PrivateFontCollection fontCollection = new PrivateFontCollection();

        public CustomFonts()
        {
            Franklin = LoadFontFromFile("Franklin.ttf", 72);
            Agency = LoadFontFromFile("agency.ttf", 72);
        }

        private Font LoadFontFromFile(string fileName, int fontSize)
        {
            var fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile("Fonts/" + fileName);
            return new Font(fontCollection.Families.Last(), fontSize);
        }
    }
}