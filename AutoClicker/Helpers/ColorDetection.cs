using System.Drawing;
using System.Linq;

namespace AutoClicker.Helpers
{
    public static class ColorDetection
    {
        private static readonly Color[] ColorArray = new Color[]
        {
            Color.Red,
            Color.Blue,
            Color.GreenYellow,
            Color.Black,
            Color.Pink,
            Color.Gray,
            Color.Orange,
            Color.Purple,
            Color.Green,
            Color.Yellow,
            Color.White
        };

        public static Color GetClosestColor(Color baseColor)
        {
            var colors = ColorArray.Select(x => new { Value = x, Diff = GetDiff(x, baseColor) }).ToList();
            int min = colors.Min(x => x.Diff);
            return colors.Find(x => x.Diff == min).Value;
        }

        private static int GetDiff(Color color, Color baseColor)
        {
            int a = color.A - baseColor.A,
                r = color.R - baseColor.R,
                g = color.G - baseColor.G,
                b = color.B - baseColor.B;
            return a * a + r * r + g * g + b * b;
        }
    }
}
