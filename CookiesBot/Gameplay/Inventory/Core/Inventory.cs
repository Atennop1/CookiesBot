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

            if (!CanAddCell(addingCell))
                throw new InvalidOperationException("Can't add cell");

            existingCell!.AddItems(addingCell.Count);
        }

        public bool CanAddCell(ICell addingCell)
        {
            if (addingCell == null)
                return false;
            
            var existingCell = _cells.Find(cell => cell.Item == addingCell.Item);
            return existingCell == null || existingCell.CanAddItems(addingCell.Count);
        }

        public void Remove(ICell removingCell)
        {
            if (removingCell == null)
                throw new ArgumentNullException(nameof(removingCell));

            if (!CanRemoveCell(removingCell))
                throw new InvalidOperationException("Can't remove cell");

            var existingCell = _cells.Find(cell => cell.Item == removingCell.Item);
            existingCell!.RemoveItems(removingCell.Count);

            if (existingCell.Count == 0)
                _cells.Remove(existingCell);
        }

        public bool CanRemoveCell(ICell removingCell)
        {
            if (removingCell == null)
                return false;

            if (_cells.All(cell => cell.Item != removingCell.Item))
                return false;

            var existingCell = _cells.Find(cell => cell.Item == removingCell.Item);
            return existingCell!.CanRemoveItems(removingCell.Count);
        }
    }
}