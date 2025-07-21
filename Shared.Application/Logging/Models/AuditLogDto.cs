namespace Shared.Application.Logging.Models
{
    public class AuditLogDto
    {
        public DateTime CreatedAt { get; set; }
        public string? UserId { get; set; }
        public string? UserFullName { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Module { get; set; }
        public string? SubSystem { get; set; }
        public string? Action { get; set; }
        public string? EntityName { get; set; }
        public string? EntityId { get; set; }
        public string? Status { get; set; }
        public string? CorrelationId { get; set; }
        public string? TransactionId { get; set; }

        public AuditLogDetailDto? Detail { get; set; }
    }
}
