namespace CookiesBot.Gameplay
{
    public interface IInventory : IReadOnlyInventory
    {
        void Add(ICell addingCell);
        void Remove(ICell removingCell);
    }
}