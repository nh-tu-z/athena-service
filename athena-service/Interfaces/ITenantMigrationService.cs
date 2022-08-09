namespace AthenaService.Interfaces
{
    public interface ITenantMigrationService
    {
        Task PerformMigration(string databaseTypeKey, string databaseName, string schema);
    }
}
