namespace Shared.Utility.PersianDateProvider
{
    public interface IPersianDateProvider
    {
        Task<string> GetPersianDateTimeAsync();
    }
}
