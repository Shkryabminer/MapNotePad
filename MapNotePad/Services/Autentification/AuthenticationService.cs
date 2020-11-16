using System.Collections.Generic;
using System.Linq;
using MapNotePad.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MapNotePad.Services
{
    public  class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository _data;
       
        public AuthenticationService(IRepository repository)
        {
            _data = repository;
        }
       
        #region --IAutentification implementation--

        public async Task<IUser> GetAuthUserAsync(string email, string password)
        {
            IUser user;

            bool isAuthentificated = await IsAutenficatedAsync(email, password);

            if(isAuthentificated)
            {
                var collection = await GetUsersAsync(email, password);
                
                user = collection.First();
            }
            else
            {
                user = null;
                Debug.WriteLine("User is not authenticated");
            }

            return user;
        }

        public async Task<bool> IsAutenficatedAsync(string login, string password)
        {
            var collection = await GetUsersAsync(login, password);
            
            return (collection != null && collection.Any());
        }

        #endregion


        #region --Private helpers--

        private async Task<IEnumerable<User>> GetUsersAsync(string login, string password)
        {
            var collection = from user in await _data.GetItemsAsync<User>()
                             where user.Email.ToUpper() == login.ToUpper() && user.Password == password
                             select user;

            return collection;
        }

        #endregion
    }
}
