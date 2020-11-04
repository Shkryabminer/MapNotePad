using SQLite;
using System.ComponentModel;

namespace MapNotePad.Models
{
    [Table("PinModels")]
    public class PinModel : IPinModel//, INotifyPropertyChanged
    {        
        [AutoIncrement, PrimaryKey, Column("ID")]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public int UserID { get; set; }

        public string KeyWords { get; set; }
              
        public bool IsActive { get; set; }


        public PinModel()
        {
            KeyWords = string.Empty;
        }

        public PinModel(int id) : this()
        {
            UserID = id;
        }
    }
}
