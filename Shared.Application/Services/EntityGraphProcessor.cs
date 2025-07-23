using System.Collections;
using System.Reflection;
using Shared.Application.Abstractions.Context;
using Shared.Application.Common;
using Shared.Application.Contracts.Services;
using Shared.Domain.Abstractions;
using Shared.Domain.Abstractions.Behaviors;
using Shared.Domain.Entities;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Shared.Application.Services
{
    public class EntityGraphProcessor<TEntity, TKey> : IEntityGraphProcessor<TEntity, TKey> where TKey : notnull where TEntity : BaseEntity<TKey>, IAggregateRoot<TKey>
    {
        private readonly IUserContext _userContext;
        private readonly Dictionary<Type, PropertyInfo[]> _propertyCache = new();

        public EntityGraphProcessor(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public ChangeLog ProcessGraphAsync(TEntity newEntity, TEntity? oldEntity, PersianDateTime persianDateTime, EntityOperation operation)
        {
            if (newEntity == null)
                throw new DomainGraphProcessingException("New entity cannot be null");

            var changeLog = new ChangeLog();
            var visited = new HashSet<BaseEntity<TKey>>();
            var stack = new Stack<(BaseEntity<TKey> newEntity, BaseEntity<TKey>? oldEntity, string path, EntityOperation op)>();

            stack.Push((newEntity, oldEntity, typeof(TEntity).Name, operation));

            while (stack.Count > 0)
            {
                var (currentNew, currentOld, path, op) = stack.Pop();

                if (currentNew == null || visited.Contains(currentNew))
                    continue;

                visited.Add(currentNew);

                try
                {
                    ProcessEntity(currentNew, currentOld, persianDateTime.Date, persianDateTime.Time, op, path, changeLog);
                }
                catch (Exception ex)
                {
                    throw new DomainGraphProcessingException(
                        $"Error processing entity graph at path: {path}", ex);
                }

                if (op == EntityOperation.Delete)
                    continue;

                foreach (var (prop, newVal, oldVal) in GetNavigationProperties(currentNew, currentOld))
                {
                    if (newVal is IEnumerable<BaseEntity<TKey>> newCollection)
                    {
                        var oldCollection = oldVal as IEnumerable<BaseEntity<TKey>> ?? Enumerable.Empty<BaseEntity<TKey>>();
                        var newDict = ToDictionary(newCollection);
                        var oldDict = ToDictionary(oldCollection);

                        foreach (var kv in newDict)
                        {
                            if (!oldDict.ContainsKey(kv.Key))
                            {
                                stack.Push((kv.Value, null, $"{path}.{prop.Name}[{kv.Key}]", EntityOperation.Add));
                            }
                            else
                            {
                                stack.Push((kv.Value, oldDict[kv.Key], $"{path}.{prop.Name}[{kv.Key}]", EntityOperation.Update));
                            }
                        }

                        foreach (var kv in oldDict)
                        {
                            if (!newDict.ContainsKey(kv.Key))
                            {
                                stack.Push((kv.Value, null, $"{path}.{prop.Name}[{kv.Key}]", EntityOperation.Delete));
                            }
                        }
                    }
                    else if (newVal is BaseEntity<TKey> newNavEntity)
                    {
                        var oldNavEntity = oldVal as BaseEntity<TKey>;
                        stack.Push((newNavEntity, oldNavEntity, $"{path}.{prop.Name}", op));
                    }
                }
            }

            return changeLog;
        }
        private void ProcessEntity(BaseEntity<TKey> newEntity, BaseEntity<TKey>? oldEntity, string persianDate, TimeSpan persianTime,
            EntityOperation op, string path, ChangeLog log)
        {
            switch (op)
            {
                case EntityOperation.Add:
                    if (newEntity is IAuditableEntity auditableAdd)
                    {
                        auditableAdd.SetCreated(persianDate, persianTime, _userContext.UserId, _userContext.UserFullName);
                    }
                    ProcessApprovalsOnAdd(newEntity, persianDate, persianTime, path, log);
                    log.Changes.Add(BuildEntityChange(newEntity, EntityChangeType.Added, path));
                    break;

                case EntityOperation.Update:
                    if (newEntity is IAuditableEntity auditableUpdate)
                    {
                        auditableUpdate.SetUpdated(persianDate, persianTime, _userContext.UserId, _userContext.UserFullName);
                    }
                    ProcessDeactivation(newEntity, oldEntity, persianDate, persianTime, path, log);
                    ProcessApprovalsOnUpdate(newEntity, oldEntity, persianDate, persianTime, path, log);

                    var propChanges = CompareProperties(newEntity, oldEntity);
                    if (propChanges.Any())
                    {
                        foreach (var change in propChanges)
                        {
                            var propInfo = newEntity.GetType().GetProperty(change.PropertyName);
                            propInfo?.SetValue(oldEntity, propInfo.GetValue(newEntity));
                        }

                        log.Changes.Add(new EntityChange
                        {
                            EntityPath = path,
                            ChangeType = EntityChangeType.Updated,
                            PropertyChanges = propChanges
                        });
                    }
                    break;

                case EntityOperation.Delete:
                    if (newEntity is IAuditableEntity auditableDelete)
                    {
                        auditableDelete.SetDeleted(persianDate, persianTime, _userContext.UserId, _userContext.UserFullName);
                        log.Changes.Add(BuildEntityChange(newEntity, EntityChangeType.Deleted, path));
                    }
                    else
                    {
                        log.Changes.Add(BuildEntityChange(newEntity, EntityChangeType.Deleted, path));
                    }
                    break;
            }
        }
        private void ProcessDeactivation(BaseEntity<TKey> newEntity, BaseEntity<TKey>? oldEntity, string persianDate, TimeSpan persianTime,
            string path, ChangeLog log)
        {
            if (newEntity is IDeactivableEntity deact)
            {
                var old = oldEntity as IDeactivableEntity;

                if (deact.IsDeactive && (old?.IsDeactive != true))
                {
                    deact.SetDeactivated(persianDate, persianTime, _userContext.UserId, _userContext.UserFullName);
                    log.Changes.Add(BuildBehaviorChange(path, nameof(deact.IsDeactive), "false", "true"));
                }
                else if (!deact.IsDeactive && (old?.IsDeactive == true))
                {
                    deact.SetActivated();
                    log.Changes.Add(BuildBehaviorChange(path, nameof(deact.IsDeactive), "true", "false"));
                }
            }
        }
        private void ProcessApprovalsOnAdd(BaseEntity<TKey> entity, string persianDate, TimeSpan persianTime, string path, ChangeLog log)
        {
            if (entity is IApprovable1Entity a1 && a1.IsApproved1)
            {
                a1.SetApproved1(persianDate, persianTime, _userContext.UserId, _userContext.UserFullName);
                log.Changes.Add(BuildBehaviorChange(path, nameof(a1.IsApproved1), null, "true"));
            }
            if (entity is IApprovable2Entity a2 && a2.IsApproved2)
            {
                a2.SetApproved2(persianDate, persianTime, _userContext.UserId, _userContext.UserFullName);
                log.Changes.Add(BuildBehaviorChange(path, nameof(a2.IsApproved2), null, "true"));
            }
            if (entity is IApprovable3Entity a3 && a3.IsApproved3)
            {
                a3.SetApproved3(persianDate, persianTime, _userContext.UserId, _userContext.UserFullName);
                log.Changes.Add(BuildBehaviorChange(path, nameof(a3.IsApproved3), null, "true"));
            }
            if (entity is IApprovable4Entity a4 && a4.IsApproved4)
            {
                a4.SetApproved4(persianDate, persianTime, _userContext.UserId, _userContext.UserFullName);
                log.Changes.Add(BuildBehaviorChange(path, nameof(a4.IsApproved4), null, "true"));
            }
        }
        private void ProcessApprovalsOnUpdate(BaseEntity<TKey> newEntity, BaseEntity<TKey>? oldEntity, string persianDate, TimeSpan persianTime,
            string path, ChangeLog log)
        {
            if (newEntity is IApprovable1Entity a1)
                ProcessApproval(a1, oldEntity as IApprovable1Entity, persianDate, persianTime, path, log, nameof(a1.IsApproved1), a1.SetApproved1, a1.SetNotApproved1);

            if (newEntity is IApprovable2Entity a2)
                ProcessApproval(a2, oldEntity as IApprovable2Entity, persianDate, persianTime, path, log, nameof(a2.IsApproved2), a2.SetApproved2, a2.SetNotApproved2);

            if (newEntity is IApprovable3Entity a3)
                ProcessApproval(a3, oldEntity as IApprovable3Entity, persianDate, persianTime, path, log, nameof(a3.IsApproved3), a3.SetApproved3, a3.SetNotApproved3);

            if (newEntity is IApprovable4Entity a4)
                ProcessApproval(a4, oldEntity as IApprovable4Entity, persianDate, persianTime, path, log, nameof(a4.IsApproved4), a4.SetApproved4, a4.SetNotApproved4);
        }
        private void ProcessApproval<T>(T newEntity, T? oldEntity, string persianDate, TimeSpan persianTime, string path,
            ChangeLog log, string propName, Action<string, TimeSpan, int?, string?> approveMethod, Action notApproveMethod)
            where T : class
        {
            if (newEntity == null) return;

            var newVal = (bool)newEntity.GetType().GetProperty(propName)?.GetValue(newEntity)!;
            var oldVal = (bool?)(oldEntity?.GetType().GetProperty(propName)?.GetValue(oldEntity)) ?? false;

            if (newVal && !oldVal)
            {
                approveMethod(persianDate, persianTime, _userContext.UserId, _userContext.UserFullName);
                log.Changes.Add(BuildBehaviorChange(path, propName, "false", "true"));
            }
            else if (!newVal && oldVal)
            {
                notApproveMethod();
                log.Changes.Add(BuildBehaviorChange(path, propName, "true", "false"));
            }
        }
        private EntityChange BuildEntityChange(BaseEntity<TKey> entity, EntityChangeType type, string path)
        {
            var props = GetCachedProperties(entity.GetType());
            var changes = props.Select(p => new PropertyChange
            {
                PropertyName = p.Name,
                NewValue = type == EntityChangeType.Added ? p.GetValue(entity)?.ToString() : null,
                OldValue = type == EntityChangeType.Deleted ? p.GetValue(entity)?.ToString() : null
            }).ToList();

            return new EntityChange
            {
                EntityPath = path,
                ChangeType = type,
                PropertyChanges = changes
            };
        }
        private EntityChange BuildBehaviorChange(string path, string prop, string? oldVal, string? newVal)
        {
            return new EntityChange
            {
                EntityPath = path,
                ChangeType = EntityChangeType.Updated,
                PropertyChanges = new List<PropertyChange>
                {
                    new PropertyChange
                    {
                        PropertyName = prop,
                        OldValue = oldVal,
                        NewValue = newVal
                    }
                }
            };
        }
        private List<PropertyChange> CompareProperties(BaseEntity<TKey> newEntity, BaseEntity<TKey>? oldEntity)
        {
            var changes = new List<PropertyChange>();
            if (oldEntity == null) return changes;

            var props = GetCachedProperties(newEntity.GetType());
            foreach (var prop in props)
            {
                var newVal = prop.GetValue(newEntity)?.ToString();
                var oldVal = prop.GetValue(oldEntity)?.ToString();

                if (newVal != oldVal)
                {
                    changes.Add(new PropertyChange
                    {
                        PropertyName = prop.Name,
                        OldValue = oldVal,
                        NewValue = newVal
                    });
                }
            }
            return changes;
        }
        private IEnumerable<(PropertyInfo prop, object? newVal, object? oldVal)> GetNavigationProperties(BaseEntity<TKey> newEntity, BaseEntity<TKey>? oldEntity)
        {
            var props = GetCachedProperties(newEntity.GetType());
            foreach (var prop in props)
            {
                var propType = prop.PropertyType;

                if (typeof(BaseEntity<TKey>).IsAssignableFrom(propType))
                {
                    var newVal = prop.GetValue(newEntity);
                    var oldVal = oldEntity?.GetType().GetProperty(prop.Name)?.GetValue(oldEntity);

                    if (newVal != null)
                        yield return (prop, newVal, oldVal);
                }
                else if (typeof(IEnumerable).IsAssignableFrom(propType)
                         && propType.IsGenericType
                         && typeof(BaseEntity<TKey>).IsAssignableFrom(propType.GetGenericArguments()[0]))
                {
                    var newVal = prop.GetValue(newEntity);
                    var oldVal = oldEntity?.GetType().GetProperty(prop.Name)?.GetValue(oldEntity);

                    if (newVal != null)
                        yield return (prop, newVal, oldVal);
                }
            }
        }
        private Dictionary<TKey, BaseEntity<TKey>> ToDictionary(IEnumerable<BaseEntity<TKey>> collection)
        {
            var dict = new Dictionary<TKey, BaseEntity<TKey>>();

            foreach (var item in collection)
            {
                if (item == null) continue;
                dict[item.Id] = item;
            }

            return dict;
        }
        private PropertyInfo[] GetCachedProperties(Type type)
        {
            if (!_propertyCache.TryGetValue(type, out var props))
            {
                props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                _propertyCache[type] = props;
            }
            return props;
        }
    }
}
