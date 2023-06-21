using CookiesBot.Loop;

namespace CookiesBot.Core
{
    public interface ILoopObjectsFactory
    {
        List<ILoopObject> Create(long userId);
    }
}