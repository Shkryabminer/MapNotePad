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
        void DeletePin(PinModelViewModel prof);
        void SaveOrUpdatePin(PinModelViewModel profile);
        IEnumerable<PinModelViewModel> GetPinModels(int id);
        IEnumerable<PinModelViewModel> GetActivePins(int id);

         CameraPosition LoadCameraPosition();

        void SaveCameraPosotion(CameraPosition cameraPosition);
       
      

    }
}
