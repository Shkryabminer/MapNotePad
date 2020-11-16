using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;

namespace MapNotePad.Services.PermissionService
{
    public interface IPermissionService
    {
        Task<PermissionStatus> GetPermissionStatus<T>() where T : BasePermission, new();

        Task<bool> CheckPermission<T>() where T : BasePermission, new();        
    }
}
