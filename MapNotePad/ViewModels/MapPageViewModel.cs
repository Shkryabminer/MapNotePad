using MapNotePad.Models;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PinService;
using Prism.Navigation;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
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

        #endregion
       
        public MapPageViewModel(INavigationService navigationService,
                                IPinService pinService,
                                IAutorization autorizationService)
                                : base(navigationService)
        {
            _pinService = pinService;
            _autorizationService = autorizationService;
        }

        #region --Overrides--

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Pins = _pinService.GetPinModels(_autorizationService.GetActiveUser());
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        #endregion

        #region --Private helpers


        #endregion


    }
}
