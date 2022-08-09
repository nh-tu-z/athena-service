using AthenaService.Domain.Models;

namespace AthenaService.Interfaces
{
    public interface ITenantProvisionTaskService
    {
        Task<TenantProvisionTaskModel?> CreateAsync(int tenantId);

        Task<TenantProvisionTaskModel?> GetAsync(int taskId);
    }
}
