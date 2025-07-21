namespace Shared.Domain.Abstractions.Behaviors
{
    public interface IApprovable4Entity
    {
        public bool IsApproved4 { get; }
        public string? Approve4Date { get; }
        public TimeSpan? Approve4Time { get; }
        public int? Approved4ByUserId { get; }
        public string? Approved4ByUserFullName { get; }
        public string? Approve4Comment { get; set; }

        public void SetNotApproved4();
        public void SetApproved4(string approveDate, TimeSpan approveTime, int? userID, string? userFullName);
    }
}
