namespace AutoClicker.Models
{
    public class Profile
    {
        public string Name { get; set; }
        public ProfileContextData Data { get; set; }
    }

    public class ProfileContextData
    {
        public dynamic Attack { get; set; }
        public dynamic Buff { get; set; }
        public dynamic Potion { get; set; }
        public dynamic RefillPotion { get; set; }
        public dynamic PickItem { get; set; }
    }
}
