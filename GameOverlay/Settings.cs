namespace GameOverlay
{
    public class Settings
    {
        public Setting FirstSpell { get; set; } = null!;
        public Setting SecondSpell { get; set; } = null!;
        public Setting ThirdSpell { get; set; } = null!;
    }

    public class Setting
    {
        public int KeyCode { get; set; }
        public string Image { get; set; } = null!;
        public int Time { get; set; }
    }
}
