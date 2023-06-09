﻿using CookiesBot.Gameplay;
using CookiesBot.Loop;
using CookiesBot.Telegram;
using RelationalDatabasesViaOOP;

namespace CookiesBot.Core
{
    public sealed class LoopObjectsFactory : ILoopObjectsFactory
    {
        private readonly ITelegram _telegram;
        private readonly IDatabase _database;
        private readonly IItems _items = new ItemsFactory().Create();

        public LoopObjectsFactory(ITelegram telegram, IDatabase database)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public List<ILoopObject> Create(long userId)
        {
            var screenEnabledFactory = new ScreenFactory(_database, userId);
            var farmingScreenEnabled = screenEnabledFactory.Create();
            var inventory = new InventoryWithSaving(new Inventory(), _database, _items, userId);
            
            var loopObjects = new List<ILoopObject>
            {
                new HelloBot(_telegram),
                new BalanceBot(_telegram, _database),
                new FarmModeEnablingBot(_telegram, farmingScreenEnabled),
                new FarmModeDisablingBot(_telegram, farmingScreenEnabled),
                new CookieAdderBot(_telegram, _database, farmingScreenEnabled),
                new GoldenCookieAdderBot(_telegram, _database, farmingScreenEnabled),
                new ShowInventoryBot(_telegram, inventory)
            };

            return loopObjects;
        }
    }
}