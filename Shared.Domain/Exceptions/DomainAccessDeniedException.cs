namespace Shared.Domain.Exceptions
{
    public class DomainAccessDeniedException : DomainException
    {
        public DomainAccessDeniedException(string message)
            : base(message)
        {
        }

        public DomainAccessDeniedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
