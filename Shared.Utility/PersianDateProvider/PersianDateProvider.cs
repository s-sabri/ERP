using Shared.Utility.Database;

namespace Shared.Utility.PersianDateProvider
{
    public class PersianDateProvider(ISqlExecutor executor, string connectionString) : IPersianDateProvider
    {
        public async Task<string> GetPersianDateTimeAsync()
        {
            const string sql = "SELECT FORMAT(GETDATE(), 'yyyy/MM/dd', 'fa-ir') AS PersianDate, CONVERT(TIME(0), GETDATE()) AS Time";
            var result = await executor.QuerySingleAsync<(string PersianDate, TimeSpan PersianTime)>(connectionString, sql);
            return result.PersianDate + " " + result.PersianTime.ToString();
        }
    }
}
