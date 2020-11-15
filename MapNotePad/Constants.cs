using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace MapNotePad
{
    public class Constants
    {
        public const string picture = "gallery.png";
        public const string dataBasePath = "MapNotePad.db";
        public const string _english = "en";
        public const string _russian = "ru";
        public const string _resource = "MapNotePad.Resources.Resource";
        public const string _emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                             @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";//from Metanit=>msdn
        public const string _passwordPattern = @"^(.{0,7}|[^0-9]*|[^A-Z])$";


        public static class NavigationParameters
        {
            public const string Pin = "Pin";
            public const string User = "User";
            public const string SelectedCell = "SelectedCell";
        }

        public static class FacebookClient
        {
            public const string AppID = "3488832751208541";
            public const string FacebookScope = "email";
            public const string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
            public const string FacebookAccessTockenUrl = "https://www.facebook.com/connect/login_succes.html";
            public const string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email,first_name,last_name&access_token=";
            public const string FacebookRedirectUrl = "https://www.facebook.com/connect/login_success.html";
            public const string FacebookAccesTockenKey = "access_token";
        }

        public static class WeatherClient
        {
            public const string Path = "https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={API key}";
            public const string Key = "1a5b4b66dae4138ec37439ed7c064e2a";
        }
    }
}
