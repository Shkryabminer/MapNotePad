using Acr.UserDialogs;
using MapNotePad.Extensions;
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

        public PinsListPageViewModel(INavigationService navigationService,
                                    IAutorization autorization,
                                    IPinService pinService,
                                    IUserDialogs userDialogs)
                                    : base(navigationService)
        {
            _userDialogs = userDialogs;
            _autorizationService = autorization;
            _pinService = pinService;

            Pins = new ObservableCollection<PinModelViewModel>();
        }

        #region --Public properties

        private ObservableCollection<PinModelViewModel> pins;
        public ObservableCollection<PinModelViewModel> Pins
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

        #region --OnCommand handlers

        private void OnEditPinModelCommand(object obj)
        {
            var pinModel = obj as PinModelViewModel;

            if (pinModel != null)
            {
                SwapToEditPinPage(pinModel);
            }
        }

        private async void OnCellTappedCommand(object obj)
        {
            var pinModel = obj as PinModelViewModel;

            if (pinModel != null)
            {
                if (pinModel.IsActive)
                {
                    var parametres = new NavigationParameters();
                    parametres.Add("selectedCell", pinModel.ToPinModel());
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}?selectedTab={nameof(MapPage)}", parametres);
                }
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

        private async void OnLogOutCommand(object obj)
        {
            _autorizationService.LogOut();

            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }

        public void OnAddNewModelPinCommand(object obj)
        {
            var prof = new PinModel(_autorizationService.GetActiveUser());

            SwapToEditPinPage(prof.ToViewModel());
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

        #endregion

        #region --Private helpers

        private async void SwapToEditPinPage(PinModelViewModel pinModel)
        {
            var navParam = new NavigationParameters
            {
                { nameof(PinModelViewModel), pinModel }
            };

            await NavigationService.NavigateAsync($"{nameof(AddEditPinPage)}", navParam);
        }

        private void SetPins()
        {
            var pins = _pinService.GetPinModels(_autorizationService.GetActiveUser());
           // var pinViewModels = pins.Select(pin => pin.ToViewModel());

            Pins = new ObservableCollection<PinModelViewModel>(pins);
        }

        private void SortPinCollection()
        {
            if (!string.IsNullOrEmpty(SearchBar))
            {
                var activePins = _pinService.GetPinModels(_autorizationService.GetActiveUser());

                var sortedPins = activePins.Pick(SearchBar);

                Pins =new ObservableCollection<PinModelViewModel>(sortedPins); 
            }
            else
            {
                var sortedPins = _pinService.GetPinModels(_autorizationService.GetActiveUser());

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
