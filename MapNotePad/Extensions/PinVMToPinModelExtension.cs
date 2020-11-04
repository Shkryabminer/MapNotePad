using MapNotePad.Models;
using MapNotePad.ViewModels;

namespace MapNotePad.Extensions
{
    static  class PinVMToPinModelExtension
    {
        public static PinModel ToPinModel(this PinModelViewModel vm)
        {
            PinModel pm = new PinModel()
            {
                ID = vm.ID,
                Latitude = vm.Latitude,
                Longtitude = vm.Longtitude,
                KeyWords = vm.KeyWords,
                UserID = vm.UserID,
                IsActive = vm.IsActive,
                Name = vm.Name
            };

            return pm;
        }
    }
}
