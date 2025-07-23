using AutoMapper;
using Shared.Application.Abstractions.Context;
using Shared.Application.Common;
using Shared.Application.Contracts.Services;
using Shared.Application.Logging.Enums;
using Shared.Domain.Abstractions;
using Shared.Domain.Abstractions.Behaviors;
using Shared.Domain.Entities;
using Shared.Domain.Exceptions;
using Shared.Domain.Repositories;
using Shared.Domain.ValueObjects;
using Shared.Utility;

namespace Shared.Application.Services
{
    public abstract class BaseService<TEntity, TReadDto, TKey> : IBaseService<TEntity, TReadDto, TKey> where TKey : notnull
        where TEntity : BaseEntity<TKey>, IAggregateRoot<TKey>, new()
    {
        protected readonly IBaseRepository<TEntity, TKey> _repository;
        protected readonly IUtilityUnitOfWork _utility;
        protected readonly IMapper _mapper;
        protected readonly ILogService<TEntity, TKey> _logService;
        protected readonly IUserContext _userContext;
        protected readonly IEntityGraphProcessor<TEntity, TKey> _graphProcessor;

        protected BaseService(IBaseRepository<TEntity, TKey> repository, IUtilityUnitOfWork utility, IMapper mapper,
            ILogService<TEntity, TKey> logService, IUserContext userContext, IEntityGraphProcessor<TEntity, TKey> graphProcessor)
        {
            _repository = repository;
            _utility = utility;
            _mapper = mapper;
            _logService = logService;
            _userContext = userContext;
            _graphProcessor = graphProcessor;
        }

        public virtual async Task<TReadDto> AddAsync<TCreateDto>(TCreateDto dto)
        {
            var newEntity = _mapper.Map<TEntity>(dto);

            var maxId = await _repository.GetMaxIdAsync();
            if (typeof(TKey) == typeof(int))
                newEntity.Id = (TKey)(object)(Convert.ToInt32(maxId) + 1);
            else if (typeof(TKey) == typeof(long))
                newEntity.Id = (TKey)(object)(Convert.ToInt64(maxId) + 1);

            var now = PersianDateTime.Create(await _utility.PersianDateProvider.GetPersianDateTimeAsync());
            var changeLog = _graphProcessor.ProcessGraphAsync(newEntity, null, now, EntityOperation.Add);

            await _repository.AddAsync(newEntity);
            await _logService.AddAuditLogAsync(now.ToGregorian(), LogActionEnum.Added, LogStatusEnum.Success, maxId.ToString()!, newEntity, null, changeLog, null);

            return _mapper.Map<TReadDto>(newEntity);
        }
        public virtual async Task<TReadDto> UpdateAsync<TUpdateDto>(TUpdateDto dto, TKey id)
        {
            var oldEntity = await _repository.GetByIdAsync(id, BuildIncludes());
            if (oldEntity == null)
                throw new DomainNotFoundException($"{typeof(TEntity).Name} with ID {id} not found");

            var newEntity = _mapper.Map<TEntity>(dto);
            var logOldEntity = _mapper.Map<TEntity>(oldEntity);
            var now = PersianDateTime.Create(await _utility.PersianDateProvider.GetPersianDateTimeAsync());
            var changeLog = _graphProcessor.ProcessGraphAsync(newEntity, oldEntity, now, EntityOperation.Update);

            await _repository.UpdateAsync(oldEntity);
            await _logService.AddAuditLogAsync(now.ToGregorian(), LogActionEnum.Updated, LogStatusEnum.Success, id.ToString()!, newEntity, logOldEntity, changeLog, null);

            return _mapper.Map<TReadDto>(oldEntity);
        }
        public virtual async Task DeleteAsync(TKey id)
        {
            var oldEntity = await _repository.GetByIdAsync(id, BuildIncludes());
            if (oldEntity == null)
                throw new DomainNotFoundException($"{typeof(TEntity).Name} with ID {id} not found");

            var changeLog = new ChangeLog();
            var now = PersianDateTime.Create(await _utility.PersianDateProvider.GetPersianDateTimeAsync());
            var logOldEntity = _mapper.Map<TEntity>(oldEntity);
            if (oldEntity is IAuditableEntity)
            {
                changeLog = _graphProcessor.ProcessGraphAsync(oldEntity, null, now, EntityOperation.Delete);
                await _repository.UpdateAsync(oldEntity);
            }
            else
            {
                changeLog.Changes.Add(new EntityChange
                {
                    EntityPath = typeof(TEntity).Name,
                    ChangeType = EntityChangeType.Deleted
                });
                await _repository.DeleteAsync(oldEntity);
            }
            await _logService.AddAuditLogAsync(now.ToGregorian(), oldEntity is IAuditableEntity ? LogActionEnum.SoftDeleted : LogActionEnum.HardDeleted,
                LogStatusEnum.Success, id.ToString()!, oldEntity, logOldEntity, changeLog, null);
        }
        public virtual async Task<TReadDto> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
        {
            var entity = await _repository.GetByIdAsync(id, include);
            if (entity == null)
                throw new DomainNotFoundException($"{typeof(TEntity).Name} with ID {id} not found.");

            return _mapper.Map<TReadDto>(entity);
        }
        public virtual async Task<IEnumerable<TReadDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync(true);
            return _mapper.Map<IEnumerable<TReadDto>>(list);
        }
        public virtual async Task<Paginated<TReadDto>> GetAllPaginatedAsync(int page, int pageSize)
        {
            var result = await _repository.GetAllPaginatedAsync(page, pageSize, true);

            return new Paginated<TReadDto>
            {
                Items = _mapper.Map<List<TReadDto>>(result.Items ?? []),
                TotalCount = result.TotalCount,
                Page = result.Page,
                PageSize = result.PageSize
            };
        }
        public virtual Func<IQueryable<TEntity>, IQueryable<TEntity>>? BuildIncludes()
        {
            return null;
        }
    }
}
