using Shared.Application.Logging.Models;

namespace Shared.Application.Logging.Interfaces
{
    public interface IAuditLogger
    {
        Task LogAsync(AuditLogDto entity);
    }
}
