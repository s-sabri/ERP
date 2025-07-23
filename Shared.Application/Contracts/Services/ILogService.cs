using Shared.Application.Common;
using Shared.Application.Logging.Enums;
using Shared.Domain.Abstractions;
using Shared.Domain.Entities;

namespace Shared.Application.Contracts.Services
{
    public interface ILogService<TEntity, TKey> where TKey : notnull where TEntity : BaseEntity<TKey>, IAggregateRoot<TKey>, new()
    {
        Task AddAuditLogAsync(DateTime createdAt, LogActionEnum action, LogStatusEnum status, string EntityId,
            TEntity? newEntity, TEntity? oldEntity, ChangeLog changeLog, Exception? ex);
        void AddAppLogInformation(string message);
        void AddAppLogWarning(string message);
        void AddAppLogError(string message, Exception? ex = null);
    }
}
