namespace Shared.Domain.Abstractions.Behaviors
{
    public interface IApprovable3Entity
    {
        public bool IsApproved3 { get; }
        public string? Approve3Date { get; }
        public TimeSpan? Approve3Time { get; }
        public int? Approved3ByUserId { get; }
        public string? Approved3ByUserFullName { get; }
        public string? Approve3Comment { get; set; }

        public void SetNotApproved3();
        public void SetApproved3(string approveDate, TimeSpan approveTime, int? userID, string? userFullName);
    }
}
