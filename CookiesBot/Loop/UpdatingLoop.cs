using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace CookiesBot.Loop
{
    public sealed class UpdatingLoop : IUpdatingLoop
    {
        private readonly BotClient _client;
        private readonly IUsersLoopObjects _usersLoopObjects;

        public UpdatingLoop(BotClient client, IUsersLoopObjects usersLoopObjects)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _usersLoopObjects = usersLoopObjects ?? throw new ArgumentNullException(nameof(usersLoopObjects));
        }

        public void Activate()
        {
            var updates = _client.GetUpdates();

            while (true)
            {
                if (!updates.Any())
                {
                    updates = _client.GetUpdates();
                    continue;
                }
                
                foreach (var update in updates)
                {
                    var updateInfo = new LibraryUpdateInfoAdapter(update);
                   
                    if (!_usersLoopObjects.IsLoopObjectsForUserExist(update))
                        _usersLoopObjects.CreateLoopObjectsForNewUser(update);

                    foreach (var loopObject in _usersLoopObjects.GetLoopObjectsOfUser(update))
                    {
                        if (loopObject.RequiredUpdateType != update.Type || !loopObject.CanGetUpdate(updateInfo)) 
                            continue;
                        
                        loopObject.GetUpdate(updateInfo);
                    }
                }

                var offset = updates.Last().UpdateId + 1;
                updates = _client.GetUpdates(offset, limit: 10, timeout: 60);
            }
        }
    }
}