using CookiesBot.Loop;

namespace CookiesBot.Core
{
    public sealed class UsersLoopObjectsFactory : IUsersLoopObjectsFactory
    {
        private readonly ILoopObjectsFactory _loopObjectsFactory;

        public UsersLoopObjectsFactory(ILoopObjectsFactory loopObjectsFactory) 
            => _loopObjectsFactory = loopObjectsFactory ?? throw new ArgumentNullException(nameof(loopObjectsFactory));

        public IUsersLoopObjects Create()
        {
            var usersDictionary = new Dictionary<long, List<ILoopObject>>();
            var usersLoopObjects = new UsersLoopObjects(_loopObjectsFactory, usersDictionary);
            return usersLoopObjects;
        }
    }
}