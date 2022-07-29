using Dapper;
using System.Data;

namespace AthenaService.Persistence
{
    public interface IPersistenceService
    {
        Task<int> ExecuteAsync(string sql, object? param = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text);
        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object? param = null);
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object? param = null, string splitOn = "Id");
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object? param = null, string splitOn = "Id");
        Task<int> InsertAsync<T>(T entity) where T : class;
        Task<bool> DeleteAsync<T>(T entity) where T : class;
        Task<bool> DeleteAllAsync<T>() where T : class;
        Task<bool> UpdateAsync<T>(T entity) where T : class;
    }
}
