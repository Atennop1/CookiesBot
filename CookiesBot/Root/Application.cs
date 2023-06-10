using CookiesBot.Loop;

namespace CookiesBot.Root
{
    public sealed class Application
    {
        public void Start()
        {
            var clientFactory = new TelegramClientFactory();
            var client = clientFactory.Create();

            var updatingCycle = new UpdatingLoop(client);
            updatingCycle.Start();
        }
    }
}