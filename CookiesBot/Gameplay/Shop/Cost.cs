using CookiesBot.Tools;

namespace CookiesBot.Gameplay
{
    public sealed class Cost : ICost
    {
        public int AverageCookies { get; }
        public int GoldenCookies { get; }

        public Cost(int averageCookies = 0, int goldenCookies = 0)
        {
            AverageCookies = averageCookies.ThrowExceptionIfLessThanZero();
            GoldenCookies = goldenCookies.ThrowExceptionIfLessThanZero();
        }
    }
}