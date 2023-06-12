using RelationalDatabasesViaOOP;
using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace CookiesBot.Loop
{
    public sealed class UsersLoopObjectsWithSaving : IUsersLoopObjects
    {
        private readonly IUsersLoopObjects _usersLoopObjects;
        private readonly IDatabase _database;

        public UsersLoopObjectsWithSaving(IUsersLoopObjects usersLoopObjects, IDatabase database)
        {
            _usersLoopObjects = usersLoopObjects ?? throw new ArgumentNullException(nameof(usersLoopObjects));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public bool IsLoopObjectsForUserExist(Update update)
            => _usersLoopObjects.IsLoopObjectsForUserExist(update);

        public List<ILoopObject> GetLoopObjectsOfUser(Update update)
            => _usersLoopObjects.GetLoopObjectsOfUser(update);

        public void CreateLoopObjectsForNewUser(Update update)
        {
            var userId = update.Type switch
            {
                UpdateType.Message => update.Message!.Chat.Id,
                UpdateType.CallbackQuery => update.CallbackQuery!.From.Id,
                _ => 0L
            };

            _database.SendNonQueryRequest($"INSERT INTO users (user_id) VALUES ({userId})");
            _usersLoopObjects.CreateLoopObjectsForNewUser(update);
        }
    }
}