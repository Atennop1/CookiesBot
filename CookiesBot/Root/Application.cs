using CookiesBot.Gameplay;
using CookiesBot.Loop;

namespace CookiesBot.Root
{
    public sealed class Application
    {
        public void Start()
        {
            var clientFactory = new TelegramClientFactory();
            var client = clientFactory.Create();
            var telegram = new Telegram.Telegram(client);

            var loopObjects = new List<ILoopObject>
            {
                new BasicBot(telegram)
            };
            
            var updatingCycle = new UpdatingLoop(client, loopObjects);
            updatingCycle.Start();
        }
    }
}