using CookiesBot.Loop;
using RelationalDatabasesViaOOP;

namespace CookiesBot.Core
{
    public sealed class UsersLoopObjectsFactory : IUsersLoopObjectsFactory
    {
        private readonly ILoopObjectsFactory _loopObjectsFactory;
        private readonly IDatabase _database;

        public UsersLoopObjectsFactory(ILoopObjectsFactory loopObjectsFactory, IDatabase database)
        {
            _loopObjectsFactory = loopObjectsFactory ?? throw new ArgumentNullException(nameof(loopObjectsFactory));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public IUsersLoopObjects Create()
        {
            var usersDictionary = new Dictionary<long, List<ILoopObject>>();
            var usersTable = _database.SendReadingRequest("SELECT user_id FROM users");

            for (var i = 0; i < usersTable.Rows.Count; i++)
                usersDictionary.Add((long)usersTable.Rows[i]["user_id"], _loopObjectsFactory.Create((long)usersTable.Rows[i]["user_id"]));

            var usersLoopObjects = new UsersLoopObjects(_loopObjectsFactory, usersDictionary);
            var usersLoopObjectsWithSaving = new UsersLoopObjectsWithSaving(usersLoopObjects, _database);
            return usersLoopObjectsWithSaving;
        }
    }
}