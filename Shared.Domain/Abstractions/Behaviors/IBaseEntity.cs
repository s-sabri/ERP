namespace Shared.Domain.Abstractions.Behaviors
{
    public interface IBaseEntity<TKey> where TKey : notnull
    {
        public TKey Id { get; set; }
    }
}
