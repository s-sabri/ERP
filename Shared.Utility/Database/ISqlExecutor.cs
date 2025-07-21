namespace Shared.Utility.Database
{
    public interface ISqlExecutor
    {
        Task<T> QuerySingleAsync<T>(string connectionString, string sql, object? parameters = null);
        Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql, object? parameters = null);
        Task<int> ExecuteAsync(string connectionString, string sql, object? parameters = null);
    }
}
