using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services.PermissionService
{
   public interface IPermissionService
    {
       Task<PermissionStatus> GetLocationPermissionStatus();

        Task<bool> CheckLoacationPermission();
       Task<bool> HasCameraPermission();
    }
}
