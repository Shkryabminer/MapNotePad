using Acr.UserDialogs;
using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.Pickers;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PinService;
using MapNotePad.Services.UserService;
using MapNotePad.Views;
using Prism.Common;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotePad.ViewModels
{
    public class PinsListPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IAutorization _autorizationService;
        private readonly IPinService _pinService;
        private readonly IUserServcie _userServcie;

        public PinsListPageViewModel(INavigationService navigationService,
                                    IAutorization autorization,
                                    IPinService pinService,
                                    IUserDialogs userDialogs,
                                    IUserServcie userServcie)
                                    : base(navigationService)
        {
            _userDialogs = userDialogs;
            _autorizationService = autorization;
            _pinService = pinService;
            _userServcie = userServcie;

            Pins = new ObservableCollection<PinModelViewModel>();

            FirstName = _userServcie.GetFirstName;
            LastName = _userServcie.GetLastName;
        }

        #region --Public properties

        private ObservableCollection<PinModelViewModel> _pins;
        public ObservableCollection<PinModelViewModel> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private string _firstNmae;
        public string FirstName
        {
            get => _firstNmae;
            set => SetProperty(ref _firstNmae, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _searchBar;
        public string SearchBar
        {
            get => _searchBar;
            set => SetProperty(ref _searchBar, value);
        }
        private bool _isActiveFrame;
        public bool IsActiveFrame
        {
            get => _isActiveFrame;
            set => SetProperty(ref _isActiveFrame, value);
        }
        private ICommand _disableMenuCommand;
        public ICommand DisableMenuCommand => _disableMenuCommand ??= new Command(OnDisableMenuCommand);

        public ICommand _editPinModelCommand;
        public ICommand EditPinModelCommand => _editPinModelCommand ??= new Command<object>(OnEditPinModelCommand);

        public ICommand _addNewPinCommand;
        public ICommand AddNewPinCommand => _addNewPinCommand ??= new Command(OnAddNewModelPinCommand);

        private ICommand _deletePinCommand;
        public ICommand DeletePinCommand => _deletePinCommand ??= new Command<object>(OnDeletePinCommand);

        private ICommand _logOutCommand;
        public ICommand LogOutCommand => _logOutCommand ??= new Command(OnLogOutCommand);

        public ICommand _listFocusedCommand;
        public ICommand ListFocusedCommand => _listFocusedCommand ??= new Command(OnListFocusedCommand);

        private ICommand _showMenuCommand;
        public ICommand ShowMenuCommand => _showMenuCommand ??= new Command(OnShowMenuCommand);

        public ICommand CellTappedCommand => new Command<PinModelViewModel>(OnCellTappedCommand);

        private ICommand _changeStatusPinCommand;
        public ICommand ChangeStatusPinCommand => _changeStatusPinCommand ??= new Command<PinModelViewModel>(OnChangeStatusPinCommand);

        #endregion

        #region --OnCommand handlers

        private void OnDisableMenuCommand()
        {
            IsActiveFrame = false;
        }

        private void OnEditPinModelCommand(object obj)
        {
            var pinModel = obj as PinModelViewModel;

            if (pinModel != null)
            {
                GoToEditPinPage(pinModel);
            }
        }

        private void OnListFocusedCommand(object obj)
        {
            if (IsActiveFrame)
            {
                IsActiveFrame = false;
            }
            else
            {
                Debug.WriteLine("IsActive id false");
            }
        }

        private async void OnCellTappedCommand(PinModelViewModel pinVM)
        {
            if (!IsActiveFrame)
            {
                if (pinVM != null)
                {
                    if (pinVM.IsActive)
                    {
                        var parametres = new NavigationParameters
                        {
                            { Constants.NavigationParameters.SelectedCell, pinVM}
                        };

                        #region -- Dirty little secret from SV --

                        var currentPage = (NavigationService as IPageAware).Page;

                        if (currentPage is TabbedPage tp)
                        {
                            currentPage = tp.CurrentPage;
                        }

                        #endregion

                        var result = await NavigationService.SelectTabAsync(nameof(MapPage), parametres);
                    }
                    else
                    {
                        Debug.WriteLine("No active _pins");
                    }
                }
                else
                {
                    Debug.WriteLine("no pinmodel instance");
                }
            }
            else
            {
                IsActiveFrame = false;
            }
        }

        private async void OnChangeStatusPinCommand(PinModelViewModel obj)
        {
            if (obj != null)
            {
                obj.IsActive = !obj.IsActive;

                await _pinService.SaveOrUpdatePinAsync(obj);
            }
        }

        private void OnShowMenuCommand(object obj)
        {
            IsActiveFrame = true;
        }

        private async void OnLogOutCommand(object obj)
        {
            _autorizationService.LogOut();

            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }

        public void OnAddNewModelPinCommand(object obj)
        {
            var prof = new PinModel(_autorizationService.GetActiveUserEmail());

            GoToEditPinPage(prof.ToViewModel());
        }

        private async void OnDeletePinCommand(object obj)
        {
            var item = (obj as PinModelViewModel);

            if (item != null)
            {
                var config = new ConfirmConfig
                {
                    Message = "Do you realy want to delete the profile",
                    OkText = "Yes",
                    CancelText = "No"
                };

                var delete = await _userDialogs.ConfirmAsync(config);

                if (delete)
                {
                    await _pinService.DeletePinAsync(item);
                    await SetPins();
                }
                else
                {
                    Debug.WriteLine("Delete confirm canceled");
                }
            }
        }

        #endregion

        #region --Overrides--

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await SetPins();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SearchBar))
            {
                await SortPinCollection();
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        #endregion

        #region --Private helpers

        private async void GoToEditPinPage(PinModelViewModel pinModel)
        {
            var navParam = new NavigationParameters
            {
                { nameof(PinModelViewModel), pinModel }
            };

            await NavigationService.NavigateAsync($"{nameof(AddEditPinPage)}", navParam);
        }

        private async Task SetPins()
        {
            var pins = await _pinService.GetAllPinsAsync();

            Pins = new ObservableCollection<PinModelViewModel>(pins);
        }

        private async Task SortPinCollection()
        {
            if (!string.IsNullOrEmpty(SearchBar))
            {
                var activePins = await _pinService.GetAllPinsAsync();

                var sortedPins = activePins.Pick(SearchBar);

                Pins = new ObservableCollection<PinModelViewModel>(sortedPins);
            }
            else
            {
                var sortedPins = await _pinService.GetAllPinsAsync();

                Pins = new ObservableCollection<PinModelViewModel>(sortedPins);
            }
        }

        #endregion
    }
}
