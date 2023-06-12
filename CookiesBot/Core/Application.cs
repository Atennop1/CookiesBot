using CookiesBot.Loop;

namespace CookiesBot.Core
{
    public sealed class Application
    {
        public void Start()
        {
            var clientFactory = new TelegramClientFactory();
            var client = clientFactory.Create();
            var telegram = new Telegram.Telegram(client);
            
            var loopObjectsFactory = new LoopObjectsFactory(telegram);
            var databaseFactory = new RelationalDatabaseFactory();
            var usersLoopObjectsFactory = new UsersLoopObjectsFactory(loopObjectsFactory, databaseFactory);
            var usersLoopObjects = usersLoopObjectsFactory.Create();
            
            var updatingCycle = new UpdatingLoop(client, usersLoopObjects);
            updatingCycle.Activate();
        }
    }
}