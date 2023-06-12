using System.Data;
using CookiesBot.Loop;

namespace CookiesBot.Core
{
    public sealed class UsersLoopObjectsFactory : IUsersLoopObjectsFactory
    {
        private readonly ILoopObjectsFactory _loopObjectsFactory;
        private readonly IDatabaseFactory _databaseFactory;

        public UsersLoopObjectsFactory(ILoopObjectsFactory loopObjectsFactory, IDatabaseFactory databaseFactory)
        {
            _loopObjectsFactory = loopObjectsFactory ?? throw new ArgumentNullException(nameof(loopObjectsFactory));
            _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
        }

        public IUsersLoopObjects Create()
        {
            var database = _databaseFactory.Create();
            var usersDictionary = new Dictionary<long, List<ILoopObject>>();
            
            var users = database.SendReaderRequest("SELECT user_id FROM users");
            var usersTable = new DataTable();
            usersTable.Load(users);

            for (var i = 0; i < usersTable.Rows.Count; i++)
                usersDictionary.Add((long)usersTable.Rows[i]["user_id"], _loopObjectsFactory.Create());

            var usersLoopObjects = new UsersLoopObjects(_loopObjectsFactory, usersDictionary);
            var usersLoopObjectsWithSaving = new UsersLoopObjectsWithSaving(usersLoopObjects, database);
            return usersLoopObjectsWithSaving;
        }
    }
}