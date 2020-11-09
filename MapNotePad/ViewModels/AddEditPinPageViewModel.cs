using Acr.UserDialogs;
using MapNotePad.Models;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PermissionService;
using MapNotePad.Services.PinService;
using MapNotePad.Validators;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.ViewModels
{
    public class AddEditPinPageViewModel : BaseViewModel
    {
        private readonly IPinService _pinService;
        private readonly IAutorization _autorizationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IPermissionService _permissionService;

        public AddEditPinPageViewModel(INavigationService navigationService,
                                        IPinService pinService,
                                        IAutorization autorization,
                                        IUserDialogs userDialogs,
                                        IPermissionService permissionService) : base(navigationService)
        {
            _userDialogs = userDialogs;
            _autorizationService = autorization;
            _pinService = pinService;
            _permissionService = permissionService;
        }

        #region --Public properties--

        private PinModelViewModel currentPinModel;
        public PinModelViewModel CurrentPinModel
        {
            get => currentPinModel;
            set => SetProperty(ref currentPinModel, value);
        }

        private List<PinModelViewModel> _pins;
        public List<PinModelViewModel> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private bool _isLocationButtonEnabled;
        public bool IsLocationButtonEnabled
        {
            get => _isLocationButtonEnabled;
            set => SetProperty(ref _isLocationButtonEnabled, value);
        }

        private ICommand _savePinCommand;
        public ICommand SavePinCommand => _savePinCommand??= new Command(OnSavePinCommand);

        private ICommand _mapClickedCommand;
        public ICommand MapClickedCommand => _mapClickedCommand??= new Command<Position>(OnMapClickedCommand);

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ??= new Command(OnGoBackCommand);

        #endregion

        
        #region --OnCommandHandlers--

        private async void OnSavePinCommand()
        {
            if (CheckFields())
            {
                CurrentPinModel.UserID = _autorizationService.GetActiveUser();
                _pinService.SaveOrUpdatePin(CurrentPinModel);
                await NavigationService.GoBackAsync();
            }
        }

        private void OnMapClickedCommand(Position point)
        {
            if (point != null)
            {
                CurrentPinModel = new PinModelViewModel(new PinModel());
                CurrentPinModel.Latitude = point.Latitude;
                CurrentPinModel.Longtitude = point.Longitude;
                CreatePin();              
            }
        }

        #endregion

        #region --Overrides--
     
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);           
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var pinModelVM = parameters.GetValue<IPinModel>(nameof(PinModelViewModel)) as PinModelViewModel; //trygetvalue, nameof

            if (pinModelVM != null)
            {
                CurrentPinModel = pinModelVM;
            }
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
             await   base.InitializeAsync(parameters);

             await SetLocationButton();

        }
        #endregion

        #region --Private helpers--

        private async Task SetLocationButton()
        {
            IsLocationButtonEnabled = await _permissionService.CheckLoacationPermission();
        }

        private void CreatePin()
        {
            var pins = new List<PinModelViewModel>();
            pins.Add(CurrentPinModel);

            Pins = pins;
        }

        private bool CheckFields()
        {
            var pinValidator = new PinValidator();

            return pinValidator.PinModelIsValid(CurrentPinModel);
        }

        private async void OnGoBackCommand()
        {
            await NavigationService.GoBackAsync();
        }

        #endregion
    }
}
