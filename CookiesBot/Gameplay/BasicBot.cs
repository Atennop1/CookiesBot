using CookiesBot.Loop;
using CookiesBot.Telegram;
using Telegram.BotAPI;

namespace CookiesBot.Gameplay
{
    public sealed class BasicBot : ILoopObject
    {
        private readonly ITelegram _telegram;

        public BasicBot(ITelegram telegram) 
            => _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));

        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (updateInfo == null)
                throw new ArgumentNullException(nameof(updateInfo));
            
            _telegram.SendMessage("Привет! Я - бот с печеньками!", (long)updateInfo.Message?.Chat.Id!);
        }

        public UpdateType RequiredUpdateType 
            => UpdateType.Message;
        
        public bool CanGetUpdate(IUpdateInfo updateInfo)
            => true;
    }
}