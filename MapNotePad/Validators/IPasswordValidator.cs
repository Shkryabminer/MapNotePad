using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace MapNotePad.Services.Validators
{
    public interface IPasswordValidator
    {
        string IsValid(string login, string password, string confirm, IEnumerable<User> users);
    }
}
