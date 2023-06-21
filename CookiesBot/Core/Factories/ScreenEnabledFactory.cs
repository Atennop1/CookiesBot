using CookiesBot.Gameplay;
using RelationalDatabasesViaOOP;

namespace CookiesBot.Core
{
    public sealed class ScreenEnabledFactory
    {
        private readonly IDatabase _database;
        private readonly long _userId;
        
        private int _lastCreatedObjectId;

        public ScreenEnabledFactory(IDatabase database, long userId)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _userId = userId;
        }

        public IScreenEnabled Create()
        {
            var screenEnabled = new ScreenEnabled(_database, _lastCreatedObjectId, _userId);
            _lastCreatedObjectId++;
            return screenEnabled;
        }
    }
}