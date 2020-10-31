﻿using MapNotePad.Views;
using Prism.Navigation;
using MapNotePad.Services.Autorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotePad.ViewModels
{
    public class MapTabbedPageViewModel : BaseViewModel
    {
        private readonly IAutorization autorizationService;
       
        #region    --Public properties--

        public ICommand LogOutCommand => new Command(OnLogOutCommand);       

        #endregion

        public MapTabbedPageViewModel(INavigationService navigationService, 
                                      IAutorization autorization)
                                      : base(navigationService)
        {
            autorizationService = autorization;
        }

        #region    --OnCommand handlers--
     
        private async void OnLogOutCommand(object obj)
        {
            autorizationService.LogOut();
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }
        #endregion
    }
}
