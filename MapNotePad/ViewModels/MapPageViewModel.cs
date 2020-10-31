using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PinService;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        private readonly IPinService _pinService;
        private readonly IAutorization _autorizationService;

        #region --Public properties--       
       
        private List<PinModel> _pins;
        public List<PinModel> Pins
        {
            get => _pins;
            set
            {
                SetProperty(ref _pins, value);
            }
        }

        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value);
        }
             

        #endregion

        public MapPageViewModel(INavigationService navigationService,
                                IPinService pinService,
                                IAutorization autorizationService)
                                : base(navigationService)
        {
            _pinService = pinService;
            _autorizationService = autorizationService;
        }

        #region --OnCommand handlers--
        
        #endregion

        #region --Overrides--

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            object selectedCell;
            
            base.OnNavigatedTo(parameters);
            
            Pins = _pinService.GetPinModels(_autorizationService.GetActiveUser());

            if (parameters.TryGetValue<object>("selectedCell", out selectedCell))
            {
                SetLocation(selectedCell);
            }
        }       

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        #endregion

        #region --Private helpers
       
        private void SetLocation(object selectedCell)
        {
            var pinModel = selectedCell as PinModel;
            SelectedPin = pinModel.ToPin();
        }

        #endregion
    }
}
