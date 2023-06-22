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
        private readonly IScreen _screen;

        public FarmModeEnablingBot(ITelegram telegram, IScreen screen)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _screen = screen ?? throw new ArgumentNullException(nameof(screen));
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
            _screen.Enable();
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => _screen.IsActive == false && updateInfo.Message!.Text!.IsCommand("/enableFarmMode");
    }
}