using CookiesBot.Tools;

namespace CookiesBot.Gameplay
{
    public sealed class Items : IItems
    {
        private readonly List<IItem> _items;

        public Items(List<IItem> items) 
            => _items = items;

        public IItem GetById(int id) 
            => _items[id.ThrowExceptionIfLessThanZero()];

        public int GetItemId(IItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var existingItem = _items.Find(second => second.IsEquals(item));

            if (existingItem == null)
                throw new InvalidOperationException("Item does not exist");
            
            return _items.IndexOf(existingItem);
        }
    }
}