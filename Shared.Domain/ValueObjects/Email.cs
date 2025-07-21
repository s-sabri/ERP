using System.Text.RegularExpressions;
using Shared.Domain.Exceptions;

namespace Shared.Domain.ValueObjects;
public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException("آدرس ایمیل نمی‌تواند خالی باشد.");

        if (!IsValidFormat(value))
            throw new DomainValidationException($"فرمت ایمیل معتبر نیست: {value}");

        Value = value.Trim().ToLowerInvariant();
    }

    public static Email Create(string value) => new Email(value);
    private static bool IsValidFormat(string email)
    {
        const string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@(?:(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,})$";
        return Regex.IsMatch(email, pattern);
    }
    public bool IsFromDomain(string domain)
    {
        if (string.IsNullOrWhiteSpace(domain)) return false;
        return Value.EndsWith("@" + domain.ToLowerInvariant());
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
    public static implicit operator string(Email email) => email.Value;
    public static explicit operator Email(string value) => Create(value);
}
