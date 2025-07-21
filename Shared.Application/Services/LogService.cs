using System.Text.Json;
using Shared.Application.Abstractions.Context;
using Shared.Application.Common;
using Shared.Application.Contracts.Services;
using Shared.Application.Logging.Enums;
using Shared.Application.Logging.Interfaces;
using Shared.Application.Logging.Models;
using Shared.Domain.Entities;

namespace Shared.Application.Services
{
    public class LogService<TEntity, TKey> : ILogService<TEntity, TKey> where TEntity : BaseEntity<TKey>, new()
    {
        private readonly IAuditLogger _auditLogger;
        private readonly IAppLogger<TEntity> _appLogger;
        private readonly IUserContext _userContext;

        public LogService(IAuditLogger auditLogger, IAppLogger<TEntity> appLogger, IUserContext userContext)
        {
            _auditLogger = auditLogger;
            _appLogger = appLogger;
            _userContext = userContext;
        }

        public async Task AddAuditLogAsync(DateTime createdAt, LogActionEnum action, LogStatusEnum status, string EntityId,
            TEntity? newEntity, TEntity? oldEntity, ChangeLog changeLog, Exception? ex)
        {
            var auditLog = new AuditLogDto
            {
                CreatedAt = createdAt,
                UserId = _userContext.UserId?.ToString(),
                UserFullName = _userContext.UserFullName,
                IpAddress = _userContext.IpAddress,
                UserAgent = _userContext.UserAgent,
                Module = typeof(TEntity).Namespace,
                SubSystem = typeof(TEntity).Namespace?.Split('.').Skip(1).FirstOrDefault(),
                Action = action.ToString(),
                EntityName = typeof(TEntity).Name,
                EntityId = EntityId,
                Status = status.ToString(),
                CorrelationId = _userContext.CorrelationId,
                TransactionId = _userContext.TransactionId,
                Detail = new AuditLogDetailDto
                {
                    OldValues = oldEntity != null ? JsonSerializer.Serialize(oldEntity) : null,
                    NewValues = newEntity != null ? JsonSerializer.Serialize(newEntity) : null,
                    Changes = changeLog != null ? JsonSerializer.Serialize(changeLog) : null,
                    Exception = ex != null ? ex.Message + "\n" + ex.InnerException ?? "" : null
                }
            };

            await _auditLogger.LogAsync(auditLog);
        }
        public void AddAppLogInformation(string message)
        {
            _appLogger.LogInformation(message);
        }
        public void AddAppLogWarning(string message)
        {
            _appLogger.LogWarning(message);
        }
        public void AddAppLogError(string message, Exception? ex = null)
        {
            _appLogger.LogError(message, ex);
        }
    }
}
