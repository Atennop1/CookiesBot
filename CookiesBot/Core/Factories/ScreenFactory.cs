using CookiesBot.Gameplay;
using RelationalDatabasesViaOOP;

namespace CookiesBot.Core
{
    public sealed class ScreenFactory
    {
        private readonly IDatabase _database;
        private readonly long _userId;
        
        private int _lastCreatedObjectId;

        public ScreenFactory(IDatabase database, long userId)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _userId = userId;
        }

        public IScreen Create()
        {
            var screenEnabled = new Screen(_database, _lastCreatedObjectId, _userId);
            _lastCreatedObjectId++;
            return screenEnabled;
        }
    }
}