using CookiesBot.Loop;
using CookiesBot.Telegram;
using Telegram.BotAPI;

namespace CookiesBot.Gameplay
{
    public sealed class MessageSendingBot : ILoopObject
    {
        private readonly ITelegram _telegram;

        public MessageSendingBot(ITelegram telegram) 
            => _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));

        public UpdateType RequiredUpdateType 
            => UpdateType.CallbackQuery;
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update");

            var message = "";
            updateInfo.CallbackQuery!.Data!.Split(' ').Skip(1).ToList().ForEach(element => message += $"{element} ");
            
            _telegram.SendMessage(message, updateInfo.CallbackQuery.From.Id);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => updateInfo.CallbackQuery!.Data!.StartsWith("send: ") && updateInfo.CallbackQuery.Data.Split(' ').Skip(1).ToList().Count > 0;
    }
}