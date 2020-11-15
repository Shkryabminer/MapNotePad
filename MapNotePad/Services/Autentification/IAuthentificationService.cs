using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services
{
  public  interface IAuthentificationService
    {
    
       Task<bool> IsAutenficatedAsync(string login, string password);
        Task<IUser> GetAuthUserAsync(string login, string password);

    }
}
