namespace Shared.Domain.Abstractions.Behaviors
{
    public interface IApprovable1Entity
    {
        public bool IsApproved1 { get; }
        public string? Approve1Date { get; }
        public TimeSpan? Approve1Time { get; }
        public int? Approved1ByUserId { get; }
        public string? Approved1ByUserFullName { get; }
        public string? Approve1Comment { get; set; }

        public void SetNotApproved1();
        public void SetApproved1(string approveDate, TimeSpan approveTime, int? userID, string? userFullName);
    }
}
