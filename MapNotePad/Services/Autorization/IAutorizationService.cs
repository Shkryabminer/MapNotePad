using MapNotePad.Models;

namespace MapNotePad.Services.Autorization
{
    public interface IAutorizationService
    {
        bool Autorizeted();

        void LogOut();

        string GetActiveUserEmail();

        void SetActiveUserEmail(User user);

    }
}
