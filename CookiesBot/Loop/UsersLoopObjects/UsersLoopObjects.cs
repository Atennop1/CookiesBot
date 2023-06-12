using CookiesBot.Core;
using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace CookiesBot.Loop
{
    public sealed class UsersLoopObjects : IUsersLoopObjects
    {
        private readonly ILoopObjectsFactory _loopObjectsFactory;
        private readonly Dictionary<long, List<ILoopObject>> _usersLoopObjects;

        public UsersLoopObjects(ILoopObjectsFactory loopObjectsFactory, Dictionary<long, List<ILoopObject>> usersLoopObjects)
        {
            _loopObjectsFactory = loopObjectsFactory ?? throw new ArgumentNullException(nameof(loopObjectsFactory));
            _usersLoopObjects = usersLoopObjects ?? throw new ArgumentNullException(nameof(usersLoopObjects));
        }

        public bool IsLoopObjectsForUserExist(Update update)
        {
            return (update.Type == UpdateType.Message && _usersLoopObjects.ContainsKey((long)update.Message?.Chat.Id!)) ||
                (update.Type == UpdateType.CallbackQuery && _usersLoopObjects.ContainsKey((long)update.CallbackQuery?.From.Id!));
        }
        
        public List<ILoopObject> GetLoopObjectsOfUser(Update update)
        {
            if (!IsLoopObjectsForUserExist(update))
                throw new InvalidOperationException("Loop objects does not exist for user");
            
            if (update.Type == UpdateType.Message && _usersLoopObjects.ContainsKey((long)update.Message?.Chat.Id!))
                return _usersLoopObjects[update.Message.Chat.Id];
            
            return _usersLoopObjects[(long)update.CallbackQuery?.From.Id!];
        }
        
        public void CreateLoopObjectsForNewUser(Update update)
        {
            if (IsLoopObjectsForUserExist(update))
                throw new InvalidOperationException("Loop objects for this user already exist");
            
            if (update.Type == UpdateType.Message)
                _usersLoopObjects.Add(update.Message!.Chat.Id, _loopObjectsFactory.Create());
            
            if (update.Type == UpdateType.CallbackQuery)
                _usersLoopObjects.Add(update.CallbackQuery!.From.Id, _loopObjectsFactory.Create());
        }
    }
}