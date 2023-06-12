using Telegram.BotAPI;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;

namespace CookiesBot.Loop
{
    public sealed class LibraryUpdateInfoAdapter : IUpdateInfo
    {
        private readonly Update _update;

        public LibraryUpdateInfoAdapter(Update update) 
            => _update = update ?? throw new ArgumentNullException(nameof(update));

        public UpdateType Type 
            => _update.Type;
        
        public Message? Message 
            => _update.Message;
        
        public CallbackQuery? CallbackQuery 
            => _update.CallbackQuery;
    }
}