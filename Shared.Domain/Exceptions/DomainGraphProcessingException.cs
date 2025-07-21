namespace Shared.Domain.Exceptions
{
    public class DomainGraphProcessingException : Exception
    {
        public DomainGraphProcessingException(string message)
            : base(message)
        {
        }
        public DomainGraphProcessingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
