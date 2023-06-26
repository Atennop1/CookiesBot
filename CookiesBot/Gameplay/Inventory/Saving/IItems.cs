namespace CookiesBot.Gameplay
{
    public interface IItems
    {
        IItem GetById(int id);
        int GetItemId(IItem item);
    }
}