using Prism.Unity;
using System;
using Xamarin.Forms;
using Prism.Ioc;
using Prism;
using MapNotePad.Views;
using MapNotePad.ViewModels;
using System.Threading.Tasks;
using MapNotePad.Services.Autorization;
using MapNotePad.Services.UserService;
using System.Linq;
using MapNotePad.Services;

using MapNotePad.Services.Validators;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using Acr.UserDialogs;
using MapNotePad.Services.PinService;

namespace MapNotePad
{
    public partial class App : PrismApplication
    {

        private IAutorization AutorizationService => Container.Resolve<IAutorization>();
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


            //      Servvices
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinService>());
            containerRegistry.RegisterInstance<IUserServcie>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IAuthentificationService>(Container.Resolve<AutentificationService>());
            containerRegistry.RegisterInstance<IAutorization>(Container.Resolve<AutorizationService>()); //Service
            containerRegistry.RegisterInstance<IPasswordValidator>(Container.Resolve<PasswordValidator>()); //Service
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
            if (_userService.GetUsers().Count() < 1)
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignUpPage)}");
            }
            else if (AutorizationService.Autorizeted())
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
