using MapNotePad.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MapNotePad.Services.FBAuthService
{
    public interface IFBAuthService
    {
        Task<FaceBookProfile> GetFBAccauntEmail(CancellationTokenSource cts);        
    }
}
