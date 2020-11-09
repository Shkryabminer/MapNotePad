using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        public void DeletePin(PinModelViewModel prof)
        {
            _repository.DeleteItem(prof.ToPinModel());
        }

        public IEnumerable<PinModelViewModel> GetPinModels(int id)
        {
            return _repository.GetItems<PinModel>().Where(x => x.UserID == id)
                              .Select(pin => pin.ToViewModel());
        }

        public IEnumerable<PinModelViewModel> GetActivePins(int id)
        {
            return GetPinModels(id).Where(x => x.IsActive);
        }



        public void SaveOrUpdatePin(PinModelViewModel pin)
        {
            _repository.AddOrrUpdate(pin.ToPinModel());
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

        #endregion
    }
}



