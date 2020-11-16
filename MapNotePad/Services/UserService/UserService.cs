using MapNotePad.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        #region --IUserServiceImplementation--

        public string GetFirstName
        {
            get => _settingsManager.FirstName;
        }

        public string GetLastName
        {
            get => _settingsManager.LastName;
        }

        public async Task<int> AddOrUpdateAsync(User user)
        {
           return await _repository.AddOrrUpdateAsync(user);
        }

        public async Task<int> DeleteUserAsync(User user)
        {
         return  await _repository.DeleteItemAsync(user);
        }       

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _repository.GetItemsAsync<User>();
        }

        #endregion
    }
}
