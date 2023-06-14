namespace CookiesBot.Gameplay
{
    public sealed class RemainingTimeString
    {
        public string GetFor(TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.FromMinutes(1))
                return $"{timeSpan.Seconds} {GetWordForm(timeSpan.Seconds, "секунда")}";
            
            if (timeSpan.Seconds == 0)
                return $"{timeSpan.Minutes} {GetWordForm(timeSpan.Minutes, "минута")}";
            
            return $"{timeSpan.Minutes} {GetWordForm(timeSpan.Minutes, "минута")} и {timeSpan.Seconds} {GetWordForm(timeSpan.Seconds, "секунда")}";
        }

        private string GetWordForm(int number, string baseForm)
        {
            if (number is >= 5 and <= 20)
                return baseForm.Remove(baseForm.Length - 1);
                
            Math.DivRem(number, 10, out var remainder);
            return remainder switch
            {
                0 or 5 or 6 or 7 or 8 or 9 => baseForm.Remove(baseForm.Length - 1),
                1 => baseForm,
                2 or 3 or 4 => baseForm.Remove(baseForm.Length - 1) + "ы"
            };
        }
    }
}