using System.Data;
using CookiesBot.Loop;
using CookiesBot.Telegram;
using CookiesBot.Tools;
using RelationalDatabasesViaOOP;

namespace CookiesBot.Gameplay
{
    public sealed class BalanceBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IDatabase _database;

        public BalanceBot(ITelegram telegram, IDatabase database)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.ButtonCallback | TypeOfUpdate.Message;
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update");

            var userId = updateInfo.Type == TypeOfUpdate.Message ? updateInfo.Message!.From!.Id : updateInfo.CallbackQuery!.From.Id;
            var userDataReader = _database.SendReaderRequest($"SELECT * FROM users WHERE user_id = {userId}");
            
            var userData = new DataTable();
            userData.Load(userDataReader);
            
            var message = $"Баланс:\nОбычных печенек: {userData.Rows[0]["average_cookies_count"]}\nЗолотых печенек: {userData.Rows[0]["gold_cookies_count"]}";
            _telegram.SendMessage(message, userId);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => (updateInfo.Type == TypeOfUpdate.ButtonCallback && updateInfo.CallbackQuery!.Data! == "balance") ||
               (updateInfo.Type == TypeOfUpdate.Message && updateInfo.Message!.Text!.IsCommand("/balance"));
    }
}