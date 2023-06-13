using CookiesBot.Loop;
using CookiesBot.Telegram;
using CookiesBot.Tools;

namespace CookiesBot.Gameplay
{
    public sealed class FarmModeDisablingBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IFarmingStatusValue _farmingStatusValue;

        public FarmModeDisablingBot(ITelegram telegram, IFarmingStatusValue farmingStatusValue)
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

            _telegram.SendMessage("Режим фермы отключен", updateInfo.Message!.From!.Id);
            _farmingStatusValue.Set(FarmingStatus.Disabled);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => _farmingStatusValue.Get() == FarmingStatus.Enabled && updateInfo.Message!.Text!.IsCommand("/disableFarmMode");
    }
}