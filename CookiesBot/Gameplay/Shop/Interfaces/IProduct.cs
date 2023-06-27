namespace CookiesBot.Gameplay;

public interface IProduct
{
    IItem Item { get; }
    ICost Cost { get; }
}