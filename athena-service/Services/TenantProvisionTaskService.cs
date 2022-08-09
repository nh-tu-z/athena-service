using Dapper;
using AthenaService.Interfaces;
using AthenaService.Domain.Models;
using AthenaService.Persistence;
using AthenaService.Common.CommandText;
using AthenaService.Domain.Base;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Services
{
    public class TenantProvisionTaskService : ITenantProvisionTaskService
    {
        private readonly ITenantService _tenantService;
        private readonly IPersistenceService _adminPersistenceService;

        public TenantProvisionTaskService(ITenantService tenantService, IPersistenceService persistenceService)
        {
            _tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));
            _adminPersistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));
        }

        public async Task<TenantProvisionTaskModel?> CreateAsync(int tenantId)
        { 
            var tenant = await _tenantService.GetTenantAsync(tenantId);
            if (tenant == null)
            {
                // throw
            }
            if (tenant.State != TenantState.AwaitingSetup && tenant.State != TenantState.SetupFailed)
            {
                // throw
            }

            var updatedTenant = await _tenantService.UpdateTenantStateAsync(tenantId, TenantState.SetupInProgress);

            try
            {
                DynamicParameters? parameters = new();
                parameters.Add("TenantId", tenantId);
                parameters.Add("ErrorMessage", null);
                parameters.Add("State", (int)TenantProvisionTaskState.Running);

                int tenantProvisionTaskId = await _adminPersistenceService.QuerySingleOrDefaultAsync<int>(
                    CommandTenantProvisionTaskText.InsertTenantProvisionTask, parameters);
                TenantProvisionTaskModel tenantProvisionTask = new();
                tenantProvisionTask.TenantProvisionTaskId = tenantProvisionTaskId;
                tenantProvisionTask.State = TenantProvisionTaskState.Running;

                _ = StartProvisionTenantTask(tenant, tenantProvisionTask);
                return tenantProvisionTask;
            }
            catch (Exception)
            {
                await _tenantService.UpdateTenantStateAsync(
                    updatedTenant.TenantId, TenantState.SetupFailed);
                throw;
            }
        }

        public Task<TenantProvisionTaskModel?> GetAsync(int taskId)
        {

        }

        private async Task StartProvisionTenantTask(TenantModel tenant, TenantProvisionTaskModel tenantProvisionTask)
        {
            string schema = tenant.TenantAlias;
            int tenantId = tenant.TenantId;
            List<string> exceptions = new();
            var tenantMigrationDirectory = new DirectoryInfo(CommonConstants.TenantMigrationDirectory);
            var subDirectories = tenantMigrationDirectory
                                    .GetDirectories()
                                    .Select(subDirectory => subDirectory.Name);
            foreach (var databaseTypeKey in subDirectories)
            {
                TenantDatabaseSchemaModel? tenantDbSchema = null;
                try
                {

                }
                catch (Exception exception)
                {
                    exceptions.Add(exception.ToString());
                    if (tenantDbSchema != null)
                    {
                        //await _tenantDatabaseSchemaService.UpdateStateAsync(
                        //    tenantDbSchema.TenantDatabaseSchemaId,
                        //    TenantDatabaseSchemaState.ProvisionedFailed);
                    }
                }
            }
        }
    }
}
