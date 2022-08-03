namespace AthenaService.Interfaces
{
    public interface IQueueReceiver
    {
        Task StartAsync();
        Task StopAsync();
        Task MessageHandler();
        Task ErrorHandler();
    }
}
