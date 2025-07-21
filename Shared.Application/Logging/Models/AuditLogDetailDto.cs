namespace Shared.Application.Logging.Models
{
    public class AuditLogDetailDto
    {
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? Changes { get; set; }
        public string? Exception { get; set; }
    }
}
