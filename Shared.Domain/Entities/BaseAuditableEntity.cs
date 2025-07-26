using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Domain.Entities
{
    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>, IAuditableEntity where TKey : notnull
    {
        public string? CreateDate { get; private set; }
        public TimeSpan? CreateTime { get; private set; }
        public int? CreatedByUserId { get; private set; }
        public string? CreatedByUserFullName { get; private set; }
        public string? UpdateDate { get; private set; }
        public TimeSpan? UpdateTime { get; private set; }
        public int? UpdatedByUserId { get; private set; }
        public string? UpdatedByUserFullName { get; private set; }
        public string? UpdateComment { get; set; }
        public bool IsDeleted { get; private set; } = false;
        public string? DeleteDate { get; private set; }
        public TimeSpan? DeleteTime { get; private set; }
        public int? DeletedByUserId { get; private set; }
        public string? DeletedByUserFullName { get; private set; }
        public string? DeleteComment { get; set; }

        public byte[]? RowVersion { get; private set; }

        protected BaseAuditableEntity()
        {
        }
        public void SetCreated(string createDate, TimeSpan createTime, int? userID, string? userFullName)
        {
            CreateDate = createDate;
            CreateTime = createTime;
            CreatedByUserId = userID;
            CreatedByUserFullName = userFullName;
        }
        public void SetUpdated(string updateDate, TimeSpan updateTime, int? userID, string? userFullName)
        {
            UpdateDate = updateDate;
            UpdateTime = updateTime;
            UpdatedByUserId = userID;
            UpdatedByUserFullName = userFullName;
        }
        public void SetDeleted(string deleteDate, TimeSpan deleteTime, int? userID, string? userFullName)
        {
            IsDeleted = true;
            DeleteDate = deleteDate;
            DeleteTime = deleteTime;
            DeletedByUserId = userID;
            DeletedByUserFullName = userFullName;
        }
    }
}
