using MapNotePad.Models;
using System.Collections.Generic;

namespace MapNotePad.Services.UserService
{
    public class UserService : IUserServcie
    {
        private readonly IRepository _repository;
      
        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        #region --IUserServiceImplementation--
      
        public int AddOrUpdate(User user)
        {
           return _repository.AddOrrUpdate(user);
        }

        public void DeleteUser(User user)
        {
            _repository.DeleteItem(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return _repository.GetItems<User>();
        }
        #endregion
    }
}
