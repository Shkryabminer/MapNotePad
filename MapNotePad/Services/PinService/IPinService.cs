using MapNotePad.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Services.PinService
{
    public interface IPinService
    {
        Task<int> DeletePinAsync(PinModelViewModel pinVM);

        Task<int> SaveOrUpdatePinAsync(PinModelViewModel pinVM);

        Task<IEnumerable<PinModelViewModel>> GetActivePinsAsync();

        Task<IEnumerable<PinModelViewModel>> GetAllPinsAsync();

        CameraPosition LoadCameraPosition();

        void SaveCameraPosotion(CameraPosition cameraPosition);
    }
}
