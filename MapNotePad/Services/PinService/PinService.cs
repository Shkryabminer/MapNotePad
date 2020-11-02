using MapNotePad.Extensions;
using MapNotePad.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Services.PinService
{
    public class PinService : IPinService
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IRepository _repository;       

        public PinService(ISettingsManager settingsManager, IRepository repository)
        {
            _settingsManager = settingsManager;
            _repository = repository;
        }

        #region --IProfileService impement--
      
        public void DeletePin(PinModel prof)
        {
            _repository.DeleteItem(prof);
        }

        public List<PinModel> GetPinModels(int id)
        {
            return _repository.GetItems<PinModel>().Where(x=>x.UserID==id).ToList();
        }

        public List<PinModel> GetActivePins(int id)
        {            
            return GetPinModels(id).Where(x=>x.IsActive==true).ToList();
        }
        
        public List<Pin> GetPins(int id)
        {
            List<Pin> pins = new List<Pin>();
            var pinModels= GetPinModels(id);
           
            foreach (PinModel p in pinModels)
            {
                pins.Add(p.ToPin());
            }
            return pins;
        }

        public void SaveOrUpdatePin(PinModel pin)
        {
            _repository.AddOrrUpdate(pin);
        }

        #endregion

        #region --Private helpers--
       
        #endregion
    }
}

        
    
