namespace CookiesBot.Gameplay;

public interface IFarmingStatusValue
{
    void Set(FarmingStatus value);
    FarmingStatus Get();
}