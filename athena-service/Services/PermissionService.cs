using AthenaService.Interfaces;
using AthenaService.Domain.Models;
using AthenaService.Persistence;
using AthenaService.Common.CommandText;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPersistenceService _adminPersistenceService;

        public PermissionService(IPersistenceService persistenceService)
        {
            _adminPersistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));
        }

        public async Task<IEnumerable<PermissionModel>> GetAllByTypeAsync(PermissionType type)
        {
            var permissions = await _adminPersistenceService
                .QueryAsync<PermissionModel>(CommandPermissionText.GetPermissionsByType, new { Type = (int)type });
            return permissions;
        }

        public async Task<IEnumerable<UserPermissionModel>> GetPermissionByUserId(int userId, int[] roleTypes, int? tenantId = null)
        {
            var cmd = CommandPermissionText.GetAdminUserPermissions;
            if (tenantId > 0)
            {
                cmd = CommandPermissionText.GetTenantUserPermissions;
            }
            var permissions = await _adminPersistenceService
                .QueryAsync<UserPermissionModel>(cmd, new { userId, roleTypes, tenantId });
            return permissions;
        }
    }
}
