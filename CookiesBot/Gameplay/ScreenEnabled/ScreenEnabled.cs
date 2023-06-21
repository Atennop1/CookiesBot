namespace CookiesBot.Gameplay
{
    public sealed class ScreenEnabled : IScreenEnabled
    {
        private bool _value;

        public ScreenEnabled(bool value) 
            => _value = value;

        public ScreenEnabled()
            => _value = false;

        public void Set(bool value)
            => _value = value;

        public bool Get()
            => _value;
    }
}