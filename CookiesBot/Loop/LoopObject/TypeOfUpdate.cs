namespace CookiesBot.Loop
{
    [Flags]
    public enum TypeOfUpdate
    {
        Unknown = 1,
        Message = 2,
        ButtonCallback = 4
    }
}