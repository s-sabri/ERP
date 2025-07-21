namespace Shared.Domain.Abstractions.Behaviors
{
    public interface IDeactivableEntity
    {
        public bool IsDeactive { get; }
        public string? DeactiveDate { get; }
        public TimeSpan? DeactiveTime { get; }
        public int? DeactivatedByUserId { get; }
        public string? DeactivatedByUserFullName { get; }
        public string? DeactiveComment { get; set; }

        public void SetActivated();
        public void SetDeactivated(string deactiveDate, TimeSpan deactiveTime, int? userID, string? userFullName);
    }
}
