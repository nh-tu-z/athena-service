using AthenaService.Domain.Models;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Interfaces
{
    public interface ITenantDatabaseSchemaService
    {
        public Task<TenantDatabaseSchemaModel?> GetAsync(int tenantDbSchemaId);
        public Task<TenantDatabaseSchemaModel?> GetAsync(int tenantId, string databaseName);
        public Task<TenantDatabaseSchemaModel> CreateAsync(int tenantId, string databaseName, string schemaName);
        public Task<TenantDatabaseSchemaModel> UpdateStateAsync(int tenantDatabaseSchemaId, TenantDatabaseSchemaState state);
    }
}
