using Acr.UserDialogs;
using MapNotePad.Models;
using MapNotePad.Services.UserService;
using MapNotePad.Views;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotePad.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        private readonly IUserServcie _userService;
        private readonly IUserDialogs _userDialogs;

        #region --Public properties--

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

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
        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get => _passwordConfirm;
            set => SetProperty(ref _passwordConfirm, value);
        }

        public ICommand SignUpCommand => new Command(OnSignUpCommand);

        #endregion
        public SignUpPageViewModel(INavigationService navigationService,
                                   IUserServcie userServcie,
                                   IUserDialogs userDialogs)
                                   : base(navigationService)
        {
            _userService = userServcie;
            _userDialogs = userDialogs;
        }

        #region --ONcommand handlers

        private async void OnSignUpCommand(object obj)
        {
            User user = new User()
            {
                Name = this.Name,
                Password = this.Password,
                Email = this.Email
            };

            _userService.AddOrUpdate(user);

            var navParam = new NavigationParameters();
            navParam.Add("Email", Email);
            await NavigationService.NavigateAsync($"/{nameof(LoginPage)}", navParam);
        }

        #endregion


    }
}
