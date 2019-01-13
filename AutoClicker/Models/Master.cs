using System;
using System.Collections.Generic;

namespace AutoClicker.Models
{
    public class Master
    {
        private static readonly Lazy<Master> lazy = new Lazy<Master>(() => new Master());
        public static Master Instance => lazy.Value;
        private Master() { }

        public List<CharacterAttributePosition> PotPositions { get; set; }

        public ScreenResolution ScreenResolution { get; set; }
    }
}
