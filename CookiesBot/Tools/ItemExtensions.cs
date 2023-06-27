using CookiesBot.Gameplay;

namespace CookiesBot.Tools
{
    public static class ItemExtensions
    {
        public static bool IsEquals(this IItem first, IItem second)
            => first.Name == second.Name && first.Description == second.Description;
    }
}