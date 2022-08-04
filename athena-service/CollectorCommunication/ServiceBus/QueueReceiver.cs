using Azure.Messaging.ServiceBus;
using AthenaService.Interfaces;
using AthenaService.Logger;

namespace AthenaService.CollectorCommunication.ServiceBus
{

    /// <summary>
    /// Backgound Service: Using Hosted service that activates a scoped service. The scoped service can use dependency injection (DI)
    /// Queue Receiver is used to receive data from Azure Service Bus and exceute them.
    /// </summary>
    public class QueueReceiver : IQueueReceiver
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _services;
        private readonly ILogManager _logManager;

        public QueueReceiver(IServiceProvider services)
        {
            _services = services;

            _logManager = services.GetRequiredService<ILogManager>();
        }

        public async Task StartAsync()
        {
            _logManager.Information("Queue Receiver Starts", GetType().Name);
        }

        public async Task StopAsync()
        {

        }

        public async Task MessageHandler()
        {

        }

        public async Task ErrorHandler()
        {

        }
    }
}
