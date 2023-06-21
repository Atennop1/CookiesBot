using RelationalDatabasesViaOOP;

namespace CookiesBot.Core
{
    public interface IDatabaseFactory
    {
        IDatabase Create();
    }
}