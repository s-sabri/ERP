using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Domain.Entities
{
    public abstract class BaseApprovable2Entity<TKey> : BaseApprovable1Entity<TKey>, IApprovable2Entity
    {
        public bool IsApproved2 { get; private set; } = false;
        public string? Approve2Date { get; private set; }
        public TimeSpan? Approve2Time { get; private set; }
        public int? Approved2ByUserId { get; private set; }
        public string? Approved2ByUserFullName { get; private set; }
        public string? Approve2Comment { get; set; }

        protected BaseApprovable2Entity()
        {
        }
        public void SetNotApproved2()
        {
            IsApproved2 = false;
            Approve2Date = null;
            Approve2Time = null;
            Approved2ByUserId = null;
            Approved2ByUserFullName = null;
            Approve2Comment = null;
        }
        public void SetApproved2(string approveDate, TimeSpan approveTime, int? userID, string? userFullName)
        {
            IsApproved2 = true;
            Approve2Date = approveDate;
            Approve2Time = approveTime;
            Approved2ByUserId = userID;
            Approved2ByUserFullName = userFullName;
        }
    }
}
