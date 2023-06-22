namespace CookiesBot.Gameplay
{
    public interface IItem
    {
        string Name { get; }
        string Description { get; }
        int Cost { get; }
    }
}