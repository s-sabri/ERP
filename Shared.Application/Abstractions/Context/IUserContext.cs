namespace Shared.Application.Abstractions.Context
{
    public interface IUserContext
    {
        int? UserId { get; }
        string? UserFullName { get; }
        string? IpAddress { get; }
        string? UserAgent { get; }
        string? CorrelationId { get; }
        string? TransactionId { get; }
    }
}
