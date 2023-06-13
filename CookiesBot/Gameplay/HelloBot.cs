using CookiesBot.Loop;
using CookiesBot.Telegram;
using CookiesBot.Tools;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableTypes;

namespace CookiesBot.Gameplay
{
    public sealed class HelloBot : ILoopObject
    {
        private readonly ITelegram _telegram;

        public HelloBot(ITelegram telegram) 
            => _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));

        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (updateInfo == null)
                throw new ArgumentNullException(nameof(updateInfo));

            var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                InlineButtonBuilder.SetCallbackData("Посмотреть баланс", "balance")
            });
            
            _telegram.SendMessage("Привет! Я - бот с печеньками!\nТы можешь посмотреть баланс, нажав на кнопку ниже", (long)updateInfo.Message?.Chat.Id!, inlineKeyboardMarkup);
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.Message;
        
        public bool CanGetUpdate(IUpdateInfo updateInfo)
            => updateInfo.Message!.Text!.IsCommand("/start");
    }
}