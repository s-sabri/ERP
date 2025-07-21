using Shared.Domain.Events;
using Shared.Domain.Repositories;

namespace Shared.Infrastructure.EFCore.Repositories
{
    public class BaseEfCoreUnitOfWork : IBaseUnitOfWork
    {
        protected readonly Microsoft.EntityFrameworkCore.DbContext _context;
        private readonly IDomainEventDispatcher _dispatcher;

        public BaseEfCoreUnitOfWork(Microsoft.EntityFrameworkCore.DbContext context, IDomainEventDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
        }
        public async Task SaveChangesAsync(IEnumerable<IDomainEvent>? domainEvents = null)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.SaveChangesAsync();

                if (domainEvents is not null && domainEvents.Any())
                {
                    await _dispatcher.DispatchAsync(domainEvents);
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
