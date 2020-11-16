﻿using MapNotePad.Models;

namespace MapNotePad.Services.Autorization
{
    public class AutorizationService : IAutorizationService
    {        
        private readonly ISettingsManager _manager;
       
        public AutorizationService(ISettingsManager manager)
        {
            _manager = manager;
        }

        #region --Autorizatio implementation --

        public bool Autorizeted()
        {
            return (!string.IsNullOrEmpty(_manager.AutorizatedUserEmail));
        }

        public void LogOut()
        {
            _manager.AutorizatedUserEmail = string.Empty;
        }

        public string GetActiveUserEmail()
        {
            return _manager.AutorizatedUserEmail;
        }

        public void SetActiveUserEmail(User user)
        {
            _manager.AutorizatedUserEmail =user.Email;
            _manager.FirstName = user.FirstName;
            _manager.LastName = user.LastName;
        }

        #endregion
    }
}
