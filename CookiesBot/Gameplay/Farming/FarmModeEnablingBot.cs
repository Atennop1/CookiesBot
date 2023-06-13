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
        private readonly IFarmingStatusValue _farmingStatusValue;

        public FarmModeEnablingBot(ITelegram telegram, IFarmingStatusValue farmingStatusValue)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _farmingStatusValue = farmingStatusValue ?? throw new ArgumentNullException(nameof(farmingStatusValue));
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.Message;
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update now");

            var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                InlineButtonBuilder.SetCallbackData("Получить печеньку", "add_cookie")
            });
            
            _telegram.SendMessage("Включен режим фермы\nЧтобы получить печеньку, нажмите на кнопку ниже\nЧтобы выйти из режима фермы напишите /disabledFarmMode", updateInfo.Message!.From!.Id, inlineKeyboardMarkup);
            _farmingStatusValue.Set(FarmingStatus.Enabled);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => _farmingStatusValue.Get() == FarmingStatus.Disabled && updateInfo.Message!.Text!.IsCommand("/enableFarmMode");
    }
}