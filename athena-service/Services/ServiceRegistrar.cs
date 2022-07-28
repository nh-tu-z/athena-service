using System.Data;
using System.Data.SqlClient;
using AthenaService.Logger;
using AthenaService.Persistence;

namespace AthenaService.Services
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            Logger.Environment logEnv = env.IsDevelopment()
                ? Logger.Environment.Development
                : Logger.Environment.Production;
            return services.AddSingleton<ILogManager>(_ => new LogManager(logEnv));
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) =>
           services
            .AddTransient<IDbConnection, SqlConnection>()
            .AddScoped(options =>
            {
            var tenantConnectionString = "Data Source=2LHZQN2;Initial Catalog=tuhngo-test-source;Integrated Security=True";
            return PersistenceService.Create(tenantConnectionString);
        });
    }
}
