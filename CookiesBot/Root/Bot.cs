using CookiesBot.Loop;

namespace CookiesBot.Root
{
    public sealed class Bot
    {
        public void Start()
        {
            var clientFactory = new BotClientFactory();
            var client = clientFactory.Create();

            var updatingCycle = new UpdatingCycle(client);
            updatingCycle.Start();
        }
    }
}