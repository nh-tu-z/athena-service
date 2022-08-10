using AthenaService.Domain.Models;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionModel>> GetAllByTypeAsync(PermissionType type);
        Task<IEnumerable<UserPermissionModel>> GetPermissionByUserId(int userId, int[] roleTypes, int? tenantId = null);
    }
}
