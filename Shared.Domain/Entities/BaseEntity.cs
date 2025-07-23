using Shared.Domain.Abstractions;
using Shared.Domain.Abstractions.Behaviors;
using Shared.Domain.Events;

namespace Shared.Domain.Entities
{
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>, IAggregateRoot<TKey> where TKey : notnull
    {
        public virtual TKey Id { get; set; } = default!;

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected BaseEntity()
        {
        }
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (BaseEntity<TKey>)obj;

            if (IsTransient() || other.IsTransient())
                return false;

            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }
        public override int GetHashCode()
        {
            if (IsTransient())
                return base.GetHashCode();

            return EqualityComparer<TKey>.Default.GetHashCode(Id!);
        }
        public bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id, default!);
        }
        public static bool operator ==(BaseEntity<TKey>? left, BaseEntity<TKey>? right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return left.Equals(right);
        }
        public static bool operator !=(BaseEntity<TKey>? left, BaseEntity<TKey>? right)
        {
            return !(left == right);
        }
    }
}
