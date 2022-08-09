using AthenaService.Domain.Models;

namespace AthenaService.Interfaces
{
    public interface ITenantProvisionTaskService
    {
        public Task<TenantProvisionTaskModel?> CreateAsync(int tenantId);

        public Task<TenantProvisionTaskModel?> GetAsync(int taskId);
    }
}
