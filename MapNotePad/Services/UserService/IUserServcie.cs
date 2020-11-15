using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services.UserService
{
   public interface IUserServcie
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<int> AddOrUpdateAsync(User user);

        Task<int> DeleteUserAsync(User user);

        string GetFirstName { get;}

        string GetLastName { get; }
    }
}
