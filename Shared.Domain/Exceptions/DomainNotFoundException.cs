namespace Shared.Domain.Exceptions
{
    public class DomainNotFoundException : DomainException
    {
        public DomainNotFoundException(string message)
            : base(message)
        {
        }
        public DomainNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
