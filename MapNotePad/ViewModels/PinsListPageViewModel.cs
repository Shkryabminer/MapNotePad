using Acr.UserDialogs;
using MapNotePad.Extensions;
using MapNotePad.Models;
using MapNotePad.Pickers;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PinService;
using MapNotePad.Services.UserService;
using MapNotePad.Views;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

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

            FirstName = _userServcie.GetFirstName();
            LastName = _userServcie.GetLastName();
        }

        #region --Public properties

        private ObservableCollection<PinModelViewModel> pins;
        public ObservableCollection<PinModelViewModel> Pins
        {
            get => pins;
            set => SetProperty(ref pins, value);
        }

        private string firstNmae;
        public string FirstName
        {
            get => firstNmae;
            set => SetProperty(ref firstNmae, value);
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
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

        public ICommand EditPinModel => new Command<object>(OnEditPinModelCommand);

        public ICommand AddNewPinCommand => new Command(OnAddNewModelPinCommand);

        public ICommand DeleteProfileCommand => new Command<object>(OnDeletePinCommand);

        public ICommand LogOutCommand => new Command(OnLogOutCommand);
        public ICommand _listFocusedCommand;
        public ICommand ListFocusedCommand
        {
            get => _listFocusedCommand ??= new Command(OnListFocusedCommand);
        }


        private ICommand _showMenuCommand;
        public ICommand ShowMenuCommand => _showMenuCommand ??= new Command(OnShowMenuCommand);

        public ICommand CellTappedCommand => new Command<object>(OnCellTappedCommand);

        public ICommand ChangeStatusPinCommand => new Command<object>(OnChangeStatusPinCommand);

        #endregion             

        #region --OnCommand handlers

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

        private async void OnCellTappedCommand(object obj)
        {
            if (!IsActiveFrame)
            {
                var pinModel = obj as PinModelViewModel;

                if (pinModel != null)
                {
                    if (pinModel.IsActive)
                    {
                        var parametres = new NavigationParameters();
                        parametres.Add("selectedCell", pinModel);
                        var result = await NavigationService.SelectTabAsync(nameof(MapPage), parametres);
                    }
                    else
                    {
                        Debug.WriteLine("No active pins");
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

        private void OnChangeStatusPinCommand(object obj)
        {
            var pinModelVM = obj as PinModelViewModel;

            if (pinModelVM != null)
            {
                pinModelVM.IsActive = !pinModelVM.IsActive;
                _pinService.SaveOrUpdatePin(pinModelVM);
                Console.WriteLine();
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

                }; // set here

                config.Message = "Do you realy want to delete the profile";
                config.OkText = "Yes";
                config.CancelText = "No";

                var delete = await _userDialogs.ConfirmAsync(config);

                if (delete)
                {
                    _pinService.DeletePin(item);
                    SetPins();
                }
                else
                {
                    Debug.WriteLine("Delete confirm canceled");
                }
            }
        }

        #endregion

        #region --Overrides--

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            SetPins();
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

        private void SetPins()
        {
            var pins = _pinService.GetPinModels(_autorizationService.GetActiveUserEmail());
            // var pinViewModels = pins.Select(pin => pin.ToViewModel());

            Pins = new ObservableCollection<PinModelViewModel>(pins);
        }

        private void SortPinCollection()
        {
            if (!string.IsNullOrEmpty(SearchBar))
            {
                var activePins = _pinService.GetPinModels(_autorizationService.GetActiveUserEmail());

                var sortedPins = activePins.Pick(SearchBar);

                Pins = new ObservableCollection<PinModelViewModel>(sortedPins);
            }
            else
            {
                var sortedPins = _pinService.GetPinModels(_autorizationService.GetActiveUserEmail());

                Pins = new ObservableCollection<PinModelViewModel>(sortedPins);
            }
        }

        //private List<PinModelViewModel> ConvertModels(IEnumerable<PinModel> pins)
        //{
        //    List<PinModelViewModel> list = new List<PinModelViewModel>();

        //    foreach (PinModel p in pins)
        //    {
        //        list.Add(p.ToViewModel());
        //    }
        //    return list;
        //}
        #endregion
    }
}
