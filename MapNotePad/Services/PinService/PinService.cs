using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Services.PinService
{
    public class PinService : IPinService
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;

        public PinService(IRepository repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        #region --IProfileService impement--

        public async Task<int> DeletePinAsync(PinModelViewModel prof)
        {
            return await _repository.DeleteItemAsync(prof.ToPinModel());
        }

        public async Task<IEnumerable<PinModelViewModel>> GetAllPinsAsync()
        {
            return await GetPinViewModelsByEmailAsync();
        }


        public async Task<IEnumerable<PinModelViewModel>> GetActivePinsAsync()
        {
            var listModels = await GetPinViewModelsByEmailAsync();

            return listModels.Where(x => x.IsActive);
        }

        public async Task<int> SaveOrUpdatePinAsync(PinModelViewModel pin)
        {
            return await _repository.AddOrrUpdateAsync(pin.ToPinModel());
        }

        public CameraPosition LoadCameraPosition()
        {
            return new CameraPosition(new Position(_settingsManager.CameraLatitude,
                                                   _settingsManager.CameraLongitude),
                                                   _settingsManager.Zoom);
        }

        public void SaveCameraPosotion(CameraPosition cameraPosition)
        {
            _settingsManager.CameraLatitude = cameraPosition.Target.Latitude;
            _settingsManager.CameraLongitude = cameraPosition.Target.Longitude;
            _settingsManager.Zoom = cameraPosition.Zoom;
        }


        #endregion

        #region --Private helpers--

        public async Task<IEnumerable<PinModelViewModel>> GetPinViewModelsByEmailAsync()
        {
            var email = _settingsManager.AutorizatedUserEmail;

            var listPM = await _repository.FindByAsync<PinModel>(x => x.UserEmail == email);

            return listPM.Select(x => x.ToViewModel());
        }
        #endregion
    }
}



