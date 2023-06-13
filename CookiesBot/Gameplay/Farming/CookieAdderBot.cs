using System.Data;
using CookiesBot.Loop;
using CookiesBot.Telegram;
using RelationalDatabasesViaOOP;

namespace CookiesBot.Gameplay
{
    public sealed class CookieAdderBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IDatabase _database;
        private readonly IFarmingStatusValue _farmingStatusValue;

        public CookieAdderBot(ITelegram telegram, IDatabase database, IFarmingStatusValue farmingStatusValue)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _farmingStatusValue = farmingStatusValue ?? throw new ArgumentNullException(nameof(farmingStatusValue));
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.ButtonCallback;
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update now");
            
            var cookiesCountReader = _database.SendReaderRequest($"SELECT average_cookies_count FROM users WHERE user_id = {updateInfo.CallbackQuery!.From.Id}");
            var cookiesCountTable = new DataTable();
            
            cookiesCountTable.Load(cookiesCountReader);
            var userCookiesCount = (int)cookiesCountTable.Rows[0]["average_cookies_count"];
            
            _database.SendNonQueryRequest($"UPDATE users SET average_cookies_count = {userCookiesCount + 1} WHERE average_cookies_count = {userCookiesCount}");
            _telegram.SendMessage("+1 печенька!", updateInfo.Message!.From!.Id);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => _farmingStatusValue.Get() == FarmingStatus.Enabled && updateInfo.CallbackQuery!.Data == "add_cookie";
    }
}