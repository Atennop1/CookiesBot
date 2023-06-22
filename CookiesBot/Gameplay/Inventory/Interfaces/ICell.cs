namespace CookiesBot.Gameplay
{
    public interface ICell : IReadOnlyCell
    {
        void AddItems(int count);
        void RemoveItems(int count);
    }
}