using Acr.UserDialogs;
using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.Pickers;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PermissionService;
using MapNotePad.Services.PinService;
using MapNotePad.Services.WeatherService;
using Plugin.Permissions;
using Prism.Navigation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IPinService _pinService;
        private readonly IAutorization _autorizationService;
        private readonly IPermissionService _permissionService;
        private readonly IWeatherService _weatherService;

        public MapPageViewModel(INavigationService navigationService,
                                IPinService pinService,
                                IAutorization autorizationService,
                                IUserDialogs userDialogs,
                                IPermissionService permissionService,
                                IWeatherService weatherService)
                                : base(navigationService)
        {
            _permissionService = permissionService;
            _userDialogs = userDialogs;
            _pinService = pinService;
            _autorizationService = autorizationService;
            _weatherService = weatherService;
        }

        #region --Public properties--       

        private bool _isLocationButtonEnabled;

        public bool IsLocationButton
        {
            get => _isLocationButtonEnabled;
            set => SetProperty(ref _isLocationButtonEnabled, value);
        }

        private string _searchBar;
        public string SearchEntry
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

        private Pin _onMpTappedPin;
        public Pin OnMapTappedPin
        {
            get => _onMpTappedPin;
            set => SetProperty(ref _onMpTappedPin, value);
        }

        private WeatherModel _pinTappedWeather;
        public WeatherModel PinTappedWeather
        {
            get => _pinTappedWeather;
            set => SetProperty(ref _pinTappedWeather, value);
        }

        private string _mapTappedPinPicture;
        public string MapTappedPinPicture
        {
            get => _mapTappedPinPicture ??= Constants.picture;
            set => SetProperty(ref _mapTappedPinPicture, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private bool _infoIsVisible;
        public bool InfoIsVisible
        {
            get => _infoIsVisible;
            set => SetProperty(ref _infoIsVisible, value);
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

        private User activeUSer;
        public User ActiveUser
        {
            get => activeUSer;
            set => SetProperty(ref activeUSer, value);
        }

        private ICommand _cameraChangedCommand;
        public ICommand CameraChangedCommand => _cameraChangedCommand ??= new Command<CameraPosition>(OnCameraChangedCommand);

        private ICommand _pinClickedCommand;
        public ICommand PinClickedCommand => _pinClickedCommand ??= new Command<Pin>(OnPinClickedCommand);

        private ICommand _mapClickedCommand;
        public ICommand MapClickedCommand => _mapClickedCommand ??= new Command(OnMapClickedCommand);

        #endregion


        #region --OnCommand handlers--

        private async void OnPinClickedCommand(Pin pin)
        {
            if (pin != null)
            {
                PinTappedWeather = await _weatherService.GetWeatherData(pin.Position.Latitude, pin.Position.Longitude);

                SetPinInfo(pin);

                InfoIsVisible = true;
            }
            else 
            {
                Debug.WriteLine("Pin is null");
            }
        }

        private void OnCameraChangedCommand(CameraPosition obj)
        {
            LastCameraPosition = obj;
        }

        private void OnMapClickedCommand()
        {
            if (InfoIsVisible)
            {
                InfoIsVisible = false;
            }
            else
            { 
            //Other way
            }
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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Pins = (await _pinService.GetActivePinsAsync()).ToList();

            if (parameters.TryGetValue(Constants.NavigationParameters.SelectedCell, out PinModelViewModel selectedCell))
            {
                SetLocationToMap(selectedCell);
            }
            else if (parameters.TryGetValue(Constants.NavigationParameters.User, out User activeUser))
            {
                ActiveUser = activeUser;
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

        protected override async void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SearchEntry))
            {
                await SortPinCollection();
            }
        }


        #endregion

        #region --Private helpers

        private void SetLocationToMap(PinModelViewModel pinModel)
        {         

            if (LastCameraPosition == null)
            {
                LastCameraPosition = _pinService.LoadCameraPosition();
            }
            else
            {
                //
            }

            StartCameraPosition = new CameraPosition(new Position(pinModel.Latitude,
                                                                   pinModel.Longtitude),
                                                                  LastCameraPosition.Zoom);

            SelectedPin = pinModel.ToPin();
        }

        private void SetPinInfo(Pin pin)
        {
            OnMapTappedPin = pin;

            string[] strings = (string[])pin.Tag;

            MapTappedPinPicture = strings[1];

            Description = strings[0];          

        }

        private async Task SortPinCollection()
        {
            if (!string.IsNullOrEmpty(SearchEntry))
            {
                var activePins = await _pinService.GetActivePinsAsync();

                Pins = activePins.Pick(SearchEntry);
            }

            else
            {
                Pins = (await _pinService.GetActivePinsAsync()).ToList();
            }
        }

        private async Task SetLocationButton()
        {
            IsLocationButton = await _permissionService.CheckPermission<LocationPermission>();
        }


        #endregion
    }
}
