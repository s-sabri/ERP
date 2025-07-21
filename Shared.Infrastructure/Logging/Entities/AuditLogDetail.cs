using Shared.Domain.Entities;

namespace Shared.Infrastructure.Logging.Entities
{
    public class AuditLogDetail : BaseEntity<long>
    {
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? Changes { get; set; }
        public string? Exception { get; set; }

        public AuditLog AuditLog { get; set; } = null!;
    }
}
