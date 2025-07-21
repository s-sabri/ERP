using Dapper;
using Microsoft.Data.SqlClient;

namespace Shared.Utility.Database
{
    public class SqlExecutor : ISqlExecutor
    {
        public async Task<T> QuerySingleAsync<T>(string connectionString, string sql, object? parameters = null)
        {
            await using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            return await conn.QuerySingleAsync<T>(sql, parameters);
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql, object? parameters = null)
        {
            await using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            return await conn.QueryAsync<T>(sql, parameters);
        }
        public async Task<int> ExecuteAsync(string connectionString, string sql, object? parameters = null)
        {
            await using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            return await conn.ExecuteAsync(sql, parameters);
        }
    }
}
