using Serilog;
using Serilog.Events;

namespace AthenaService.Logger
{
    /// <summary>
    /// TODO - need to be implementaion as a library
    /// </summary>
    public enum Environment { Development, Production }

    public class LogManager : ILogManager
    {
        private readonly Serilog.ILogger _logger;

        public LogManager(Environment env)
        {
            var loggerConfiguration = new LoggerConfiguration()
               .MinimumLevel.Information()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .Enrich.FromLogContext()
               .WriteTo.Console();

            if (env == Environment.Development)
            {
                loggerConfiguration = loggerConfiguration
                           .WriteTo.Debug();
            }
            else
            {
                // TODO - add to Application Insight
            }
            _logger = loggerConfiguration
                           .CreateLogger();
        }

        public void Error(Exception ex, string message, string prefix = "")
        {
            string msg = (prefix != string.Empty) ? $"[{prefix}] {message}" : message;
            _logger.Error(ex, msg);
        }

        public void Error(string message, string prefix = "")
        {
            string msg = (prefix != string.Empty) ? $"[{prefix}] {message}" : message;
            _logger.Error(msg);
        }

        public void Error(Exception ex, string template, params object[] propertyValues)
        {
            _logger.Error(ex, template, propertyValues);
        }

        public void Information(string message, string prefix = "")
        {
            string msg = (prefix != string.Empty) ? $"[{prefix}] {message}" : message;
            _logger.Information(msg);
        }

        public void Information(string template, params object[] propertyValues)
        {
            _logger.Information(template, propertyValues);
        }

        public void Warning(string template, params object[] propertyValues)
        {
            _logger.Warning(template, propertyValues);
        }
    }
}
