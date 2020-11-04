using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MapNotePad.Validators
{
    public  class Validator
    {
        //public static Validator Email{ get; private set; }
        //public static Validator Password { get; private set; }

        //static Validator()
        //{
        //    Email = new Validator(Constants._email_pattern);
        //}
        //protected Validator(string pattern)
        //{
        //    Pattern = pattern;
        //}

        public  string Pattern { get; set; }

        public static bool Validate(string message, Validator validator)
        {
            return !string.IsNullOrEmpty(message)&&Regex.IsMatch(message, validator.Pattern);
        }

        public static bool Validate(string message, string pattern)
        {
            return !string.IsNullOrEmpty(message) && Regex.IsMatch(message, pattern);
        }
        
    }
}
