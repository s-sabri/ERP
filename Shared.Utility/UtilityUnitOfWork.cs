using Shared.Utility.Number;
using Shared.Utility.PersianDateProvider;

namespace Shared.Utility
{
    public class UtilityUnitOfWork(IPersianDateProvider persianDateProvider, INumberUtility numberUtility) : IUtilityUnitOfWork
    {
        public IPersianDateProvider PersianDateProvider { get; } = persianDateProvider;
        public INumberUtility NumberUtility { get; } = numberUtility;
    }
}
