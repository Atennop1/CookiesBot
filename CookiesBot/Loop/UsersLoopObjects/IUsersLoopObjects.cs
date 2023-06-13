namespace CookiesBot.Loop
{
    public interface IUsersLoopObjects
    {
        bool IsLoopObjectsForUserExist(IUpdateInfo updateInfo);
        
        List<ILoopObject> GetLoopObjectsOfUser(IUpdateInfo updateInfo);
        void CreateLoopObjectsForNewUser(IUpdateInfo updateInfo);
    }
}