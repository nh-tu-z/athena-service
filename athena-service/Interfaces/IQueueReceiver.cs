using Azure.Messaging.ServiceBus;

namespace AthenaService.Interfaces
{
    public interface IQueueReceiver
    {
        Task StartAsync();
        Task StopAsync();
        Task MessageHandler(ProcessMessageEventArgs args);
        Task ErrorHandler(ProcessErrorEventArgs args);
    }
}
