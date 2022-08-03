using AthenaService.Interfaces;
using AthenaService.Logger;

namespace AthenaService.CollectorCommunication.ServiceBus
{
    public class QueueReceiver : IQueueReceiver
    {
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
