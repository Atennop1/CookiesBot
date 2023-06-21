using CookiesBot.Gameplay;
using CookiesBot.Loop;
using CookiesBot.Telegram;
using RelationalDatabasesViaOOP;

namespace CookiesBot.Core
{
    public sealed class LoopObjectsFactory : ILoopObjectsFactory
    {
        private readonly ITelegram _telegram;
        private readonly IDatabase _database;

        public LoopObjectsFactory(ITelegram telegram, IDatabase database)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public List<ILoopObject> Create()
        {
            var farmingStatusValue = new ScreenEnabled();
            
            var loopObjects = new List<ILoopObject>
            {
                new HelloBot(_telegram),
                new BalanceBot(_telegram, _database),
                new FarmModeEnablingBot(_telegram, farmingStatusValue),
                new FarmModeDisablingBot(_telegram, farmingStatusValue),
                new CookieAdderBot(_telegram, _database, farmingStatusValue),
                new GoldenCookieAdderBot(_telegram, _database, farmingStatusValue)
            };

            return loopObjects;
        }
    }
}