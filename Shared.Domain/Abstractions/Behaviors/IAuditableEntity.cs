namespace Shared.Domain.Abstractions.Behaviors
{
    public interface IAuditableEntity
    {
        public string? CreateDate { get; }
        public TimeSpan? CreateTime { get; }
        public int? CreatedByUserId { get; }
        public string? CreatedByUserFullName { get; }
        public string? UpdateDate { get; }
        public TimeSpan? UpdateTime { get; }
        public int? UpdatedByUserId { get; }
        public string? UpdatedByUserFullName { get; }
        public string? UpdateComment { get; set; }
        public bool IsDeleted { get; }
        public string? DeleteDate { get; }
        public TimeSpan? DeleteTime { get; }
        public int? DeletedByUserId { get; }
        public string? DeletedByUserFullName { get; }
        public string? DeleteComment { get; set; }
        public byte[]? RowVersion { get; }

        public void SetCreated(string createDate, TimeSpan createTime, int? userID, string? userFullName);
        public void SetUpdated(string updateDate, TimeSpan updateTime, int? userID, string? userFullName);
        public void SetDeleted(string deleteDate, TimeSpan deleteTime, int? userID, string? userFullName);
    }
}
