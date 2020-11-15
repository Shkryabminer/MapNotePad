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
using MapNotePad.Services.UserService;
using System.Threading;

namespace MapNotePad.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAuthentificationService _authentificationService;
        private readonly IAutorization _autorizationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IFBAuthService _fbAuthService;
        private readonly IUserServcie _userService;

        public LoginPageViewModel(INavigationService navigationService,
                                  IAutorization autorization,
                                  IAuthentificationService authentificationService,
                                  IUserDialogs userDialogs,
                                  IFBAuthService fBAuthService,
                                  IUserServcie userServcie)
                                  : base(navigationService)
        {
            _authentificationService = authentificationService;
            _autorizationService = autorization;
            _userDialogs = userDialogs;
            _fbAuthService = fBAuthService;
            _userService = userServcie;
        }

        #region --Public Properties--

        public CancellationTokenSource CTS { get; private set; }

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
            FaceBookProfile fbProfile;
            CTS = new CancellationTokenSource();

            using (_userDialogs.Loading("Press cancell to abort authorisation",()=>CTS.Cancel()))
            {
                
                fbProfile = await _fbAuthService.GetFBAccauntEmail(CTS);
            }

            if (fbProfile != null)
            {
                _autorizationService.SetActiveUserEmail(fbProfile);

                GoToMapPage(fbProfile);
            }
            else
            {
                await _userDialogs.AlertAsync("Error",null,"Ok");
            }
        }       

        private async void OnSignInButtonCommand()
        {
            var authUser = await _authentificationService.GetAuthUserAsync(Email, Password);

            if (authUser != null)
            {
              //  _userService.ActiveUser = (User)authUser;
                _autorizationService.SetActiveUserEmail((User)authUser);
                GoToMapPage(authUser);
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

            if (parameters.TryGetValue(Constants.NavigationParameters.Email, out string eMail))
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

        private async void GoToMapPage(IUser user)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(Constants.NavigationParameters.User, user);

            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}?selectedTab={nameof(MapPage)}", navigationParameters);
        }
        
        #endregion
    }

}
