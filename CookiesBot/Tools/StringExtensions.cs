namespace CookiesBot.Tools
{
    public static class StringExtensions
    {
        public static bool IsCommand(this string str, string command) 
            => str == command || str == $"{command}@CookiesAtennop_bot";
    }
}