using Dapper;
using AthenaService.Interfaces;
using AthenaService.Persistence;
using AthenaService.Domain.Models;
using AthenaService.Common.CommandText;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Services
{
    public class TenantDatabaseSchemaService : ITenantDatabaseSchemaService
    {
        private readonly IPersistenceService _adminPersistenceService;
        private readonly ITenantService _tenantService;

        public TenantDatabaseSchemaService(IPersistenceService persistenceService, ITenantService tenantService)
        {
            _adminPersistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));
            _tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));
        }

        private async Task<TenantDatabaseSchemaModel?> GetAsync(
                string query, object parameters)
        {
            var tenantDatabaseSchemas = await _adminPersistenceService
                .QueryAsync<TenantDatabaseSchemaModel, TenantModel, TenantDatabaseSchemaModel>(
                query, (tenantDbSchema, tenant) =>
                {
                    tenantDbSchema.Tenant = tenant;
                    return tenantDbSchema;
                }, parameters, splitOn: nameof(TenantModel.TenantId));
            return tenantDatabaseSchemas.FirstOrDefault();
        }

        public async Task<TenantDatabaseSchemaModel?> GetAsync(int tenantDbSchemaId)
        {
            var parameters = new { TenantDatabaseSchemaId = tenantDbSchemaId };
            string query = CommandTenantDatabaseSchemaText.GetTenantDatabaseSchema;
            return await GetAsync(query, parameters);
        }

        public async Task<TenantDatabaseSchemaModel?> GetAsync(int tenantId, string databaseName)
        {
            var parameters = new { TenantId = tenantId, DatabaseName = databaseName };
            string query = CommandTenantDatabaseSchemaText.GetTenantDatabaseSchemaByTenantIdAndDatabaseName;
            return await GetAsync(query, parameters);
        }

        public async Task<TenantDatabaseSchemaModel> CreateAsync(int tenantId, string databaseName, string schemaName)
        {
            var tenant = await _tenantService.GetTenantAsync(tenantId);
            if (tenant == null)
            {
                // throw 
            }

            var parameters = new DynamicParameters();
            parameters.Add("TenantId", tenantId);
            parameters.Add("DatabaseName", databaseName);
            parameters.Add("SchemaName", schemaName);
            parameters.Add("State", (int)TenantDatabaseSchemaState.NotReady);

            var tenantDbSchemaId = await _adminPersistenceService.QuerySingleOrDefaultAsync<int>(
                CommandTenantDatabaseSchemaText.InsertTenantDatabaseSchema, parameters);
            TenantDatabaseSchemaModel tenantDatabaseSchema = new();
            tenantDatabaseSchema.TenantDatabaseSchemaId = tenantDbSchemaId;
            tenantDatabaseSchema.DatabaseName = databaseName;
            tenantDatabaseSchema.SchemaName = schemaName;
            tenantDatabaseSchema.State = TenantDatabaseSchemaState.NotReady;
            tenantDatabaseSchema.TenantId = tenantId;
            return tenantDatabaseSchema;
        }

        public async Task<TenantDatabaseSchemaModel> UpdateStateAsync(int tenantDatabaseSchemaId, TenantDatabaseSchemaState state)
        {
            var parameters = new DynamicParameters();
            parameters.Add("TenantDatabaseSchemaId", tenantDatabaseSchemaId);
            parameters.Add("State", (int)state);
            var tenantDbSchemaId = await _adminPersistenceService.QuerySingleOrDefaultAsync<int>(
                CommandTenantDatabaseSchemaText.UpdateTenantDatabaseSchemaState, parameters);
            TenantDatabaseSchemaModel? tenantDatabaseSchema = await GetAsync(tenantDbSchemaId);
            if (tenantDatabaseSchema == null)
            {
                // throw 
            }
            return tenantDatabaseSchema;
        }
    }
}
