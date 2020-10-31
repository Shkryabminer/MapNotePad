using MapNotePad.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Models
{
    [Table("PinModels")]
    public class PinModel : IPinModel
    {
        [AutoIncrement, PrimaryKey, Column("ID")]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public int UserID { get; set; }
        
        public string KeyWords { get; set; }

        public bool IsActive { get; set; }
        public PinState State { get; set; }

        public PinModel()
        { }

        public PinModel(int id)
        {
            UserID = id;
        }
    }
}
