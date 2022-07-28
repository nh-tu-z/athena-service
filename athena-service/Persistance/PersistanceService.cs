using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace AthenaService.Persistence
{

    /// <summary>
    /// TODO - need to implement as a separated library
    /// </summary>
    public class PersistenceService : IPersistenceService
    {
        private readonly IDbConnection _connection;

        private PersistenceService(IDbConnection connection) => _connection = connection;

        /// <summary>
        /// Factory method to create a new instance of <c>IPersistenceService</c>
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <returns>A new instance of IPersistenceService</returns>
        public static IPersistenceService Create(string connectionString)
        {
            return new PersistenceService(new SqlConnection(connectionString));
        }

        /// <summary>
        /// Factory method to create a new instance of <c>IPersistenceService</c>
        /// </summary>
        /// <param name="serverName">Server name</param>
        /// <param name="databaseName">Database name</param>
        /// <param name="userName">Database user name</param>
        /// <param name="password">Database user password</param>
        /// <returns>A new instance of IPersistenceService</returns>
        public static IPersistenceService Create(string serverName, string databaseName, string userName, string password)
        {
            string connectionString = $"Data Source={serverName};Initial Catalog={databaseName};User ID={userName};Password={password};Persist Security Info=True;MultipleActiveResultSets=True";
            return Create(connectionString);
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            return await _connection.ExecuteAsync(sql, param);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text)
        {
            return await _connection.QueryAsync<T>(sql, param, commandType: commandType);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object? param = null)
        {
            return await _connection.QueryMultipleAsync(sql, param);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null)
        {
            return await _connection.QuerySingleOrDefaultAsync<T>(sql, param);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object? param = null, string splitOn = "Id")
        {
            return await _connection.QueryAsync(sql, map, param, splitOn: splitOn);
        }
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object? param = null, string splitOn = "Id")
        {
            return await _connection.QueryAsync(sql, map, param, splitOn: splitOn);
        }

        public async Task<int> InsertAsync<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await _connection.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await _connection.DeleteAsync(entity);
        }

        public async Task<bool> DeleteAllAsync<T>() where T : class
        {
            return await _connection.DeleteAllAsync<T>();
        }

        public async Task<bool> UpdateAsync<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await _connection.UpdateAsync(entity);
        }
    }
}
