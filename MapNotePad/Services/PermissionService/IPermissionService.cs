using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services.PermissionService
{
   public interface IPermissionService
    {
        Task<PermissionStatus> GetPermissionStatus<T>() where T : BasePermission, new();

        Task<bool> CheckPermission<T>() where T : BasePermission, new();
        
    }
}
