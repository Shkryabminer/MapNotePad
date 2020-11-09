using Acr.UserDialogs;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissions _permissionPluggin;

        public PermissionService(IUserDialogs userDialogs,
                                  IPermissions permissionPluggin)
        {           
           _permissionPluggin = permissionPluggin;
        }

        #region --IPermissionService implementation--

        public async Task<bool> HasCameraPermission()
        {
            return false;
        }

        public async Task<PermissionStatus> GetLocationPermissionStatus()
        {
            PermissionStatus status = PermissionStatus.Unknown;
            try
            {
               status = await _permissionPluggin.RequestPermissionAsync<LocationPermission>();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return await Task.FromResult(status);
        }
       

        public async Task<bool> CheckLoacationPermission()
        {
            return await GetLocationPermissionStatus() == PermissionStatus.Granted;
        }



        #endregion
    }
}
