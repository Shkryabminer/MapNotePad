using MapNotePad.Models;
using MapNotePad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Services.PinService
{
  public interface IPinService
    {
        Task<int> DeletePinAsync(PinModelViewModel pinVM);
        Task<int> SaveOrUpdatePinAsync(PinModelViewModel pinVM);
     //   Task<IEnumerable<PinModel>> GetPinModelsByEmailAsync(string email);
        Task<IEnumerable<PinModelViewModel>> GetActivePinsAsync();
        //Task<IEnumerable<PinModelViewModel>> GetPinViewModelsByEmailAsync(string email);
        Task<IEnumerable<PinModelViewModel>> GetAllPinsAsync();        

        CameraPosition LoadCameraPosition();

        void SaveCameraPosotion(CameraPosition cameraPosition);
       
      

    }
}
