using RelationalDatabasesViaOOP;

namespace CookiesBot.Gameplay
{
    public sealed class Screen : IScreen
    {
        private readonly IDatabase _database;
        private readonly int _id;
        private readonly long _userId;

        public Screen(IDatabase database, int id, long userId)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _id = id;
            _userId = userId;
            
            var loadedId = _database.SendReadingRequest($"SELECT selected_screen_id FROM users WHERE user_id = {userId}").Rows[0]["selected_screen_id"];
            IsActive = loadedId.GetType() != typeof(DBNull) && (int)loadedId == _id;
        }
        
        public bool IsActive { get; private set; }

        public void Enable()
        {
            IsActive = true;
            _database.SendNonQueryRequest($"UPDATE users SET selected_screen_id = {_id} WHERE user_id = {_userId}");
        }

        public void Disable()
        {
            IsActive = false;
            var loadedId = _database.SendReadingRequest($"SELECT selected_screen_id FROM users WHERE user_id = {_userId}").Rows[0]["selected_screen_id"];
            
            if (loadedId.GetType() != typeof(DBNull) && (int)loadedId == _id)
                _database.SendNonQueryRequest($"UPDATE users SET selected_screen_id = -1 WHERE user_id = {_userId}");
        }
    }
}