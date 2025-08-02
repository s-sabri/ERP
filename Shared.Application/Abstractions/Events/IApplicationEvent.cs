namespace Shared.Application.Abstractions.Events
{
    public interface IApplicationEvent
    {
        DateTime OccurredOn { get; }
    }
}
