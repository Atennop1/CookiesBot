using RelationalDatabasesViaOOP;
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

        public bool IsLoopObjectsForUserExist(IUpdateInfo updateInfo)
            => _usersLoopObjects.IsLoopObjectsForUserExist(updateInfo);

        public List<ILoopObject> GetLoopObjectsOfUser(IUpdateInfo updateInfo)
            => _usersLoopObjects.GetLoopObjectsOfUser(updateInfo);

        public void CreateLoopObjectsForNewUser(IUpdateInfo updateInfo)
        {
            var userId = updateInfo.Type switch
            {
                TypeOfUpdate.Message => updateInfo.Message!.Chat.Id,
                TypeOfUpdate.ButtonCallback => updateInfo.CallbackQuery!.From.Id,
                _ => 0L
            };

            _database.SendNonQueryRequest($"INSERT INTO users (user_id) VALUES ({userId})");
            _usersLoopObjects.CreateLoopObjectsForNewUser(updateInfo);
        }
    }
}