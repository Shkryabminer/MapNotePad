using MapNotePad.Models;
using System.Collections.Generic;

namespace MapNotePad.Services.UserService
{
    public class UserService : IUserServcie
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;

        public UserService(IRepository repository,
                            ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        #region --Public properties--



        #endregion

        #region --IUserServiceImplementation--

        public string GetFirstName()
        {
            return _settingsManager.FirstName;
        }

        public string GetLastName()
        {
            return _settingsManager.LastName;
        }

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
