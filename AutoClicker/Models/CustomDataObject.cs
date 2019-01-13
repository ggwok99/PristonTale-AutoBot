using System.Collections.Generic;

namespace AutoClicker.Models
{
    public class CustomDataObject
    {
        public string ClientName { get; set; }
        public ScreenResolution ScreenResolution { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<CharacterAttributePosition> Attributes { get; set; }
    }
}
