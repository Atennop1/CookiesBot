using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;

namespace CookiesBot.Telegram
{
    public sealed class Telegram : ITelegram
    {
        private readonly BotClient _client;

        public Telegram(BotClient client) 
            => _client = client ?? throw new ArgumentNullException(nameof(client));

        public void SendMessage(string text, long id)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            _client.SendMessage(text: text, chatId: id);
        }
    }
}