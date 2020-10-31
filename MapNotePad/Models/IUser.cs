using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Models
{
    public interface IUser : IEntity
    {        
        string Name { get; set; }
        string Password { get; set; }
        string Email { get; set; }
    }
}
