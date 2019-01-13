using System.Drawing;

namespace AutoClicker.Models
{
    public class ScreenResolution
    {
        public ScreenResolution()
        {

        }

        public ScreenResolution(int x, int y)
        {
            X = x;
            Y = y;
            Center = new Point(X / 2, Y / 2);
        }

        public ScreenResolution(string x, string y)
        {
            int.TryParse(x, out int xResult);
            X = xResult;
            int.TryParse(y, out int yResult);
            Y = yResult;
            Center = new Point(X / 2, Y / 2);
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Point Center { get; set; }
    }
}
