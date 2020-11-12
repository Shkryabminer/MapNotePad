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
        void DeletePin(PinModelViewModel pinVM);
        void SaveOrUpdatePin(PinModelViewModel pinVM);
        IEnumerable<PinModelViewModel> GetPinModels(string email);
        IEnumerable<PinModelViewModel> GetActivePinsByEmail(string email);

        IEnumerable<PinModelViewModel> GetAllPins();        

        CameraPosition LoadCameraPosition();

        void SaveCameraPosotion(CameraPosition cameraPosition);
       
      

    }
}
