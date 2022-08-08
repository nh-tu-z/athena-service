using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Azure.Messaging.ServiceBus;
using AthenaService.Interfaces;
using AthenaService.Logger;
using AthenaService.Common.Configuration;
using AthenaService.CollectorCommunication.Message;
using AthenaService.Common.Utility;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.CollectorCommunication.ServiceBus
{

    /// <summary>
    /// Backgound Service: Using Hosted service that activates a scoped service. The scoped service can use dependency injection (DI)
    /// Queue Receiver is used to receive data from Azure Service Bus and exceute them.
    /// This uses the Service Bus Processor to handle received message
    /// </summary>
    public class QueueReceiver : IQueueReceiver
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _services;
        private readonly ILogManager _logManager;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public QueueReceiver(IOptions<ServiceBusSetting> option, IServiceProvider services, IConfiguration configuration, ServiceBusClient client)
        {
            var serviceBus = option.Value;
            _services = services;
            _client = client;
            _processor = _client.CreateProcessor(serviceBus.QueueName);

            _logManager = services.GetRequiredService<ILogManager>();
            _memoryCache = services.GetRequiredService<IMemoryCache>();
            _configuration = configuration;
        }

        public async Task StartAsync()
        {
            try
            {
                _processor.ProcessMessageAsync += MessageHandler;
                _processor.ProcessErrorAsync += ErrorHandler;

                _logManager.Information("Queue Receiver Started", GetType().Name);
                await _processor.StartProcessingAsync();
            }
            catch (Exception ex)
            {
                _logManager.Error(ex, ex.Message, GetType().Name);
            }
        }

        public async Task StopAsync()
        {
            _logManager.Information($"Queue Receiver Stopped", GetType().Name);
            await _processor.StopProcessingAsync();
            await _processor.DisposeAsync();
            await _client.DisposeAsync();
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            try
            {
                string body = args.Message.Body.ToString();
                _logManager.Information($"Qeue Received: {body}", GetType().Name);

                var message = JsonSerializer.Deserialize<CollectorMessage>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                if (message?.MessageTypeId == (int)MessageType.Integration && Enum.IsDefined(typeof(IntegrationState), message.State))
                {
                    string cachedConnectionString = await _memoryCache.GetOrCreateConnectionString(_configuration, message.TenantId);

                    // it will change the connection string there... then using IntegrationService to update sth...
                    using (var scope = _services.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<IIntegrationService>();
                        var result = await service.UpdateStateByTokenIdAsync(message.TokenId, (IntegrationState)message.State);
                        if (result == 0)
                        {
                            _logManager.Error($"Tenant {message.TenantId} can't update integration with id: {message.ItemId}", GetType().Name);
                        }
                    }
                }

                // complete the message. messages is deleted from the queue.
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                _logManager.Error(ex, ex.Message);
            }
        }

        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logManager.Error(args.Exception.ToString(), GetType().Name);
            return Task.CompletedTask;
        }
    }
}
