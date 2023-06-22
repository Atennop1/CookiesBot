namespace CookiesBot.Gameplay
{
    public sealed class Cell : ICell
    {
        public IItem Item { get; }
        public int Count { get; private set; }

        public Cell(IItem item, int count)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Count = count;
        }

        public bool CanAddItems(int count)
            => true;

        public bool CanRemoveItems(int count)
            => Count >= count;

        public void AddItems(int count) 
            => Count += count;

        public void RemoveItems(int count)
        {
            if (!CanRemoveItems(count))
                throw new InvalidOperationException($"Can't remove {count} items");

            Count -= count;
        }
    }
}