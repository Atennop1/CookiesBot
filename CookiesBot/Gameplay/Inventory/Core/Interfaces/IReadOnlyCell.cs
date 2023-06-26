namespace CookiesBot.Gameplay
{
    public interface IReadOnlyCell
    {
        IItem Item { get; }
        int Count { get; }

        bool CanAddItems(int count);
        bool CanRemoveItems(int count);
    }
}