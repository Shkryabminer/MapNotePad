using Acr.UserDialogs;
using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.Pickers;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PinService;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MapNotePad.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IPinService _pinService;
        private readonly IAutorization _autorizationService;

        public MapPageViewModel(INavigationService navigationService,
                                IPinService pinService,
                                IAutorization autorizationService,
                                IUserDialogs userDialogs)
                                : base(navigationService)
        {
            _userDialogs = userDialogs;
            _pinService = pinService;
            _autorizationService = autorizationService;
        }

        #region --Public properties--       

        private string _searchBar;
        public string SearchBar
        {
            get => _searchBar;
            set => SetProperty(ref _searchBar, value);
        }

        private List<PinModelViewModel> _pins;
        public List<PinModelViewModel> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value);
        }

        private ICommand _pinClickedCommand;
        public ICommand PinClickedCommand => _pinClickedCommand ??= new Command<Pin>(OnPinClickedCommand);

       
        #endregion


        #region --OnCommand handlers--

        private void OnPinClickedCommand(Pin pin) 
        {
            if (pin != null)
            {
                var config = SetActionSheetConfig(pin);

                _userDialogs.ActionSheet(config);
            }
        }
       

        #endregion

        #region --Overrides--

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Pins = _pinService.GetActivePins(_autorizationService.GetActiveUser()).ToList();

            if (parameters.TryGetValue("selectedCell", out PinModel selectedCell))
            {
                SetLocation(selectedCell);
            }
            else
            {
                Debug.WriteLine("Selected cell is not exist");
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SearchBar))
            {
                SortPinCollection();
            }
        }
        #endregion

        #region --Private helpers

        private void SetLocation(PinModel pinModel)
        {
            SelectedPin = pinModel.ToPin();
        }

        private ActionSheetConfig SetActionSheetConfig(Pin pin)
        {
            ActionSheetConfig config = new ActionSheetConfig
            {
                Title = pin.Label,

            }; // set all here

            config.SetUseBottomSheet(true);

            config.Add($"Latitude:  {pin.Position.Latitude}");
            config.Add($"Longitude: {pin.Position.Longitude}");

            config.SetCancel(null, null, null);

            return config;
        }

        private void SortPinCollection()
        {
            if (!string.IsNullOrEmpty(SearchBar))
            {
                var activePins = _pinService.GetActivePins(_autorizationService.GetActiveUser());

                Pins = activePins.Pick(SearchBar);
            }

            else
            {
                Pins = _pinService.GetActivePins(_autorizationService.GetActiveUser()).ToList();
            }
        }
        #endregion
    }
}
