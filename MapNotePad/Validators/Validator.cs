using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MapNotePad.Validators
{
    public  class Validator
    {
        public static Validator EmailValidator { get; private set; }
        public static Validator PasswordValidator { get; private set; }

        static Validator()
        {
            EmailValidator = new Validator(Constants._emailPattern);
            PasswordValidator = new Validator(Constants._passwordPattern);
        }

        protected Validator(string pattern)
        {
            Pattern = pattern;
        }

        public  string Pattern { get; set; }

        public static bool ValidateEmail(string message, Validator validator)
        {
            return !string.IsNullOrEmpty(message) && Regex.IsMatch(message, validator.Pattern,RegexOptions.IgnoreCase);
        }

        public static bool ValidatePassword(string message, Validator validator)
        {
            return !string.IsNullOrEmpty(message)&&Regex.IsMatch(message, validator.Pattern);
        }

        public static bool ValidatePassword(string message, string pattern)
        {
            return !string.IsNullOrEmpty(message) && Regex.IsMatch(message, pattern);
        }
        
    }
}
