﻿using Acr.UserDialogs;
using MapNotePad.Models;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.PinService;
using MapNotePad.Validators;
using Prism.Navigation;
using System.Collections.Generic;
using System.ComponentModel;
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

        #region --Public properties--

        private PinModel currentPinModel;
        public PinModel CurrentPinModel
        {
            get => currentPinModel;
            set => SetProperty(ref currentPinModel, value);
        }

        private List<PinModel> _pins;
        public List<PinModel> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }
        
        public ICommand SavePinCommand =>new Command(OnSavePinCommand);
       
        public ICommand MapClickedCommand => new Command<object>(OnMapClickedCommand);

        #endregion


        public AddEditPinPageViewModel(INavigationService navigationService,
                                        IPinService pinService,
                                        IAutorization autorization,
                                        IUserDialogs userDialogs) : base(navigationService)
        {
            _userDialogs = userDialogs;
            _autorizationService = autorization;
            _pinService = pinService;
        }

        #region --OnCommandHandlers--

        private async void OnSavePinCommand(object obj)
        {
            if (CheckFields())
            {
                CurrentPinModel.UserID = _autorizationService.GetActiveUser();
                _pinService.SaveOrUpdatePin(CurrentPinModel);
                await NavigationService.GoBackAsync();
            }
        }

        private void OnMapClickedCommand(object obj)
        {
            MapClickedEventArgs args = obj as MapClickedEventArgs;
            if (args != null)
            {
                CurrentPinModel.Latitude = args.Point.Latitude;
                CurrentPinModel.Longtitude = args.Point.Longitude;
                RaisePropertyChanged(nameof(CurrentPinModel));                
            }
        }

        #endregion

        #region --Overrides--
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(CurrentPinModel))
            {
                CreatePin();
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var pinModel = parameters.GetValue<IPinModel>("Pin") as PinModel;

            if (pinModel != null)
            {
                CurrentPinModel = pinModel;
            }
        }
        #endregion

        #region --Private helpers--
       
        private void CreatePin()
        {
            var pins = new List<PinModel>();
            pins.Add(CurrentPinModel);

            Pins = pins;
        }

        private bool CheckFields()
        {
            IPinValidator pinValidator = new PinValidator();

            return pinValidator.PinModelIsValid(CurrentPinModel);
        }
        #endregion
    }
}
