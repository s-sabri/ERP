using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Domain.Entities
{
    public abstract class BaseApprovable4Entity<TKey> : BaseApprovable3Entity<TKey>, IApprovable4Entity
    {
        public bool IsApproved4 { get; private set; } = false;
        public string? Approve4Date { get; private set; }
        public TimeSpan? Approve4Time { get; private set; }
        public int? Approved4ByUserId { get; private set; }
        public string? Approved4ByUserFullName { get; private set; }
        public string? Approve4Comment { get; set; }

        protected BaseApprovable4Entity()
        {
        }
        public void SetNotApproved4()
        {
            IsApproved4 = false;
            Approve4Date = null;
            Approve4Time = null;
            Approved4ByUserId = null;
            Approved4ByUserFullName = null;
            Approve4Comment = null;
        }
        public void SetApproved4(string approveDate, TimeSpan approveTime, int? userID, string? userFullName)
        {
            IsApproved4 = true;
            Approve4Date = approveDate;
            Approve4Time = approveTime;
            Approved4ByUserId = userID;
            Approved4ByUserFullName = userFullName;
        }
    }
}
