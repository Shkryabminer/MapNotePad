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

        #region --Public properties--       

        private string _searchBar;
        public string SearchBar
        {
            get => _searchBar;
            set => SetProperty(ref _searchBar, value);
        }

        private List<PinModel> _pins;
        public List<PinModel> Pins
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

        public ICommand PinClickedCommand => new Command<object>(OnPinClickedCommand);

        public ICommand TextChangedCommand => new Command<object>(OnTextChangedCommand);

        #endregion

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

        #region --OnCommand handlers--

        private void OnPinClickedCommand(object obj)
        {
            PinClickedEventArgs args = obj as PinClickedEventArgs;

            if (args != null)
            {
                Pin pin = args.Pin;

                var config = SetActionSheetConfig(pin);

                _userDialogs.ActionSheet(config);
            }
        }

        private void OnTextChangedCommand(object obj)
        {
            TextChangedEventArgs args = obj as TextChangedEventArgs;
            if (args != null)
            {
                string newText = args.NewTextValue;
            }
        }


        #endregion

        #region --Overrides--

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            object selectedCell;

            Pins = _pinService.GetActivePins(_autorizationService.GetActiveUser());

            if (parameters.TryGetValue<object>("selectedCell", out selectedCell))
            {
                SetLocation(selectedCell);
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
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

        private void SetLocation(object selectedCell)
        {
            var pinModel = selectedCell as PinModel;
            SelectedPin = pinModel.ToPin();
        }

        private ActionSheetConfig SetActionSheetConfig(Pin pin)
        {
            ActionSheetConfig config = new ActionSheetConfig();

            config.SetUseBottomSheet(true);

            config.Title = pin.Label;

            config.Add($"Latitude:  {pin.Position.Latitude}");
            config.Add($"Longitude: {pin.Position.Longitude}");

            config.SetCancel(null, null, null);

            return config;
        }

        private void SortPinCollection()
        {
            if (!string.IsNullOrEmpty(SearchBar))
            {
                PinModelPicker picker = new PinModelPicker();

                var activePins = _pinService.GetActivePins(_autorizationService.GetActiveUser());

                Pins = picker.Pick(activePins, SearchBar);
            }
            else Pins = _pinService.GetActivePins(_autorizationService.GetActiveUser());
        }
        #endregion
    }
}
