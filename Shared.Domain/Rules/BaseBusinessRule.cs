using Shared.Domain.Exceptions;

namespace Shared.Domain.Rules
{
    public abstract class BusinessRuleBase : IBusinessRule
    {
        public abstract bool IsBroken();
        public abstract string Message { get; }

        public void Check()
        {
            if (IsBroken())
                throw new DomainBusinessRuleViolationException(Message);
        }
    }
}
