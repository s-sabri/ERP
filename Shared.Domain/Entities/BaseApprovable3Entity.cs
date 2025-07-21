using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Domain.Entities
{
    public abstract class BaseApprovable3Entity<TKey> : BaseApprovable2Entity<TKey>, IApprovable3Entity
    {
        public bool IsApproved3 { get; private set; } = false;
        public string? Approve3Date { get; private set; }
        public TimeSpan? Approve3Time { get; private set; }
        public int? Approved3ByUserId { get; private set; }
        public string? Approved3ByUserFullName { get; private set; }
        public string? Approve3Comment { get; set; }

        protected BaseApprovable3Entity()
        {
        }
        public void SetNotApproved3()
        {
            IsApproved3 = false;
            Approve3Date = null;
            Approve3Time = null;
            Approved3ByUserId = null;
            Approved3ByUserFullName = null;
            Approve3Comment = null;
        }
        public void SetApproved3(string approveDate, TimeSpan approveTime, int? userID, string? userFullName)
        {
            IsApproved3 = true;
            Approve3Date = approveDate;
            Approve3Time = approveTime;
            Approved3ByUserId = userID;
            Approved3ByUserFullName = userFullName;
        }
    }
}
