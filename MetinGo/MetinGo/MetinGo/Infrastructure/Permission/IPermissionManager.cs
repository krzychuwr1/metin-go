using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace MetinGo.Infrastructure.Permission
{
    public interface IPermissionManager
    {
        Task<bool> CheckAndAskIfNeeded(string message, string permissionDeniedMessage, Plugin.Permissions.Abstractions.Permission permission);
    }
}