using CookiesBot.Loop;
using CookiesBot.Telegram;
using CookiesBot.Tools;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableTypes;

namespace CookiesBot.Gameplay
{
    public sealed class ExampleBot : ILoopObject
    {
        private readonly ITelegram _telegram;

        public ExampleBot(ITelegram telegram) 
            => _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));

        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (updateInfo == null)
                throw new ArgumentNullException(nameof(updateInfo));

            var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                InlineButtonBuilder.SetCallbackData("Кнопка!", "send: Ты нажал на кнопку!")
            });
            
            _telegram.SendMessage("Привет! Я - бот с печеньками!", (long)updateInfo.Message?.Chat.Id!, inlineKeyboardMarkup);
        }

        public UpdateType RequiredUpdateType 
            => UpdateType.Message;
        
        public bool CanGetUpdate(IUpdateInfo updateInfo)
            => updateInfo.Message!.Text!.IsCommand("/start");
    }
}