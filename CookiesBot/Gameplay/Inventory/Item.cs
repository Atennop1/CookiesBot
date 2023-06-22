namespace CookiesBot.Gameplay
{
    public sealed class Item : IItem
    {
        public string Name { get; }
        public string Description { get; }
        public int Cost { get; }

        public Item(string name, string description, int cost)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Cost = cost;
        }
    }
}