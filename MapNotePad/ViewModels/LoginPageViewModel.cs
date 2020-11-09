using Acr.UserDialogs;
using MapNotePad.Views;
using Prism.Navigation;
using MapNotePad.Services;
using MapNotePad.Services.Autorization;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Xamarin.Auth;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Json;
using Newtonsoft.Json;
using MapNotePad.Models;
using Device = Xamarin.Forms.Device;
using MapNotePad.Services.FBAuthService;

namespace MapNotePad.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAuthentificationService _authentificationService;
        private readonly IAutorization _autorizationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IFBAuthService _fbAuthService;

        public LoginPageViewModel(INavigationService navigationService,
                                  IAutorization autorization,
                                  IAuthentificationService authentificationService,
                                  IUserDialogs userDialogs,
                                  IFBAuthService fBAuthService)
                                  : base(navigationService)
        {
            _authentificationService = authentificationService;
            _autorizationService = autorization;
            _userDialogs = userDialogs;
            _fbAuthService = fBAuthService;
        }

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

        private ICommand _signButtonCommand;
        public ICommand SignButtonCommand => _signButtonCommand ??= new Command(OnSignInButtonCommand);

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= _signUpCommand = new Command(OnSignUpCommand);

        private ICommand _fbLoginCommand;
        public ICommand FBLoginCommand => _fbLoginCommand ??= new Command(OnFBLoginCommand);


        #endregion


        #region  --Oncommand handlers--

        private async void OnFBLoginCommand(object obj)
        {
            var email = await _fbAuthService.GetFBAccauntEmail();

            Debug.WriteLine(email);
        }

       

        private async void OnSignInButtonCommand()
        {
            var authUser = _authentificationService.GetAuthUser(Email, Password);

            if (authUser != null)
            {
                _autorizationService.SetActiveUser(authUser.ID);
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}");
            }
            else
            {
                //string mes = _translator.GetTranslate("IncorrectLoginOrPassw");
                _userDialogs.Alert("Invalid login or password");
                Password = "";
            }
        }

        private async void OnSignUpCommand()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }

        #endregion

        #region     --Overrides--  
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue("Email", out string eMail))
            {
                Email = eMail;
            }
            else
            {
                Email = string.Empty;
            }
        }

        #endregion

        #region --Private handlers--

        
        #endregion
    }

}
