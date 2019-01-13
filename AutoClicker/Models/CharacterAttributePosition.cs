using AutoClicker.Enums;
using System.Drawing;

namespace AutoClicker.Models
{
    public class CharacterAttributePosition
    {
        public RECT Position { get; set; }
        public AttributeType Type { get; set; }

        public CharacterAttributePosition(int Left, int Top, int Right, int Bottom, Color color)
        {
            Position = new RECT(Left, Top, Right, Bottom);
            Type = GetAttribteType(color);
        }

        private AttributeType GetAttribteType(Color color)
        {
            if (color.R > color.B && color.R > color.G)
            {
                return AttributeType.HP;
            }

            if (color.B > color.R && color.B > color.G)
            {
                return AttributeType.MP;
            }

            return AttributeType.STM;
        }
    }
}
