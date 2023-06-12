using Telegram.BotAPI;
using Telegram.BotAPI.AvailableTypes;

namespace CookiesBot.Loop
{
    public interface IUpdateInfo
    {
        UpdateType Type { get; }
        Message? Message { get; }
        CallbackQuery? CallbackQuery { get; }
    }
}