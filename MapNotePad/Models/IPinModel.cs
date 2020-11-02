namespace MapNotePad.Models
{
    public interface IPinModel:IEntity
    {       
        string Name { get; set; }
        double Latitude { get; set; }
        double Longtitude { get; set; }
        string KeyWords { get; set; }
        int UserID { get; set; }
        bool IsActive { get; set; }
                        
    }
}
