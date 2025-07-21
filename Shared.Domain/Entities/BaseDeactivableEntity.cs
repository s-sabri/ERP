using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Domain.Entities
{
    public abstract class BaseDeactivableEntity<TKey> : BaseAuditableEntity<TKey>, IDeactivableEntity
    {
        public bool IsDeactive { get; private set; } = false;
        public string? DeactiveDate { get; private set; }
        public TimeSpan? DeactiveTime { get; private set; }
        public int? DeactivatedByUserId { get; private set; }
        public string? DeactivatedByUserFullName { get; private set; }
        public string? DeactiveComment { get; set; }

        protected BaseDeactivableEntity()
        {
        }
        public void SetActivated()
        {
            IsDeactive = false;
            DeactiveDate = null;
            DeactiveTime = null;
            DeactivatedByUserId = null;
            DeactivatedByUserFullName = null;
            DeactiveComment = null;
        }
        public void SetDeactivated(string deactiveDate, TimeSpan deactiveTime, int? userID, string? userFullName)
        {
            IsDeactive = true;
            DeactiveDate = deactiveDate;
            DeactiveTime = deactiveTime;
            DeactivatedByUserId = userID;
            DeactivatedByUserFullName = userFullName;
        }
    }
}
