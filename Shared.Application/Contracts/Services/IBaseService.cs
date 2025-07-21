using Shared.Application.Common;
using Shared.Domain.Abstractions;
using Shared.Domain.Entities;

namespace Shared.Application.Contracts.Services
{
    public interface IBaseService<TEntity, TReadDto, TKey> where TEntity : BaseEntity<TKey>, IAggregateRoot, new()
    {
        Task<TReadDto> AddAsync<TCreateDto>(TCreateDto dto);
        Task<TReadDto> UpdateAsync<TUpdateDto>(TUpdateDto dto, TKey id);
        Task DeleteAsync(TKey id);
        Task<TReadDto> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
        Task<IEnumerable<TReadDto>> GetAllAsync();
        Task<Paginated<TReadDto>> GetAllPaginatedAsync(int page, int pageSize);
    }
}
