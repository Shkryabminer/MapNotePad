using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Services.PinService
{
    public class PinService : IPinService
    {
        private readonly IRepository _repository;

        public PinService(IRepository repository)
        {            
            _repository = repository;
        }

        #region --IProfileService impement--

        public void DeletePin(PinModelViewModel prof)
        {
            _repository.DeleteItem(prof.ToPinModel());
        }

        public IEnumerable<PinModelViewModel> GetPinModels(int id)
        {
            return _repository.GetItems<PinModel>().Where(x => x.UserID == id)
                              .Select(pin => pin.ToViewModel());


        }

        public IEnumerable<PinModelViewModel> GetActivePins(int id)
        {
            return GetPinModels(id).Where(x => x.IsActive);
        }



        public void SaveOrUpdatePin(PinModelViewModel pin)
        {
            _repository.AddOrrUpdate(pin.ToPinModel());
        }



        #endregion

        #region --Private helpers--

        #endregion
    }
}



