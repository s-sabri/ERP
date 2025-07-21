using Shared.Utility.Number;
using Shared.Utility.PersianDateProvider;

namespace Shared.Utility
{
    public interface IUtilityUnitOfWork
    {
        IPersianDateProvider PersianDateProvider { get; }
        INumberUtility NumberUtility { get; }
    }
}
