using Shared.Application.Logging.Interfaces;
using Shared.Application.Logging.Models;
using Shared.Infrastructure.Logging.Entities;

namespace Shared.Infrastructure.Logging
{
    public class EfAuditLogger : IAuditLogger
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _context;
        public EfAuditLogger(Microsoft.EntityFrameworkCore.DbContext context)
        {
            _context = context;
        }
        public async Task LogAsync(AuditLogDto dto)
        {
            var entity = new AuditLog
            {
                CreatedAt = dto.CreatedAt,
                UserId = dto.UserId,
                UserFullName = dto.UserFullName,
                IpAddress = dto.IpAddress,
                UserAgent = dto.UserAgent,
                Module = dto.Module,
                SubSystem = dto.SubSystem,
                Action = dto.Action,
                EntityName = dto.EntityName,
                EntityId = dto.EntityId,
                Status = dto.Status,
                CorrelationId = dto.CorrelationId,
                TransactionId = dto.TransactionId,
                Detail = dto.Detail is not null ? new AuditLogDetail
                {
                    OldValues = dto.Detail.OldValues,
                    NewValues = dto.Detail.NewValues,
                    Changes = dto.Detail.Changes,
                    Exception = dto.Detail.Exception
                } : null
            };

            await _context.Set<AuditLog>().AddAsync(entity);
        }
    }
}
