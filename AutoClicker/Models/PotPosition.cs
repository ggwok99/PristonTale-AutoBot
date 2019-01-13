using System.Drawing;

namespace AutoClicker.Models
{
    public class PotPosition
    {
        public PotPosition(Point hp, Point mp, Point stm)
        {
            HP = hp;
            MP = mp;
            STM = stm;
        }

        public Point HP { get; set; }
        public Point MP { get; set; }
        public Point STM { get; set; }
    }
}
