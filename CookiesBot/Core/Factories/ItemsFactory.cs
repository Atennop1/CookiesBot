using CookiesBot.Gameplay;

namespace CookiesBot.Core
{
    public sealed class ItemsFactory : IItemsFactory
    {
        public IItems Create()
        {
            return new Items(new List<IItem>
            {
                new Item("Древняя печенька", "Таинственная древняя печенька, найденная в заброшенном храме", 100)
            });
        }
    }
}