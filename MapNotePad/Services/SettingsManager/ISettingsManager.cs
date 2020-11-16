namespace MapNotePad.Services
{
    public interface ISettingsManager
    {
        string AutorizatedUserEmail { get; set; }

        int SelectedSortMethode { get; set; }

        string LanguageSource { get; set; }
        
        string FirstName { get; set; }

        string LastName { get; set; }

        double CameraLatitude { get; set; }

        double CameraLongitude { get; set; }

        double Zoom { get; set; }
    }
}
