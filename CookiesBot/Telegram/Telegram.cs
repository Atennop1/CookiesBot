using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;

namespace CookiesBot.Telegram
{
    public sealed class Telegram : ITelegram
    {
        private readonly BotClient _client;

        public Telegram(BotClient client) 
            => _client = client ?? throw new ArgumentNullException(nameof(client));

        public void SendMessage(string text, long id, ReplyMarkup replyMarkup = null!)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            try { _client.SendMessage(text: text, chatId: id, replyMarkup: replyMarkup); }
            catch { /*ignored*/ }
        }
    }
}