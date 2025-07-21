namespace Shared.Domain.Exceptions
{
    public class DomainBusinessRuleViolationException : DomainException
    {
        public DomainBusinessRuleViolationException(string message)
            : base(message)
        {
        }
        public DomainBusinessRuleViolationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
