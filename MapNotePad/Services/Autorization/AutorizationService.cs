using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Services.Autorization
{
    public class AutorizationService : IAutorization
    {
        
        private readonly ISettingsManager Manager;
       
        public AutorizationService(ISettingsManager manager)
        {
            Manager = manager;
        }
        public bool Autorizeted()
        {
            return (Manager.AutorizatedUserId > -1);
        }
        public void LogOut()
        {
            Manager.AutorizatedUserId = -1;
        }

        public int GetActiveUser()
        {
            return Manager.AutorizatedUserId;
        }
        public void SetActiveUser(int id)
        {
            Manager.AutorizatedUserId = id;
        }
    }
}
