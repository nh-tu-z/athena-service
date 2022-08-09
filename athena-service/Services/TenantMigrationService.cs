using System.Data.SqlClient;
using System.Transactions;
using AthenaService.Interfaces;
using AthenaService.Common.Utility;
using AthenaService.Domain.Base;

namespace AthenaService.Services
{
    public class TenantMigrationService : ITenantMigrationService
    {
        public async Task PerformMigration(string databaseTypeKey, string databaseName, string schema)
        {
            string username = schema;
            string password = PasswordGenerator.Generate(12, 3);

            string connectionString = "";

            string migrationDirectory = GetMigrationDirectory(databaseTypeKey);
            using TransactionScope scope = new();
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                ExecuteSqlCommand(connection, $"CREATE SCHEMA [{schema}]");
                ExecuteSqlCommand(connection, $"CREATE USER [{username}] WITH PASSWORD = '{password}', DEFAULT_SCHEMA = [{schema}]");
                ExecuteSqlCommand(connection, $"GRANT INSERT, UPDATE, SELECT, DELETE ON SCHEMA :: [{schema}] TO [{username}]");

                var evolveInstance = new Evolve.Evolve(connection)
                {
                    Locations = new[] { migrationDirectory },
                    Schemas = new[] { schema },
                    IsEraseDisabled = true,
                    Placeholders = new Dictionary<string, string>
                    { ["${schema}"] = schema }
                };
                evolveInstance.Migrate();
            }
            scope.Complete();
        }
        private static void ExecuteSqlCommand(SqlConnection connection, string sqlCommand)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandTimeout = 600;
            command.CommandText = sqlCommand;
            command.ExecuteNonQuery();
        }

        private static string GetMigrationDirectory(string databaseTypeKey)
            => CommonConstants.TenantMigrationDirectory + $"/{databaseTypeKey}";
    }
}
