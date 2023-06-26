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

            return _items.IndexOf(item);
        }
    }
}