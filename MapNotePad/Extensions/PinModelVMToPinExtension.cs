using MapNotePad.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Extensions
{
    public static class PinModelVMToPinExtension
    {
        static public Pin ToPin(this PinModelViewModel viewModel)
        {
            return new Pin
            {
                Label = viewModel.Name,
                Position = new Position(viewModel.Latitude, viewModel.Longtitude),
                Tag = viewModel.KeyWords
            };
        }
    }
}
