using MapNotePad.Models;
using MapNotePad.ViewModels;

namespace MapNotePad.Extensions
{
    static class PinModelToViewModelExtension
    {
        public static PinModelViewModel ToViewModel(this PinModel pinModel)
        {
            return new PinModelViewModel(pinModel);
        }

    }
}
