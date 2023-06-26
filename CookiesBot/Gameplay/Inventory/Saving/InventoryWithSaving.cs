using RelationalDatabasesViaOOP;

namespace CookiesBot.Gameplay
{
    public sealed class InventoryWithSaving : IInventory
    {
        private readonly IInventory _inventory;
        private readonly IDatabase _database;
        private readonly IItems _items;
        private readonly long _userId;

        public InventoryWithSaving(IInventory inventory, IDatabase database, IItems items, long userId)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _items = items ?? throw new ArgumentNullException(nameof(items));
            _userId = userId;

            var loadedCells = _database.SendReadingRequest($"SELECT * FROM users_items WHERE user_id = {_userId}");
            
            for (var i = 0; i < loadedCells.Rows.Count; i++)
                _inventory.Add(new Cell(_items.GetById((int)loadedCells.Rows[i]["item_id"]), (int)loadedCells.Rows[i]["count_of_items"]));
        }

        public IReadOnlyList<IReadOnlyCell> Cells 
            => _inventory.Cells;

        public bool CanAddCell(ICell addingCell) 
            => _inventory.CanAddCell(addingCell);

        public bool CanRemoveCell(ICell removingCell) 
            => _inventory.CanRemoveCell(removingCell);

        public void Add(ICell addingCell)
        {
            if (!CanAddCell(addingCell))
                throw new InvalidOperationException("Can't add cell");

            var itemId = _items.GetItemId(addingCell.Item);
            var existingData = _database.SendReadingRequest($"SELECT * from users_items WHERE user_id = {_userId} AND item_id = {itemId}");

            _database.SendNonQueryRequest(existingData.Rows.Count == 0
                ? $"INSERT INTO users_items (user_id, item_id, count_of_items) VALUES ({_userId}, {itemId}, {addingCell.Count})"
                : $"UPDATE users_items SET count_of_items = {(int)existingData.Rows[0]["count_of_items"] + addingCell.Count} WHERE user_id = {_userId} AND item_id = {itemId}");

            _inventory.Add(addingCell);
        }

        public void Remove(ICell removingCell)
        {
            if (!CanRemoveCell(removingCell))
                throw new InvalidOperationException("Can't remove cell");
            
            var itemId = _items.GetItemId(removingCell.Item);
            _database.SendNonQueryRequest($"DELETE FROM uses_items WHERE user_id = {_userId} AND item_id = {itemId}");
            _inventory.Remove(removingCell);
        }
    }
}