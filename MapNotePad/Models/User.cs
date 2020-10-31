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
        public int ID { get; set ; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
    }
}
