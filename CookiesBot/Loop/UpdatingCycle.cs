using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace CookiesBot.Loop
{
    public sealed class UpdatingCycle : IUpdatingCycle
    {
        private readonly BotClient _client;

        public UpdatingCycle(BotClient client) 
            => _client = client ?? throw new ArgumentNullException(nameof(client));

        public void Start()
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
                    //there will be logic of updates handling
                }

                var offset = updates.Last().UpdateId + 1;
                updates = _client.GetUpdates(offset, limit: 10, timeout: 60);
            }
        }
    }
}