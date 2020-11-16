using Prism.Unity;
using Xamarin.Forms;
using Prism.Ioc;
using Prism;
using MapNotePad.Views;
using MapNotePad.ViewModels;
using System.Threading.Tasks;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.UserService;
using MapNotePad.Services;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using Acr.UserDialogs;
using MapNotePad.Services.PinService;
using MapNotePad.Services.PermissionService;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using MapNotePad.Services.FBAuthService;
using MapNotePad.Services.WeatherService;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace MapNotePad
{
    public partial class App : PrismApplication
    {

        private IAutorizationService AutorizationService => Container.Resolve<IAutorizationService>();
        private IUserServcie _userService => Container.Resolve<IUserServcie>();

        public App()
        {
            InitializeComponent();
            
        }
        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer)
        { }

        #region --Overrides--
      
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await InitNavigationAsync();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //      Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainTabbedPage>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<PinsListPage, PinsListPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPinPage, AddEditPinPageViewModel>();
          
            //      Pluggins
            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
            containerRegistry.RegisterInstance<IPermissions>(CrossPermissions.Current);
            containerRegistry.RegisterInstance<IMedia>(CrossMedia.Current);

            //      Servvices
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinService>());
            containerRegistry.RegisterInstance<IUserServcie>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IAutorizationService>(Container.Resolve<AutorizationService>());           
            containerRegistry.RegisterInstance<IPermissionService>(Container.Resolve<PermissionService>());
            containerRegistry.RegisterInstance<IFBAuthService>(Container.Resolve<FBAuthService>());
            containerRegistry.RegisterInstance<IWeatherService>(Container.Resolve<WeatherService>());
        }

        protected override void OnStart()
        { }

        protected override void OnSleep()
        { }

        protected override void OnResume()
        { }

        #endregion

        #region --Private Helpers--
        private async Task InitNavigationAsync()
        {            
            if (AutorizationService.Autorizeted())
            { 
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainTabbedPage)}"); 
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LoginPage)}");
            }           
        }

        #endregion
    }
}
