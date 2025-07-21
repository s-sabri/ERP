namespace Shared.Application.Common
{
    public class ChangeLog
    {
        public List<EntityChange> Changes { get; set; } = new();
    }

    public class EntityChange
    {
        public string EntityPath { get; set; } = null!;
        public EntityChangeType ChangeType { get; set; }
        public List<PropertyChange> PropertyChanges { get; set; } = new();
    }

    public class PropertyChange
    {
        public string PropertyName { get; set; } = null!;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }

    public enum EntityChangeType
    {
        Added,
        Updated,
        Deleted
    }

    public enum EntityOperation
    {
        Add,
        Update,
        Delete
    }
}
