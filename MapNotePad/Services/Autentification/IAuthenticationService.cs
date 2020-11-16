using MapNotePad.Models;
using System.Threading.Tasks;

namespace MapNotePad.Services
{
    public interface IAuthenticationService
    {
        Task<bool> IsAutenficatedAsync(string login, string password);

        Task<IUser> GetAuthUserAsync(string login, string password);
    }
}
