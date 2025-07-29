namespace Shared.Domain.Exceptions
{
    public class DomainNotFoundException : DomainException
    {
        public string EntityName { get; }
        public object EntityId { get; }

        public DomainNotFoundException(string entityName, object entityId)
            : base($"{entityName} with ID '{entityId}' not found.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }

        public DomainNotFoundException(string entityName, object entityId, Exception inner)
            : base($"{entityName} with ID '{entityId}' not found.", inner)
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }
}
