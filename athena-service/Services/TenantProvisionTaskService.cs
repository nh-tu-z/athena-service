using Dapper;
using Newtonsoft.Json;
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
        private readonly ITenantDatabaseSchemaService _tenantDatabaseSchemaService;
        private readonly ITenantMigrationService _tenantMigrationService;
        private readonly IRoleService _roleService;

        public TenantProvisionTaskService(ITenantService tenantService, IPersistenceService persistenceService,
                                            ITenantDatabaseSchemaService tenantDatabaseSchemaService, IRoleService roleService, 
                                            ITenantMigrationService tenantMigrationService)
        {
            _tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));
            _adminPersistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));
            _tenantDatabaseSchemaService = tenantDatabaseSchemaService ?? throw new ArgumentNullException(nameof(tenantDatabaseSchemaService));
            _tenantMigrationService = tenantMigrationService ?? throw new ArgumentNullException(nameof(tenantMigrationService));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
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

        public async Task<TenantProvisionTaskModel?> GetAsync(int taskId)
        {
            object parameters = new { TenantProvisionTaskId = taskId };
            string query = CommandTenantProvisionTaskText.GetTenantProvisionTaskById;
            var tenantProvisionTasks = await GetAllAsync(query, parameters);
            return tenantProvisionTasks.FirstOrDefault();
        }

        private async Task<IEnumerable<TenantProvisionTaskModel>> GetAllAsync(string query, object parameters)
        {
            return await _adminPersistenceService
                .QueryAsync<TenantProvisionTaskModel, TenantModel, TenantProvisionTaskModel>(
                query, (tenantProvisionTask, tenant) =>
                {
                    tenantProvisionTask.Tenant = tenant;
                    return tenantProvisionTask;
                },
                parameters, splitOn: nameof(TenantModel.TenantId));
        }

        public async Task<int> UpdateAsync(int taskId,
        TenantProvisionTaskState state, string? errorMessage)
        {
            DynamicParameters parameters = new();
            parameters.Add("TenantProvisionTaskId", taskId);
            parameters.Add("ErrorMessage", errorMessage);
            parameters.Add("State", (int)state);

            return await _adminPersistenceService.QuerySingleOrDefaultAsync<int>(
                CommandTenantProvisionTaskText.UpdateTenantProvisionTask, parameters);
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
                    string databaseName = "tuhngo-db";

                    tenantDbSchema = await _tenantDatabaseSchemaService.GetAsync(tenantId, databaseName);
                    if (tenantDbSchema == null)
                    {
                        tenantDbSchema = await _tenantDatabaseSchemaService.CreateAsync(tenantId, databaseName, schema);
                    }
                    else if (tenantDbSchema.State == TenantDatabaseSchemaState.Provisioned)
                    {
                        continue;
                    }

                    await _tenantMigrationService.PerformMigration(databaseTypeKey, databaseName, schema);

                    await _tenantDatabaseSchemaService.UpdateStateAsync(
                            tenantDbSchema.TenantDatabaseSchemaId,
                            TenantDatabaseSchemaState.Provisioned);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception.ToString());
                    if (tenantDbSchema != null)
                    {
                        await _tenantDatabaseSchemaService.UpdateStateAsync(
                            tenantDbSchema.TenantDatabaseSchemaId,
                            TenantDatabaseSchemaState.ProvisionedFailed);
                    }
                }
                TenantState tenantNewState = TenantState.Active;
                TenantProvisionTaskState taskState = TenantProvisionTaskState.Success;
                string? errorMessage = null;
                if (exceptions.Any())
                {
                    tenantNewState = TenantState.SetupFailed;
                    taskState = TenantProvisionTaskState.Failed;
                    errorMessage = JsonConvert.SerializeObject(exceptions);
                }
                else
                {
                    await _roleService.CreateRolesForTenant(tenantId);
                }
                await _tenantService.UpdateTenantStateAsync(tenantId, tenantNewState);
                await UpdateAsync(tenantProvisionTask.TenantProvisionTaskId,
                    taskState, errorMessage);
            }
        }
    }
}
