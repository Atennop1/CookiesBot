using Telegram.BotAPI;

namespace CookiesBot.Loop
{
    public interface ILoopObject
    {
        UpdateType RequiredUpdateType { get; }
        
        void GetUpdate(IUpdateInfo updateInfo);
        bool CanGetUpdate(IUpdateInfo updateInfo);
    }
}