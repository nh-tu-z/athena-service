namespace AthenaService.Common.DbConnections
{
    public interface IConnectionStringContext
    {
        string Get();
    }

    public class ConnectionStringContext : IConnectionStringContext, IDisposable
    {
        private static volatile ConnectionStringContext _instance = new ConnectionStringContext();

        private AsyncLocal<string?> _connectionStringHolder = new AsyncLocal<string?>();

        private ConnectionStringContext()
        {
        }

        public string Get()
        {
            if (string.IsNullOrEmpty(_connectionStringHolder?.Value))
            {
                throw new InvalidOperationException($"{GetType()} has no context in scope.");
            }
            return _connectionStringHolder.Value;
        }

        private void Set(string connectionString)
        {
            _connectionStringHolder.Value = connectionString;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _connectionStringHolder.Value = default;
        }

        public static ConnectionStringContext BeginContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _instance.Set(connectionString);
            return _instance;
        }

        public static ConnectionStringContext Current { get => _instance; }
    }
}
