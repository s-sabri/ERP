namespace Shared.Domain.Exceptions
{
    public class DomainValidationException : DomainException
    {
        public string? PropertyName { get; }
        public object? RejectedValue { get; }

        public DomainValidationException(string message, string? propertyName = null, object? rejectedValue = null)
            : base(message)
        {
            PropertyName = propertyName;
            RejectedValue = rejectedValue;
        }

        public DomainValidationException(string message, Exception innerException, string? propertyName = null, object? rejectedValue = null)
            : base(message, innerException)
        {
            PropertyName = propertyName;
            RejectedValue = rejectedValue;
        }

        public override string ToString()
        {
            return $"{base.ToString()} [Property: {PropertyName}, Value: {RejectedValue}]";
        }
    }
}
