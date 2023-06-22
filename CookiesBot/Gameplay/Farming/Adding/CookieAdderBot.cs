using CookiesBot.Loop;
using CookiesBot.Telegram;
using RelationalDatabasesViaOOP;

namespace CookiesBot.Gameplay
{
    public sealed class CookieAdderBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IDatabase _database;
        private readonly IScreen _screen;

        public CookieAdderBot(ITelegram telegram, IDatabase database, IScreen screen)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _screen = screen ?? throw new ArgumentNullException(nameof(screen));
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.ButtonCallback;
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update now");
            
            var cookiesCountTable = _database.SendReadingRequest($"SELECT average_cookies_count FROM users WHERE user_id = {updateInfo.CallbackQuery!.From.Id}");
            var userCookiesCount = (int)cookiesCountTable.Rows[0]["average_cookies_count"];
            
            _database.SendNonQueryRequest($"UPDATE users SET average_cookies_count = {userCookiesCount + 1} WHERE user_id = {updateInfo.CallbackQuery!.From.Id}");
            _telegram.SendMessage("+1 печенька!", updateInfo.CallbackQuery!.From.Id);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => _screen.IsActive && updateInfo.CallbackQuery!.Data == "add_cookie";
    }
}