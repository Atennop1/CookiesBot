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
                   
                    if (updateInfo.Type == TypeOfUpdate.Unknown)
                        continue;
                    
                    if (!_usersLoopObjects.IsLoopObjectsForUserExist(updateInfo))
                        _usersLoopObjects.CreateLoopObjectsForNewUser(updateInfo);

                    foreach (var loopObject in _usersLoopObjects.GetLoopObjectsOfUser(updateInfo))
                    {
                        if (!loopObject.RequiredTypeOfUpdate.HasFlag(updateInfo.Type) || !loopObject.CanGetUpdate(updateInfo)) 
                            continue;
                        
                        loopObject.GetUpdate(updateInfo);
                        break;
                    }
                }

                var offset = updates.Last().UpdateId + 1;
                updates = _client.GetUpdates(offset, limit: 10, timeout: 60);
            }
        }
    }
}