using Acr.UserDialogs;
using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.Pickers;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PermissionService;
using MapNotePad.Services.PinService;
using MapNotePad.ViewModels.Interfaces;
using Plugin.Permissions.Abstractions;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IPermissionService _permissionService;

        public MapPageViewModel(INavigationService navigationService,
                                IPinService pinService,
                                IAutorization autorizationService,
                                IUserDialogs userDialogs,
                                IPermissionService permissionService)
                                : base(navigationService)
        {
            _permissionService = permissionService;
            _userDialogs = userDialogs;
            _pinService = pinService;
            _autorizationService = autorizationService;
        }

        #region --Public properties--       

        private bool _isLocationButtonEnabled;

        public bool IsLocationButton
        {
            get => _isLocationButtonEnabled;
            set => SetProperty(ref _isLocationButtonEnabled, value);
        }

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

        private CameraPosition _lastCameraPosition;
        public CameraPosition LastCameraPosition
        {
            get => _lastCameraPosition;
            set => SetProperty(ref _lastCameraPosition, value);
        }

        private CameraPosition _startCameraPosition;
        public CameraPosition StartCameraPosition
        {
            get => _startCameraPosition;
            set => SetProperty(ref _startCameraPosition, value);
        }


        private ICommand _cameraChangedCommand;
        public ICommand CameraChangedCommand => _cameraChangedCommand ??= new Command<CameraPosition>(OnCameraChangedCommand);

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
        private void OnCameraChangedCommand(CameraPosition obj)
        {
            LastCameraPosition = obj;
        }


        #endregion

        #region --Overrides--     


        public override void SaveCameraPosition()
        {
            base.SaveCameraPosition();
            if (LastCameraPosition != null)
            {
                _pinService.SaveCameraPosotion(LastCameraPosition);
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            StartCameraPosition = _pinService.LoadCameraPosition();
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            await base.InitializeAsync(parameters);

            await SetLocationButton();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Pins = _pinService.GetActivePins(_autorizationService.GetActiveUser()).ToList();

            if (parameters.TryGetValue("selectedCell", out PinModelViewModel selectedCell))
            {
                SetLocationToMap(selectedCell);
            }
            else
            {
                Debug.WriteLine("Selected cell is not exist");
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

        private void SetLocationToMap(PinModelViewModel pinModel)
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
        private async Task SetLocationButton()
        {
            IsLocationButton = await _permissionService.CheckLoacationPermission();
        }


        #endregion
    }
}
