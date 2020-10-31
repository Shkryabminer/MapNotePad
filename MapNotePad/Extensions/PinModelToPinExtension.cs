using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Extensions
{
    public static class PinModelToPinExtension
    {
        public static Pin ToPin(this PinModel pinModel)
        {
            Pin pin = new Pin();
            pin.Position = new Position(pinModel.Latitude, pinModel.Longtitude);
            pin.Label = pinModel.Name;

            return pin;
        }
    }
}
