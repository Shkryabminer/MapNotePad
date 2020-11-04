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
    }
}
