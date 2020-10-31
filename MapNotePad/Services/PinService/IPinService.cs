using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Services.PinService
{
  public interface IPinService
    {
        List<Pin> GetPins(int id);
        void DeletePin(PinModel prof);
        void SaveOrUpdatePin(PinModel profile);
        List<PinModel> GetPinModels(int id);
        List<PinModel> GetActivePins(int id);
      

    }
}
