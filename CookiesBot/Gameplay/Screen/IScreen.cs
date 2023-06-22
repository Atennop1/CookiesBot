namespace CookiesBot.Gameplay
{
    public interface IScreen
    {
        bool IsActive { get; }

        void Enable();
        void Disable();
    }
}