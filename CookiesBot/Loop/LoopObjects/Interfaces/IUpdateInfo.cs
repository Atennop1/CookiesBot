using Telegram.BotAPI;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.InlineMode;

namespace CookiesBot.Loop
{
    public interface IUpdateInfo
    {
        UpdateType Type { get; }
        Message? Message { get; }
        
        InlineQuery? InlineQuery { get; }
        ChosenInlineResult? ChosenInlineResult { get; }
    }
}