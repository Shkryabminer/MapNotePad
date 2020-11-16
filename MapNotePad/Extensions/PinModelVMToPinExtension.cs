using MapNotePad.ViewModels;
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
                Tag = new string[] { viewModel.KeyWords, viewModel.Picture }
            };
        }
    }
}
