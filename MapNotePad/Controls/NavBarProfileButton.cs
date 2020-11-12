using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MapNotePad.Controls
{
    public class NavBarProfileButton : Button
    {
        public NavBarProfileButton()
        {
            Text = "YP";
        }

        #region --Public properties--

        public static BindableProperty FirstNameProperty = BindableProperty.
                                                                Create(nameof(FirstName),
                                                                       typeof(string),
                                                                       typeof(NavBarProfileButton));

        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        public static BindableProperty LastNameProperty = BindableProperty.
                                                        Create(nameof(LastName),
                                                               typeof(string),
                                                               typeof(NavBarProfileButton));

        public string LastName
        {
            get => (string)GetValue(LastNameProperty);
            set => SetValue(LastNameProperty, value);
        }

        #endregion

        #region --Overrides--
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(FirstName))
            {
                if (!string.IsNullOrEmpty(FirstName))
                {                    
                    SetNames(FirstName);
                }
                else
                {
                }

            }
            if (propertyName == nameof(LastName))
            {
                if (!string.IsNullOrEmpty(LastName))
                {
                    SetNames(LastName);
                }
                else
                {
                }
            }
        }

        #endregion

        #region --Private helpers--

        private void SetNames(string value)
        {
            if (Text.Length == 2)
            {
                Text = value.First<Char>().ToString().ToUpper();
            }
            else
            { 
            Text += value.First<Char>().ToString().ToUpper();
            }
           
        }
        #endregion
    }
}
