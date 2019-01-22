using System;

namespace AutoClicker.Models
{
    public class Master
    {
        private static readonly Lazy<Master> lazy = new Lazy<Master>(() => new Master());
        public static Master Instance => lazy.Value;
        private Master() { }

        public ScreenResolution ScreenResolution { get; set; }
    }
}
