namespace CookiesBot.Gameplay;

public interface IReadOnlyInventory
{
    IReadOnlyList<IReadOnlyCell> Cells { get; }
    bool CanAddCell(ICell addingCell);
    bool CanRemoveCell(ICell removingCell);
}