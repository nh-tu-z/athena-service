using AthenaService.Interfaces;
using AthenaService.Persistence;
using AthenaService.Common.CommandText;

namespace AthenaService.Services
{
    public class RoleService : IRoleService
    {
        private readonly IPersistenceService _adminPersistenceService;

        public RoleService(IPersistenceService persistenceService)
        {
            _adminPersistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));
        }

        public async Task CreateRolesForTenant(int tenantid)
        {
            if (tenantid <= 0)
            {
                return;
            }
            var param = new { TenantId = tenantid };
            await _adminPersistenceService.ExecuteAsync(CommandRoleText.CreateRolesForTenant, param);
        }
    }
}
