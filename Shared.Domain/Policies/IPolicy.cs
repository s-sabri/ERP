namespace Shared.Domain.Policies
{
    public interface IPolicy<in TSubject>
    {
        string Message { get; }
        bool IsSatisfiedBy(TSubject subject);
    }
}
