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

        public PermissionService(IPermissions permissionPluggin)
        {
            _permissionPluggin = permissionPluggin;
        }

        #region --IPermissionService implementation--       

        public async Task<PermissionStatus> GetPermissionStatus<T>() where T : BasePermission, new()
        {
            PermissionStatus status = PermissionStatus.Unknown;
            try
            {
                status = await _permissionPluggin.RequestPermissionAsync<T>();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return await Task.FromResult(status);
        }


        public async Task<bool> CheckPermission<T>() where T : BasePermission, new()
        {
            return await GetPermissionStatus<T>() == PermissionStatus.Granted;
        }

        #endregion
    }
}
