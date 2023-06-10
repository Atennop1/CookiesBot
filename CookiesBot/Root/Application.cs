using CookiesBot.Loop;

namespace CookiesBot.Root
{
    public sealed class Application
    {
        public void Start()
        {
            var clientFactory = new TelegramClientFactory();
            var client = clientFactory.Create();

            var loopObjects = new List<ILoopObject>()
            {

            };
            
            var updatingCycle = new UpdatingLoop(client, loopObjects);
            updatingCycle.Start();
        }
    }
}