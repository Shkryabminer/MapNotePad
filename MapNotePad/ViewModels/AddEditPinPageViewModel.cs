using Acr.UserDialogs;
using MapNotePad.Models;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PermissionService;
using MapNotePad.Services.PinService;
using MapNotePad.Validators;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private readonly IMedia _mediaPlugin;

        public AddEditPinPageViewModel(INavigationService navigationService,
                                        IPinService pinService,
                                        IAutorization autorization,
                                        IUserDialogs userDialogs,
                                        IPermissionService permissionService,
                                        IMedia mediaPlugin) : base(navigationService)
        {
            _userDialogs = userDialogs;
            _autorizationService = autorization;
            _pinService = pinService;
            _permissionService = permissionService;
            _mediaPlugin = mediaPlugin;
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

        private bool _pictureMenuIsActive;
        public bool PictureMenuIsActive
        {
            get => _pictureMenuIsActive;
            set => SetProperty(ref _pictureMenuIsActive, value);
        }

        private ICommand _urlPictureCommand;
        public ICommand UrlPictureCommand => _urlPictureCommand ??= new Command(OnUrlPictureCommand);               

        private ICommand _disableMenuCommand;
        public ICommand DisableMenuCommand => _disableMenuCommand ??= new Command(OnDisableMenuCommand);

        private ICommand _savePinCommand;
        public ICommand SavePinCommand => _savePinCommand ??= new Command(OnSavePinCommand);

        private ICommand _mapClickedCommand;
        public ICommand MapClickedCommand => _mapClickedCommand ??= new Command<Position>(OnMapClickedCommand);

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ??= new Command(OnGoBackCommand);

        private ICommand _cameraPictureCommand;
        public ICommand CameraPictureCommand => _cameraPictureCommand ??= new Command(OnCameraPictureCommand);

        private ICommand _storagePictureCommand;
        public ICommand StoragePictureCommand => _storagePictureCommand ??= new Command(OnStoragePictureCommand);

        private ICommand _showImageCommand;
        public ICommand ShowImageMenu => _showImageCommand ??= new Command(() => PictureMenuIsActive = !PictureMenuIsActive);

        #endregion


        #region --OnCommandHandlers--

        private void OnDisableMenuCommand()
        {
            if (PictureMenuIsActive)
            {
                PictureMenuIsActive = false;
            }
            else
            {
                //other branch
            }
        }
        private async void OnSavePinCommand()
        {
            if (CheckFields())
            {
                CurrentPinModel.UserEmail = _autorizationService.GetActiveUserEmail();
                await _pinService.SaveOrUpdatePinAsync(CurrentPinModel);
                await NavigationService.GoBackAsync();
            }
        }

        private void OnMapClickedCommand(Position point)
        {
            if (point != null)
            {
                if (CurrentPinModel == null)
                {
                    CurrentPinModel = new PinModelViewModel(new PinModel());
                }
                else
                {
                    //alternative brunch
                }

                CurrentPinModel.Latitude = point.Latitude;
                CurrentPinModel.Longtitude = point.Longitude;

                CreatePin();
            }
            else
            {
                // alternative branch
            }
        }

        private async void OnCameraPictureCommand()
        {
            var cameraAllowed = await _permissionService.CheckPermission<CameraPermission>();

            if (cameraAllowed)
            {
                if (_mediaPlugin.IsTakePhotoSupported && _mediaPlugin.IsCameraAvailable)
                {
                    var options = new StoreCameraMediaOptions();
                    options.SaveToAlbum = true;
                    options.PhotoSize = PhotoSize.Custom;
                    options.CustomPhotoSize = 600;

                    MediaFile file = await _mediaPlugin.TakePhotoAsync(options);

                    if (file != null)
                    {
                        CurrentPinModel.Picture = file.Path;
                    }
                }
            }
            else
            {
                await _userDialogs.AlertAsync("You need allow to use camera on your device",okText:"Ok");
            }
        }

        private async void OnStoragePictureCommand()
        {
            if (_mediaPlugin.IsPickPhotoSupported)
            {
                MediaFile file = await _mediaPlugin.PickPhotoAsync();

                if (file != null)
                {
                    CurrentPinModel.Picture = file.Path;
                }
                else
                {
                    Debug.WriteLine("CurrentPinModel == null");
                }
            }
            else
            {
                Debug.WriteLine("Pick photo is not supported");
            }
        }
        private async void OnUrlPictureCommand()
        {
            var result = await _userDialogs.PromptAsync("Enter your URL", "Picture URL", "Ok", "Cancell",placeholder:"Url", inputType: InputType.Url);
            CurrentPinModel.Picture = result.Text;
        }

        private async void OnGoBackCommand()
        {
            await NavigationService.GoBackAsync();
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

            var pinModelVM = parameters.GetValue<PinModelViewModel>(nameof(PinModelViewModel)) as PinModelViewModel; //trygetvalue, nameof

            Debug.WriteLine("");
            if (pinModelVM != null)
            {
                CurrentPinModel = pinModelVM;
            }
            else
            {
                CurrentPinModel = new PinModelViewModel(new PinModel());
            }
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            await base.InitializeAsync(parameters);

            await SetLocationButton();

        }
        #endregion

        #region --Private helpers--

        private async Task SetLocationButton()
        {
            IsLocationButtonEnabled = await _permissionService.CheckPermission<LocationPermission>();
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

        private async void SetPictureFromGalery()
        {
            if (_mediaPlugin.IsPickPhotoSupported)
            {
                MediaFile file = await _mediaPlugin.PickPhotoAsync();
                CurrentPinModel.Picture = file.Path;
               // RaisePropertyChanged(nameof(CurrentPinModel));
            }
        }

        #endregion
    }
}
