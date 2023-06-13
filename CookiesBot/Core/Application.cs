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
            
            var databaseFactory = new RelationalDatabaseFactory();
            var database = databaseFactory.Create();
            
            var loopObjectsFactory = new LoopObjectsFactory(telegram, database);
            var usersLoopObjectsFactory = new UsersLoopObjectsFactory(loopObjectsFactory, database);
            var usersLoopObjects = usersLoopObjectsFactory.Create();
            
            var updatingCycle = new UpdatingLoop(client, usersLoopObjects);
            updatingCycle.Activate();
        }
    }
}