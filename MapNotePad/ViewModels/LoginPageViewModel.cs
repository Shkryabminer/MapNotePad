using Acr.UserDialogs;
using MapNotePad.Models;
using MapNotePad.Views;
using Prism.Navigation;
using MapNotePad.Services;
using MapNotePad.Services.Autorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotePad.ViewModels
{
  public  class LoginPageViewModel:BaseViewModel
    {
        private readonly IAuthentificationService _authentificationService;
        private readonly IAutorization _autorizationService;
        private readonly IUserDialogs _userDialogs;

        #region --Public Properties--

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand SignButtonCommand => new Command(OnSignButtonCommand);

        public ICommand SignUpCommand => new Command(OnSignUpCommand);        

        #endregion

        public LoginPageViewModel(INavigationService navigationService,
                                   IAutorization autorization,
                                   IAuthentificationService authentificationService,
                                   IUserDialogs userDialogs) 
                                   : base (navigationService)
        {
            _authentificationService = authentificationService;
            _autorizationService = autorization;
            _userDialogs = userDialogs;
        }

        #region  --Oncommand handlers--
   
        private async void OnSignButtonCommand(object obj)
        {
            IUser authUser = _authentificationService.GetAuthUser(Email, Password);
            if (authUser != null)
            {
                _autorizationService.SetActiveUser(authUser.ID);
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}");
            }
            else
            {
                //string mes = _translator.GetTranslate("IncorrectLoginOrPassw");
                _userDialogs.Alert("");
                Password = "";
            }
        }

        private async void OnSignUpCommand(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }

        #endregion

        #region     --Overrides--  
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            string eMail;

            parameters.TryGetValue<string>("Email", out eMail);
            if (eMail == null)
                Email = string.Empty;
            Email = eMail;
        }

        #endregion

    }

}
