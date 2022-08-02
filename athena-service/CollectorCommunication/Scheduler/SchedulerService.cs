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

        private Timer _timer = null;

        SchedulerService(IServiceProvider services, IConfiguration configuration)
        {
            _services = services;

            _logManager = services.GetRequiredService<ILogManager>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logManager.Information("Scheduler Service Has Started", GetType().Name);

            var interval = _intervalInSeconds >= 300 ? _intervalInSeconds / 5 : _intervalInSeconds;
            //_timer = new Timer()
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {

        }

        public void Dispose()
        {

        }
    }
}
