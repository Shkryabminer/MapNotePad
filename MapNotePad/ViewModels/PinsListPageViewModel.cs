using Acr.UserDialogs;
using MapNotePad.Models;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PinService;
using MapNotePad.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.ViewModels
{
    public class PinsListPageViewModel: BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IAutorization _autorizationService;
        private readonly IPinService _pinService;

        #region --Public properties

        private ICommand _selectPinCommand;
        public ICommand SelectPinCommand => _selectPinCommand ?? (_selectPinCommand = new Command<object>(OnSelectPin));
                
        private ICommand _addNewPinCommand;
        public ICommand AddNewPinCommand => _addNewPinCommand ?? (_addNewPinCommand = new Command(OnAddNewModelPin));

        private ICommand _deleteProfileCommand;
        public ICommand DeleteProfileCommand => _deleteProfileCommand ?? (_deleteProfileCommand = new Command<object>(OnDeletePin));

        ICommand _logOutCommand;
        public ICommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new Command(OnLogOut));

        
        private List<PinModel> pins;
        public List<PinModel> Pins
        {
            set => SetProperty(ref pins, value);
            get => pins;
        }

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

        private void OnSelectPin(object obj)
        {
            SwapToProfilePage(obj as PinModel);
        }

        private async void OnLogOut(object obj)
        {
            _autorizationService.LogOut();
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }

        public void OnAddNewModelPin(object obj)
        {
            IPinModel prof = new PinModel(_autorizationService.GetActiveUser());
            SwapToProfilePage(prof);
        }

        private void OnDeletePin(object obj)
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
            Pins = _pinService.GetPinModels(_autorizationService.GetActiveUser()).ToList();
            RaisePropertyChanged(nameof(Pins));
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        #endregion

        #region --Private helpers

        private async void SwapToProfilePage(IPinModel pinModel)
        {
            var navParam = new NavigationParameters();
            navParam.Add("Pin", pinModel);
            await NavigationService.NavigateAsync($"{nameof(AddEditPinPage)}", navParam);
        }

        private void SetPins()
        {
            Pins = _pinService.GetPinModels(_autorizationService.GetActiveUser());
            RaisePropertyChanged($"{nameof(Pins)}");
        }

        #endregion
    }
}
