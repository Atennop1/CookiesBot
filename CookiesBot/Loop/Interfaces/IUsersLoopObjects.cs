using Telegram.BotAPI.GettingUpdates;

namespace CookiesBot.Loop
{
    public interface IUsersLoopObjects
    {
        bool IsLoopObjectsForUserExist(Update update);
        
        List<ILoopObject> GetLoopObjectsOfUser(Update update);
        void CreateLoopObjectsForNewUser(Update update);
    }
}