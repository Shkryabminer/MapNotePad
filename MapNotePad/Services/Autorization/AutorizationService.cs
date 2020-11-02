using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Services.Autorization
{
    public class AutorizationService : IAutorization
    {
        
        private readonly ISettingsManager _manager;
       
        public AutorizationService(ISettingsManager manager)
        {
            _manager = manager;
        }

        #region --Autorizatio implementation --

        public bool Autorizeted()
        {
            return (_manager.AutorizatedUserId > -1);
        }
        public void LogOut()
        {
            _manager.AutorizatedUserId = -1;
        }

        public int GetActiveUser()
        {
            return _manager.AutorizatedUserId;
        }
        public void SetActiveUser(int id)
        {
            _manager.AutorizatedUserId = id;
        }

        #endregion
    }
}
