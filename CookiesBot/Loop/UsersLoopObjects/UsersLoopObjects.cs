﻿using CookiesBot.Core;

namespace CookiesBot.Loop
{
    public sealed class UsersLoopObjects : IUsersLoopObjects
    {
        private readonly ILoopObjectsFactory _loopObjectsFactory;
        private readonly Dictionary<long, List<ILoopObject>> _usersLoopObjects;

        public UsersLoopObjects(ILoopObjectsFactory loopObjectsFactory, Dictionary<long, List<ILoopObject>> usersLoopObjects)
        {
            _loopObjectsFactory = loopObjectsFactory ?? throw new ArgumentNullException(nameof(loopObjectsFactory));
            _usersLoopObjects = usersLoopObjects ?? throw new ArgumentNullException(nameof(usersLoopObjects));
        }

        public bool IsLoopObjectsForUserExist(IUpdateInfo updateInfo)
        {
            return (updateInfo.Type == TypeOfUpdate.Message && _usersLoopObjects.ContainsKey((long)updateInfo.Message?.Chat.Id!)) ||
                (updateInfo.Type == TypeOfUpdate.ButtonCallback && _usersLoopObjects.ContainsKey((long)updateInfo.CallbackQuery?.From.Id!));
        }
        
        public List<ILoopObject> GetLoopObjectsOfUser(IUpdateInfo updateInfo)
        {
            if (!IsLoopObjectsForUserExist(updateInfo))
                throw new InvalidOperationException("Loop objects does not exist for user");
            
            if (updateInfo.Type == TypeOfUpdate.Message && _usersLoopObjects.ContainsKey((long)updateInfo.Message?.Chat.Id!))
                return _usersLoopObjects[updateInfo.Message.Chat.Id];
            
            return _usersLoopObjects[(long)updateInfo.CallbackQuery?.From.Id!];
        }
        
        public void CreateLoopObjectsForNewUser(IUpdateInfo updateInfo)
        {
            if (IsLoopObjectsForUserExist(updateInfo))
                throw new InvalidOperationException("Loop objects for this user already exist");
            
            if (updateInfo.Type == TypeOfUpdate.Message)
                _usersLoopObjects.Add(updateInfo.Message!.Chat.Id, _loopObjectsFactory.Create(updateInfo.Message!.Chat.Id));
            
            if (updateInfo.Type == TypeOfUpdate.ButtonCallback)
                _usersLoopObjects.Add(updateInfo.CallbackQuery!.From.Id, _loopObjectsFactory.Create(updateInfo.CallbackQuery!.From.Id));
        }
    }
}