using CookiesBot.Gameplay;
using CookiesBot.Loop;
using CookiesBot.Telegram;

namespace CookiesBot.Core
{
    public sealed class LoopObjectsFactory : ILoopObjectsFactory
    {
        private readonly ITelegram _telegram;

        public LoopObjectsFactory(ITelegram telegram) 
            => _telegram = telegram ?? throw new ArgumentNullException(nameof(telegram));

        public List<ILoopObject> Create()
        {
            var loopObjects = new List<ILoopObject>
            {
                new ExampleBot(_telegram)
            };

            return loopObjects;
        }
    }
}