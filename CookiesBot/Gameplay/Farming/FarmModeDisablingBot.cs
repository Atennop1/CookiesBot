using CookiesBot.Loop;
using CookiesBot.Telegram;
using CookiesBot.Tools;

namespace CookiesBot.Gameplay
{
    public sealed class FarmModeDisablingBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IScreenEnabled _screenEnabled;

        public FarmModeDisablingBot(ITelegram telegram, IScreenEnabled screenEnabled)
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

            _telegram.SendMessage("Режим фермы отключен", updateInfo.Message!.From!.Id);
            _screenEnabled.Set(false);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => _screenEnabled.Get() && updateInfo.Message!.Text!.IsCommand("/disableFarmMode");
    }
}