using AthenaService.Domain.Models;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Interfaces
{
    public interface ITenantDatabaseSchemaService
    {
        Task<TenantDatabaseSchemaModel?> GetAsync(int tenantDbSchemaId);
        Task<TenantDatabaseSchemaModel?> GetAsync(int tenantId, string databaseName);
        Task<TenantDatabaseSchemaModel> CreateAsync(int tenantId, string databaseName, string schemaName);
        Task<TenantDatabaseSchemaModel> UpdateStateAsync(int tenantDatabaseSchemaId, TenantDatabaseSchemaState state);
    }
}
