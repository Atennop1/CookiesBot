using CookiesBot.Loop;
using CookiesBot.Telegram;
using CookiesBot.Tools;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableTypes;

namespace CookiesBot.Gameplay
{
    public sealed class FarmModeEnablingBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IScreenEnabled _screenEnabled;

        public FarmModeEnablingBot(ITelegram telegram, IScreenEnabled screenEnabled)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _screenEnabled = screenEnabled ?? throw new ArgumentNullException(nameof(screenEnabled));
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.Message;
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update now");

            var inlineKeyboardMarkup = new InlineKeyboardMarkup(
                new[] { InlineButtonBuilder.SetCallbackData("Получить обычную печеньку", "add_cookie") }, 
                new[] { InlineButtonBuilder.SetCallbackData("Получить золотую печеньку", "add_golden_cookie") });
            
            _telegram.SendMessage("Включен режим фермы\nЗдесь вы можете получать обычные и золотые печеньки\nЧтобы выйти из режима фермы напишите /disableFarmMode", updateInfo.Message!.From!.Id, inlineKeyboardMarkup);
            _screenEnabled.Set(true);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => _screenEnabled.Get() == false && updateInfo.Message!.Text!.IsCommand("/enableFarmMode");
    }
}