namespace CookiesBot.Gameplay
{
    public sealed class Item : IItem
    {
        public string Name { get; }
        public string Description { get; }

        public Item(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}