using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Services.Autorization
{
   public interface IAutorization
    {
        bool Autorizeted();
        void LogOut();
        string GetActiveUserEmail();
        void SetActiveUserEmail(User user);

    }
}
