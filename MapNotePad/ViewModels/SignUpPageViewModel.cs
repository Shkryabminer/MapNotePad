using Acr.UserDialogs;
using MapNotePad.Models;
using MapNotePad.Services.UserService;
using MapNotePad.Validators;
using MapNotePad.Views;
using Prism.Navigation;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotePad.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        private readonly IUserServcie _userService;
        private readonly IUserDialogs _userDialogs;

        public SignUpPageViewModel(INavigationService navigationService,
                                   IUserServcie userServcie,
                                   IUserDialogs userDialogs)
                                   : base(navigationService)
        {
            _userService = userServcie;
            _userDialogs = userDialogs;
        }

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

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= new Command(OnGoBackCommand);        

        private ICommand _signUpCommand;
        public ICommand SignUpCommand =>_signUpCommand??= new Command(OnSignUpCommand);

        #endregion

        #region -- ONcommand handlers --

        private async void OnGoBackCommand(object obj)
        {
            await NavigationService.GoBackAsync();
        }

        private async void OnSignUpCommand()
        {
            User user = new User
            {
                Name = this.Name,
                Password = this.Password,
                Email = this.Email
            };

            if (ValidatedUser())
            {
                _userService.AddOrUpdate(user);

                var navParam = new NavigationParameters
                 {
                     { nameof(Email), _email }
                 };

                await NavigationService.NavigateAsync($"/{nameof(LoginPage)}", navParam);
            }
            else
            {
                await _userDialogs.AlertAsync("Invalid email or Password error");
            }
        }

        #endregion

        #region --Private helpers--

        private bool ValidatedUser()
        {
            return Validator.Validate(Email, Constants._emailPattern)&&
                   Validator.Validate(Password, Constants._passwordPattern)&&
                   Password==PasswordConfirm;
        }

        #endregion
    }
}
