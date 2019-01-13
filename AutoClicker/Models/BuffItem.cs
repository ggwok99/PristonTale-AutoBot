using System.Windows.Forms;

namespace AutoClicker.Models
{
    public class BuffItem
    {
        public BuffItem()
        {

        }

        public BuffItem(Keys key, int delayInSecond)
        {
            Key = key;
            Delay = delayInSecond;
        }

        public BuffItem(Keys key, string delayInSecond)
        {
            Key = key;
            int.TryParse(delayInSecond, out int delay);
            Delay = delay;
        }

        public bool DecrementTrueIfCounter0()
        {
            Counter--;
            if (Counter <= 0)
            {
                Counter = Delay;
                return true;
            }

            return false;
        }

        public Keys Key { get; set; }
        public int Delay { get; set; }
        public int Counter { get; set; }
    }
}
