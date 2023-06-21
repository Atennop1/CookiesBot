using RelationalDatabasesViaOOP;

namespace CookiesBot.Gameplay
{
    public sealed class ScreenEnabled : IScreenEnabled
    {
        private readonly IDatabase _database;
        private readonly int _screenId;
        private readonly long _userId;
        
        private bool _value;

        public ScreenEnabled(IDatabase database, int screenId, long userId)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _screenId = screenId;
            _userId = userId;
            
            var loadedId = _database.SendReadingRequest($"SELECT selected_screen_id FROM users WHERE user_id = {userId}").Rows[0]["selected_screen_id"];
            _value = loadedId.GetType() != typeof(DBNull) && (int)loadedId == _screenId;
        }

        public void Set(bool value)
        {
            _value = value;

            _database.SendNonQueryRequest(value
                ? $"UPDATE users SET selected_screen_id = {_screenId} WHERE user_id = {_userId}"
                : $"UPDATE users SET selected_screen_id = -1 WHERE user_id = {_userId}");
        }

        public bool Get()
            => _value;
    }
}