using SQLite;

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

        public string UserEmail { get; set; }

        public string KeyWords { get; set; }=string.Empty;
              
        public bool IsActive { get; set; }

        public string Picture { get; set; }

        public PinModel()
        {                   
        }

        public PinModel(string userEmail)
        {
            UserEmail = userEmail;
        }
    }
}
