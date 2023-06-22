using CookiesBot.Loop;
using CookiesBot.Telegram;
using CookiesBot.Tools;

namespace CookiesBot.Gameplay
{
    public sealed class FarmModeDisablingBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IScreen _screen;

        public FarmModeDisablingBot(ITelegram telegram, IScreen screen)
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

            _telegram.SendMessage("Режим фермы отключен", updateInfo.Message!.From!.Id);
            _screen.Disable();
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => _screen.IsActive && updateInfo.Message!.Text!.IsCommand("/disableFarmMode");
    }
}