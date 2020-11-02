using SQLite;
using System.ComponentModel;

namespace MapNotePad.Models
{
    [Table("PinModels")]
    public class PinModel : IPinModel, INotifyPropertyChanged
    {        
        [AutoIncrement, PrimaryKey, Column("ID")]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public int UserID { get; set; }

        public string KeyWords { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value; 
                OnPropertyChanged(nameof(IsActive));
            }
        }

        
        public PinModel()
        {
            KeyWords = string.Empty;
        }

        public PinModel(int id) : this()
        {
            UserID = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string v = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
    }
}
