using Shared.Domain.Exceptions;
using Shared.Domain.Rules.Interfaces;

namespace Shared.Domain.Rules.Base
{
    public abstract class BaseBusinessRule : IBusinessRule
    {
        public abstract string Message { get; }
        public abstract bool IsBroken();

        public void Check()
        {
            if (IsBroken())
                throw new DomainBusinessRuleViolationException(Message);
        }
    }
}
