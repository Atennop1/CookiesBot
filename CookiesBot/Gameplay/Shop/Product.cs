namespace CookiesBot.Gameplay
{
    public sealed class Product : IProduct
    {
        public IItem Item { get; }
        public ICost Cost { get; }

        public Product(IItem item, ICost cost)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Cost = cost ?? throw new ArgumentNullException(nameof(cost));
        }
    }
}