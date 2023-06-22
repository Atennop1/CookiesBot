namespace CookiesBot.Gameplay
{
    public interface IInventory
    {
        IReadOnlyList<IReadOnlyCell> Cells { get; }
        void Add(ICell addingCell);
        void Remove(ICell removingCell);
    }
}