﻿using CookiesBot.Loop;
using CookiesBot.Telegram;
using RelationalDatabasesViaOOP;

namespace CookiesBot.Gameplay
{
    public sealed class GoldenCookieAdderBot : ILoopObject
    {
        private readonly ITelegram _telegram;
        private readonly IDatabase _database;
        private readonly IScreen _screen;
        private readonly RemainingTimeString _remainingTimeString = new();

        public GoldenCookieAdderBot(ITelegram telegram, IDatabase database, IScreen screen)
        {
            _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _screen = screen ?? throw new ArgumentNullException(nameof(screen));
        }

        public TypeOfUpdate RequiredTypeOfUpdate 
            => TypeOfUpdate.ButtonCallback;
        
        public void GetUpdate(IUpdateInfo updateInfo)
        {
            if (!CanGetUpdate(updateInfo))
                throw new InvalidOperationException("Can't get update now");
            
            var cookiesCountTable = _database.SendReadingRequest($"SELECT gold_cookies_count, time_of_last_gold_cookie_getting FROM users WHERE user_id = {updateInfo.CallbackQuery!.From.Id}");
            var timeOfLastGoldenCookie = (DateTime)cookiesCountTable.Rows[0]["time_of_last_gold_cookie_getting"];
            var userCookiesCount = (int)cookiesCountTable.Rows[0]["gold_cookies_count"];

            if (timeOfLastGoldenCookie.Add(TimeSpan.FromHours(1)) < DateTime.Now)
            {
                _database.SendNonQueryRequest($"UPDATE users SET gold_cookies_count = {userCookiesCount + 1}, " +
                    $"time_of_last_gold_cookie_getting = TIMESTAMP '{DateTime.Now:yyyy-MM-dd H:mm:ss}' " +
                    $"WHERE user_id = {updateInfo.CallbackQuery!.From.Id}");
                
                _telegram.SendMessage("+1 золотая печенька!\nСледующую ты сможешь получить лишь через час", updateInfo.CallbackQuery!.From.Id);
                return;
            }
            
            var timeForNextGoldenCookie = timeOfLastGoldenCookie.Add(TimeSpan.FromHours(1)).Subtract(DateTime.Now);
            _telegram.SendMessage($"До получения следующей золотой печеньки осталось: {_remainingTimeString.GetFor(timeForNextGoldenCookie) }", updateInfo.CallbackQuery!.From.Id);
        }

        public bool CanGetUpdate(IUpdateInfo updateInfo) 
            => _screen.IsActive && updateInfo.CallbackQuery!.Data == "add_golden_cookie";
    }
}