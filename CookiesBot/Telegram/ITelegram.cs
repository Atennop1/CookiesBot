namespace CookiesBot.Telegram
{
    public interface ITelegram
    {
        void SendMessage(string text, long id);
    }
}