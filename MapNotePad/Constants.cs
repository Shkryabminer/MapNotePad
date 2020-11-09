using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace MapNotePad
{
    public class Constants
    {
        public const string dataBasePath = "MapNotePad.db";
        public const string _english = "en";
        public const string _russian = "ru";
        public const string _resource = "MapNotePad.Resources.Resource";
        public const string _emailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public const string _passwordPattern = @"^(.{0,7}|[^0-9]*|[^A-Z])$";
        

        public static class NavigationParameters
        {
            public const string Pin = "Pin";
        }

        public static class FacebookClient
        {
            public const string AppID = "3488832751208541";
            public const string FacebookScope = "email";
            public const string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
            public const string FacebookAccessTockenUrl = "https://www.facebook.com/connect/login_succes.html";
            public const string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email&access_token=";

            public const string FacebookIOSRedirectUrl = "https://www.facebook.com/connect/login_success.html";
            public const string FacebookAndroidRedirectUrl = "https://www.facebook.com/connect/login_success.html";

            public const string FacebookAccesTockenKey = "access_token";

        }

    }
}
