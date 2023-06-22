namespace CookiesBot.Gameplay
{
    public sealed class Inventory : IInventory
    {
        private readonly List<ICell> _cells = new();

        public IReadOnlyList<IReadOnlyCell> Cells 
            => _cells;

        public void Add(ICell addingCell)
        {
            if (addingCell == null)
                throw new ArgumentNullException(nameof(addingCell));

            if (_cells.All(cell => cell.Item != addingCell.Item))
            {
                _cells.Add(addingCell);
                return;
            }

            var existingCell = _cells.Find(cell => cell.Item == addingCell.Item);

            if (!existingCell!.CanAddItems(addingCell.Count))
                throw new InvalidOperationException("Can't add cell");

            existingCell.AddItems(addingCell.Count);
        }

        public void Remove(ICell removingCell)
        {
            if (removingCell == null)
                throw new ArgumentNullException(nameof(removingCell));

            if (_cells.All(cell => cell.Item != removingCell.Item))
                throw new InvalidOperationException("Can't remove cell");

            var existingCell = _cells.Find(cell => cell.Item == removingCell.Item);
            
            if (!existingCell!.CanRemoveItems(removingCell.Count))
                throw new InvalidOperationException("Can't remove cell");
            
            existingCell.RemoveItems(removingCell.Count);

            if (existingCell.Count == 0)
                _cells.Remove(existingCell);
        }
    }
}