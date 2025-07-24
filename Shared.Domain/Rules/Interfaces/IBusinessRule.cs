namespace Shared.Domain.Rules.Interfaces
{
    public interface IBusinessRule
    {
        string Message { get; }
        bool IsBroken();
    }
}
