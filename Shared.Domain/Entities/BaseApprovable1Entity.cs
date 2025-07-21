using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Domain.Entities
{
    public abstract class BaseApprovable1Entity<TKey> : BaseDeactivableEntity<TKey>, IApprovable1Entity
    {
        public bool IsApproved1 { get; private set; } = false;
        public string? Approve1Date { get; private set; }
        public TimeSpan? Approve1Time { get; private set; }
        public int? Approved1ByUserId { get; private set; }
        public string? Approved1ByUserFullName { get; private set; }
        public string? Approve1Comment { get; set; }

        protected BaseApprovable1Entity()
        {
        }
        public void SetNotApproved1()
        {
            IsApproved1 = false;
            Approve1Date = null;
            Approve1Time = null;
            Approved1ByUserId = null;
            Approved1ByUserFullName = null;
            Approve1Comment = null;
        }
        public void SetApproved1(string approveDate, TimeSpan approveTime, int? userID, string? userFullName)
        {
            IsApproved1 = true;
            Approve1Date = approveDate;
            Approve1Time = approveTime;
            Approved1ByUserId = userID;
            Approved1ByUserFullName = userFullName;
        }
    }
}
