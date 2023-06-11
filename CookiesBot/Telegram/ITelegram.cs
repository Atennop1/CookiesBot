using Telegram.BotAPI.AvailableTypes;

namespace CookiesBot.Telegram
{
    public interface ITelegram
    {
        void SendMessage(string text, long id, ReplyMarkup replyMarkup = null!);
    }
}