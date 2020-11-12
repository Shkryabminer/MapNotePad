using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Services.UserService
{
   public interface IUserServcie
    {
        IEnumerable<User> GetUsers();

        int AddOrUpdate(User user);

        void DeleteUser(User user);

        string GetFirstName();

        string GetLastName();
    }
}
