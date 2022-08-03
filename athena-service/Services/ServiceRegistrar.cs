using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AthenaService.AutoMappers;
using AthenaService.Configuration;
using AthenaService.CollectorCommunication.WebSocketHandler;
using AthenaService.CollectorCommunication.ServiceBus;
using AthenaService.Interfaces;
using AthenaService.Logger;
using AthenaService.Persistence;

namespace AthenaService.Services
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IPipelineService, PipelineService>();
            return services;
        }

        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            Logger.Environment logEnv = env.IsDevelopment()
                ? Logger.Environment.Development
                : Logger.Environment.Production;
            return services.AddSingleton<ILogManager>(_ => new LogManager(logEnv));
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services) =>
           services
            .AddTransient<IDbConnection, SqlConnection>()
            .AddScoped(options =>
            {
                var tenantConnectionString = "Data Source=2LHZQN2;Initial Catalog=tuhngo-test-source;Integrated Security=True";
                return PersistenceService.Create(tenantConnectionString); ;
            });

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TagProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            return services.AddSingleton(mapper);
        }

        public static IServiceCollection AddApiVersioningService(this IServiceCollection services) =>
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

        public static IServiceCollection AddWebSocket(this IServiceCollection services)
        {
            services.AddSingleton<IWebSocketFactory, WebSocketFactory>();

            return services;
        }

        public static IServiceCollection AddAndStartServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IQueueReceiver, QueueReceiver>();

            var sp = services.BuildServiceProvider();
            var queueReceiver = sp.GetService<IQueueReceiver>();
            _ = queueReceiver!.StartAsync();

            return services;
        }

        public static IServiceCollection AddServiceConfigurations(this IServiceCollection services, IConfiguration configuration) =>
            services
                .Configure<TokenSettings>(configuration.GetSection(nameof(TokenSettings)));
    }
}
