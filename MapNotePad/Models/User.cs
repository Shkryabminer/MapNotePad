using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MapNotePad.Models
{
    [Table ("Users")]
    public class User : IUser
    {
        [PrimaryKey, AutoIncrement,Column("ID")]
        public  int ID { get; set ; }
        public virtual string FirstName { get; set; }
        public string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string LastName { get; set; }

        //public User()
        //{
        //    FirstName = string.Empty;
        //    LastName = string.Empty;
        //}
    }
}
