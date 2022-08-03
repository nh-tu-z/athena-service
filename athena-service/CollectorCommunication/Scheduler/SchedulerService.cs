using AthenaService.Logger;

namespace AthenaService.CollectorCommunication.Scheduler
{

    /// <summary>
    /// Background task that runs on a timer
    /// TODO - Adding descriptive information
    /// </summary>
    public class SchedulerService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private readonly ILogManager _logManager;
        private readonly int _intervalInSeconds;
        private DateTime _lastSentMessagesTime;

        private Timer _timer = null;

        public SchedulerService(IServiceProvider services, IConfiguration configuration)
        {
            _services = services;

            _logManager = services.GetRequiredService<ILogManager>();

            // hardcore - 5s
            _intervalInSeconds = 5;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logManager.Information("Scheduler Service Has Started", GetType().Name);

            var interval = _intervalInSeconds >= 300 ? _intervalInSeconds / 5 : _intervalInSeconds;
            _logManager.Information($"Interval: {interval}", GetType().Name);
            _timer = new Timer(DoWork, cancellationToken, TimeSpan.Zero, TimeSpan.FromSeconds(interval));
            _lastSentMessagesTime = default;

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            _logManager.Information("We're in DoWork", "AthenaService");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logManager.Information("Scheduler Service Is Stopping", GetType().Name);

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
