using Acr.UserDialogs;
using MapNotePad.Models;
using MapNotePad.Pickers;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PinService;
using MapNotePad.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

        #region --Public properties

        private List<PinModel> pins;
        public List<PinModel> Pins
        {
            set => SetProperty(ref pins, value);
            get => pins;
        }

        private string _searchBar;
        public string SearchBar
        {
            get => _searchBar;
            set => SetProperty(ref _searchBar, value);
        }

        public ICommand EditPinModel => new Command<object>(OnEditPinModelCommand);
       
        public ICommand AddNewPinCommand => new Command(OnAddNewModelPinCommand);
        
        public ICommand DeleteProfileCommand => new Command<object>(OnDeletePinCommand);
       
        public ICommand LogOutCommand => new Command(OnLogOutCommand);

        public ICommand CellTappedCommand => new Command<object>(OnCellTappedCommand);        

        public ICommand ChangeStatusPinCommand => new Command<object>(OnChangeStatusPinCommand);

        #endregion

        public PinsListPageViewModel(INavigationService navigationService,
                                    IAutorization autorization,
                                    IPinService pinService,
                                    IUserDialogs userDialogs)
                                    : base(navigationService)
        {
            _userDialogs = userDialogs;
            _autorizationService = autorization;
            _pinService = pinService;
        }

        #region --OnCommand handlers

        private void OnEditPinModelCommand(object obj)
        {
            var pinModel = obj as PinModel;

            if (pinModel != null)
            {
                SwapToEditPinPage(pinModel);
            }
        }
        private async void OnCellTappedCommand(object obj)
        {
            var pinModel = obj as PinModel;

            if (pinModel != null)
            {
                if (pinModel.IsActive == true)
                {
                    var parametres = new NavigationParameters();
                    parametres.Add("selectedCell", pinModel);
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}?selectedTab = {nameof(MapPage)}", parametres);
                }
            }
        }

        private void OnChangeStatusPinCommand(object obj)
        {
            var pinModel = obj as PinModel;  
            
            if (pinModel != null)
            {
                pinModel.IsActive = !pinModel.IsActive;
                _pinService.SaveOrUpdatePin(pinModel);
                Console.WriteLine();
            }

            RaisePropertyChanged(nameof(Pins));
        }

        private async void OnLogOutCommand(object obj)
        {
            _autorizationService.LogOut();

            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }

        public void OnAddNewModelPinCommand(object obj)
        {
            IPinModel prof = new PinModel(_autorizationService.GetActiveUser());

            SwapToEditPinPage(prof);
        }

        private void OnDeletePinCommand(object obj)
        {
            var item = (obj as PinModel);

            if (item != null)
            {
                var config = new ConfirmConfig();
                config.Message = "Do you realy want to delete the profile";
                config.OkText = "Yes";
                config.CancelText = "No";
                config.SetAction((b) =>
                {
                    if (b)
                    {
                        _pinService.DeletePin(item);
                        SetPins();
                    }
                });
                _userDialogs.Confirm(config);
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

        #endregion

        #region --Private helpers

        private async void SwapToEditPinPage(IPinModel pinModel)
        {
            var navParam = new NavigationParameters();
            navParam.Add("Pin", pinModel);
            await NavigationService.NavigateAsync($"{nameof(AddEditPinPage)}", navParam);
        }

        private void SetPins()
        {
            var pins = _pinService.GetPinModels(_autorizationService.GetActiveUser()).ToList();
            Pins = pins;           
        }

        private void SortPinCollection()
        {
            if (!string.IsNullOrEmpty(SearchBar))
            {
                PinModelPicker picker = new PinModelPicker();

                var activePins = _pinService.GetPinModels(_autorizationService.GetActiveUser());

                Pins = picker.Pick(activePins, SearchBar);
            }
            else Pins = _pinService.GetPinModels(_autorizationService.GetActiveUser());
        }
        #endregion
    }
}
