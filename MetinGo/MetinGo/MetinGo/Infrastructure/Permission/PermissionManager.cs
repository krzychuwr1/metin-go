using System.Linq;
using System.Threading.Tasks;
using MetinGo.Infrastructure.Navigation;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace MetinGo.Infrastructure.Permission
{
    public class PermissionManager : IPermissionManager
    {
        private readonly INavigationManager _navigationManager;
        public PermissionManager(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        public async Task<bool> CheckAndAskIfNeeded(string message, string permissionDeniedMessage, Plugin.Permissions.
        Abstractions.Permission permission)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                var permissionsGranted =
                    await CrossPermissions.Current.CheckPermissionStatusAsync(permission) == PermissionStatus.Granted;
                if (!permissionsGranted)
                {
                    await _navigationManager.CurrentPage.DisplayAlert("Permission", message, "OK");
                    var result = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                    permissionsGranted = result.FirstOrDefault(p => p.Key == permission).Value == PermissionStatus.Granted;
                }
                if (!permissionsGranted)
                    await _navigationManager.CurrentPage.DisplayAlert("Permission", permissionDeniedMessage, "OK");
                return permissionsGranted;
            }
            else
            {
            return true;
            }
        }
    }
}