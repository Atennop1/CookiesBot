using Telegram.BotAPI.AvailableTypes;

namespace CookiesBot.Loop
{
    public interface IUpdateInfo
    {
        TypeOfUpdate Type { get; }
        Message? Message { get; }
        CallbackQuery? CallbackQuery { get; }
    }
}