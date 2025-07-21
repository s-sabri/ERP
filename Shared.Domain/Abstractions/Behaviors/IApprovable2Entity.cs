namespace Shared.Domain.Abstractions.Behaviors
{
    public interface IApprovable2Entity
    {
        public bool IsApproved2 { get; }
        public string? Approve2Date { get; }
        public TimeSpan? Approve2Time { get; }
        public int? Approved2ByUserId { get; }
        public string? Approved2ByUserFullName { get; }
        public string? Approve2Comment { get; set; }

        public void SetNotApproved2();
        public void SetApproved2(string approveDate, TimeSpan approveTime, int? userID, string? userFullName);
    }
}
